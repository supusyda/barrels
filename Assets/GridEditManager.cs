using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR;

public class GridEditManager : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] int width = 3;
    [SerializeField] int height = 3;
    [SerializeField] public int totalCanOnYesNode = 0;
    [SerializeField] public int totalCanOnNoNode = 0;

    [SerializeField] private Transform cube;
    [SerializeField] private Transform yesNode;
    [SerializeField] private Transform noNode;

    [SerializeField] private Transform gridPrefabs;
    [SerializeField] private Box2 box2;
    [SerializeField] private List<BoxType> BoxTypesSO = new();
    [SerializeField] private BoxType2SO wallBox;
    [SerializeField] private BoxType2SO normalBox;
    [SerializeField] private BoxType2SO specialBox;
    [SerializeField] private BoxType2SO specialBox2;
    [SerializeField] private BoxType2SO specialBox3;
    [SerializeField] private BoxType2SO specialBox4;


    [SerializeField] private BoxType2SO noneBox;
    [SerializeField] private BoxType2SO barrelBox;



    private BoxType2SO _currentDrawBoxTypeSO2;
    [SerializeField] SaveObject saveObject1;


    private List<Node> _nodes = new();
    private List<Box2> _boxes = new();
    private List<Box2> barrels = new();

    private Transform gridBG;

    static public UnityEvent OnEditGirdSelected = new();
    static public UnityEvent<int> OnLoadPlayLevel = new();

    static public UnityEvent OnMoveBarrel = new();
    static public UnityEvent OnEnterYesNode = new();


    static public UnityEvent<SwipeDirection> OnPlayerSwipe = new UnityEvent<SwipeDirection>();

    static public GridEditManager instance;
    private bool isSpawnBox = true;
    private Transform currentNodeSpawn;







    void OnEnable() => RegisterEvents();
    void OnDisable() => UnregisterEvents();

    void Awake()
    {
        if (instance != null) Destroy(this);
        instance = this;
        _currentDrawBoxTypeSO2 = wallBox;
        currentNodeSpawn = yesNode;
    }

    private void RegisterEvents()
    {
        OnEditGirdSelected.AddListener(GenGrid);
        OnLoadPlayLevel.AddListener(LoadPlayModeByLevel);
        OnPlayerSwipe.AddListener(Shift);

        NodeEvent.OnInteractNoNode.AddListener(OnInteractNoNode);
        NodeEvent.OnInteractYesNode.AddListener(OnInteractYesNode);
    }

    private void UnregisterEvents()
    {
        OnEditGirdSelected.RemoveListener(GenGrid);
        OnLoadPlayLevel.RemoveListener(LoadPlayModeByLevel);
        OnPlayerSwipe.RemoveListener(Shift);

        NodeEvent.OnInteractNoNode.RemoveListener(OnInteractNoNode);
        NodeEvent.OnInteractYesNode.RemoveListener(OnInteractYesNode);
    }

    private void OnInteractYesNode(int amount)
    {

        totalCanOnYesNode += amount;
    }

    private void OnInteractNoNode(int amount)
    {
        totalCanOnNoNode += amount;
    }



    // Update is called once per frame
    void Update()
    {
        if (!GameManager.instance.isEditMode) return;

        HandleInput();
    }
    private void HandleInput()
    {
        ChangeCurrentDrawBox();
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePos.z = 0f;
            SpawnBlockAtMouseClick(mousePos);
        }
        if (Input.GetKeyDown(KeyCode.Space)) SaveEditGrid();
        if (Input.GetKeyDown(KeyCode.Z)) LoadEditGrid();
    }
    void GenGrid()
    {

        InitNode();
        Vector3 center = new Vector3((float)width / 2 - 0.5f, (float)height / 2 - 0.5f);
        InitBoard(center);
        SetCamPos(new Vector3(center.x, center.y, -10));
    }
    void LoadPlayModeByLevel(int level)
    {
        LoadGrid(level);
        RestPoint();
    }
    void RestPoint()
    {
        totalCanOnYesNode = 0;
        totalCanOnNoNode = 0;
    }
    void SetCamPos(Vector3 newPos)
    {
        Camera.main.transform.position = newPos;
    }
    void InitBoard(Vector3 position)
    {
        gridBG = Instantiate(gridPrefabs, position, Quaternion.identity);
        gridBG.gameObject.transform.GetComponent<SpriteRenderer>().size = new Vector2(width, height);
    }

    void SpawnBlockAtMouseClick(Vector3 mousePos)
    {
        var cellSize = 1f;
        Vector2 realNodePos = mousePos;
        // Adjust the position by adding half of the cell size to ensure rounding up

        float adjustedX = realNodePos.x + (cellSize / 2);
        float adjustedY = realNodePos.y + (cellSize / 2);
        // Calculate cell coordinates
        realNodePos.x = Mathf.FloorToInt(adjustedX) / cellSize;
        realNodePos.y = Mathf.FloorToInt(adjustedY) / cellSize;
        //get node by pos(coordinate)

        Node node = GetNodeByPos(realNodePos);
        if (isSpawnBox)
        {
            SpawnBoxInEditMode(node, _currentDrawBoxTypeSO2);
        }
        else
        {
            SpawnInteractNodeEditMode(node, currentNodeSpawn);
        }
        // 

    }
    public int GetBarrelNum()
    {
        return barrels.Count;
    }
    public List<Box2> SpawnMoreBarrel(Vector3 spawnDir)
    {
        List<Box2> barrelCopy = new(barrels);// get a copy of barrels
        List<Box2> spawnBox = new(); // return value
        foreach (Box2 box in barrelCopy)
        {
            Node node = GetNodeByPos(box.GetCurrentNodeOn().GetPosition() + spawnDir);
            // Debug.Log("box.GetPosition() + spawnDir" + (box.GetPosition() + spawnDir));
            Box2 box2 = SpawnBarrel(node);

            if (box2 != null)
            {
                spawnBox.Add(box2);
                box2.transform.name = "Box " + node.GetPosition().x + " " + node.GetPosition().y;
            };
        }
        return spawnBox;

    }
    public void DeSpawnMoreBarrel(List<Box2> spawnedBox2)
    {
        foreach (Box2 box in spawnedBox2)
        {
            barrels.Remove(box);
            _boxes.Remove(box);
            box.SetCurrentNodeOnNotOccupied();
            box.DestroyMe();
        }
    }

    void DeleteBoxInEditMode(Node node)
    {
        if (node == null) return;
        _boxes.Remove(node.GetOccupiedBox2());
        node.GetOccupiedBox2().DestroyMe();
        node.SetNotOccupied();
        Debug.Log("IS GO IN DELE");

    }

    void SpawnBoxInEditMode(Node node, BoxType2SO boxType)
    {
        if (_currentDrawBoxTypeSO2 == noneBox)
        {
            DeleteBoxInEditMode(node);
            return;
        }

        if (node == null) return;//check

        if (node.IsOccupied())
        {
            node.GetOccupiedBox2().SetMyBoxType(boxType);
            return;
        }//check

        var spawnBox = Instantiate(box2, node.GetPosition(), Quaternion.identity).GetComponent<Box2>();
        spawnBox.Init(boxType, node);
        spawnBox.SetNodeOn(node);
        _boxes.Add(spawnBox);

        if (boxType.boxType == BoxType2.barrel && GameManager.instance.isEditMode == false) barrels.Add(spawnBox);
    }
    void SpawnInteractNodeEditMode(Node node, Transform prefab)
    {
        if (node == null) return;
        Node oldNode = node;

        // SpawnNode()
        Node spawnNode = SpawnNode(prefab, oldNode.GetPosition());
        _nodes.Remove(oldNode);
        oldNode.DestroyME();
    }
    Box2 SpawnBarrel(Node nodeSpawnAt)
    {
        // Debug.Log("" + nodeSpawnAt);
        // if (nodeSpawnAt.GetPosition() == new Vector3(7, 6, 0))
        // {

        //     Debug.Log("IS OCCUPIED AT" + nodeSpawnAt.GetPosition());
        //     // Debug.Log("SPAWN AT " + nodeSpawnAt.GetPosition());
        // }
        if (!nodeSpawnAt) return null;
        if (nodeSpawnAt.IsOccupied())
        {
            // if (nodeSpawnAt.GetPosition() == new Vector3(7, 6, 0))
            // {

            //     Debug.Log("IS OCCUPIED AT" + nodeSpawnAt.GetPosition());
            //     // Debug.Log("SPAWN AT " + nodeSpawnAt.GetPosition());
            // }
            return null;
        }//check

        var spawnBox = Instantiate(box2, nodeSpawnAt.GetPosition(), Quaternion.identity).GetComponent<Box2>();
        spawnBox.Init(barrelBox, nodeSpawnAt);
        spawnBox.boxAnim.SpawnInAnim(nodeSpawnAt.GetPosition());
        barrels.Add(spawnBox);
        _boxes.Add(spawnBox);
        // if (nodeSpawnAt.GetPosition() == new Vector3(7, 6, 0))
        // {

        //     Debug.Log("SPAWN AT " + nodeSpawnAt.GetPosition());
        // }

        return spawnBox;

    }
    void InitNode()
    {
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                SpawnNode(cube, new Vector3(i, j, 0));
            }
        }
    }
    Node SpawnNode(Transform prefab, Vector3 pos)
    {
        Node spawnNode = Instantiate(prefab, pos, Quaternion.identity).GetComponent<Node>();
        _nodes.Add(spawnNode);
        spawnNode.gameObject.name = "Node " + pos.x + " " + pos.y;
        return spawnNode;
    }
    public void Shift(SwipeDirection direction)
    {

        Vector3 dir = Vector3.zero;
        switch (direction)
        {
            case SwipeDirection.Up:
                dir = Vector3.up;
                break;
            case SwipeDirection.Down:
                dir = Vector3.down;
                break;
            case SwipeDirection.Left:
                dir = Vector3.left;
                break;
            case SwipeDirection.Right:
                dir = Vector3.right;
                break;
            default:
                break;
        }

        List<Box2> orderBox = new();
        // order base move dir if go up than the first node to move is the top row and so on
        // move left then the first col need to move first

        orderBox = barrels.OrderBy(x => x.GetPosition().x).ThenByDescending(y => y.GetPosition().y).ToList();
        // if move down or right than reverse order
        if (direction == SwipeDirection.Right || direction == SwipeDirection.Down) orderBox.Reverse();


        // check can move or not
        foreach (Box2 box in orderBox)
        {
            Node targetNode = GetNodeByPos(box.GetPosition() + dir);

            if (targetNode == null) return;
            if (targetNode.IsOccupied())
            {
                if (targetNode.GetOccupiedBox2().GetBoxType() != BoxType2.barrel) return;//not move
            }
        }

        //move
        foreach (Box2 box in orderBox)
        {
            Node targetNode = GetNodeByPos(box.GetPosition() + dir);
            box.SetNodeOn(targetNode);
        }
        Event.OnDoneMoveBarrel?.Invoke();

    }
    public bool CheckBarrelCanMove(SwipeDirection direction)
    {

        Vector3 dir = Vector3.zero;
        switch (direction)
        {
            case SwipeDirection.Up:
                dir = Vector3.up;
                break;
            case SwipeDirection.Down:
                dir = Vector3.down;
                break;
            case SwipeDirection.Left:
                dir = Vector3.left;
                break;
            case SwipeDirection.Right:
                dir = Vector3.right;
                break;
            default:
                break;
        }

        List<Box2> orderBox = new();
        // order base move dir if go up than the first node to move is the top row and so on
        // move left then the first col need to move first

        orderBox = barrels.OrderBy(x => x.GetPosition().x).ThenByDescending(y => y.GetPosition().y).ToList();
        // if move down or right than reverse order
        if (direction == SwipeDirection.Right || direction == SwipeDirection.Down) orderBox.Reverse();


        // check can move or not
        foreach (Box2 box in orderBox)
        {
            Node targetNode = GetNodeByPos(box.GetPosition() + dir);

            if (targetNode == null) return false;
            if (targetNode.IsOccupied())
            {
                if (targetNode.GetOccupiedBox2().GetBoxType() != BoxType2.barrel) return false;
            }
        }
        return true;

    }
    void ResetGrid()
    {
        for (int i = 0; i < _nodes.Count; i++)
        {

            _nodes[i].DestroyME();
        }
        _boxes.ForEach(x => x.DestroyMe());
        _nodes.Clear();
        _boxes.Clear();
        barrels.Clear();


    }

    public Node GetNodeByPos(Vector3 pos)
    {
        return _nodes.FirstOrDefault(node => node.GetPosition().x == pos.x && node.GetPosition().y == pos.y);
    }

    public BoxType GetBlokTypeByValue(int value)
    {
        return BoxTypesSO.First(x => x.number == value);
    }

    void ChangeCurrentDrawBox()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            isSpawnBox = true;
            _currentDrawBoxTypeSO2 = normalBox;
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            isSpawnBox = true;

            _currentDrawBoxTypeSO2 = specialBox;
        }
        else if (Input.GetKeyDown(KeyCode.X))
        {
            isSpawnBox = true;

            _currentDrawBoxTypeSO2 = specialBox2;
        }
        else if (Input.GetKeyDown(KeyCode.C))
        {
            isSpawnBox = true;

            _currentDrawBoxTypeSO2 = specialBox3;
        }
        else if (Input.GetKeyDown(KeyCode.V))
        {
            isSpawnBox = true;

            _currentDrawBoxTypeSO2 = specialBox4;
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            isSpawnBox = true;

            _currentDrawBoxTypeSO2 = wallBox;
        }
        else if (Input.GetKeyDown(KeyCode.F))
        {
            isSpawnBox = true;

            _currentDrawBoxTypeSO2 = noneBox;
        }
        else if (Input.GetKeyDown(KeyCode.G))
        {
            isSpawnBox = true;

            _currentDrawBoxTypeSO2 = barrelBox;
        }
        else if (Input.GetKeyDown(KeyCode.W))
        {
            isSpawnBox = false;
            currentNodeSpawn = yesNode;
        }
        else if (Input.GetKeyDown(KeyCode.E))
        {
            isSpawnBox = false;

            currentNodeSpawn = noNode;
        }
        else if (Input.GetKeyDown(KeyCode.R))
        {
            isSpawnBox = false;

            currentNodeSpawn = cube;
        }
    }

    void SaveEditGrid()
    {

        if (Input.GetKeyDown(KeyCode.Space))
        {
            List<Box2.BoxData> boxDatas = new();
            List<Node.NodeData> nodeDatas = new();

            _boxes.ForEach(box => { boxDatas.Add(box.ToBoxData()); });
            _nodes.Where(node => node.GetNodeType() != NodeType.None).ToList().ForEach(node => { nodeDatas.Add(node.ToNodeData()); });

            SaveObject saveObject = new SaveObject(width, height, boxDatas.ToArray(), nodeDatas.ToArray());
            string jsonData = JsonUtility.ToJson(saveObject);

            HandleSave.Save(jsonData);
        }

    }
    void LoadEditGrid()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            LoadGrid();
        }
    }

    void LoadGrid(int level = 0)
    {
        string saveData = null;
        if (level == 0) saveData = HandleSave.Load();
        else saveData = HandleSave.Load(level);

        if (saveData == null) return;
        Debug.Log(saveData);
        saveObject1 = JsonUtility.FromJson<SaveObject>(saveData);
        if (saveObject1.boxes == null) return;

        ResetGrid();
        GenGrid();

        for (int i = 0; i < saveObject1.boxes.Length; i++)
        {
            Node node = GetNodeByPos(new Vector3(saveObject1.boxes[i].x, saveObject1.boxes[i].y, 0));
            SpawnBoxInEditMode(node, TempGetBoxType2SOByBoxType(saveObject1.boxes[i].boxType2));
        }

        if (saveObject1.nodes == null) return;

        //spawn interactNode that replace normal node
        int yesNodeTotal = 0;
        for (int i = 0; i < saveObject1.nodes?.Length; i++)
        {
            Node node = GetNodeByPos(new Vector3(saveObject1.nodes[i].x, saveObject1.nodes[i].y, 0));
            Transform interactNode = GetNodeInteractPrefabByType(saveObject1.nodes[i].nodeType);
            SpawnInteractNodeEditMode(node, interactNode);
            if (saveObject1.nodes[i].nodeType == NodeType.YesNode) yesNodeTotal++;
        }
        GameManager.instance.SetWinCodition(yesNodeTotal);

    }
    Transform GetNodeInteractPrefabByType(NodeType nodeType)
    {
        // if (nodeType == NodeType.) return null;
        if (nodeType == NodeType.YesNode)
        {
            return yesNode;
        }
        else if (nodeType == NodeType.NoNode)
        {
            return noNode;
        }
        return null;
    }
    BoxType2SO TempGetBoxType2SOByBoxType(BoxType2 boxType2)
    {
        switch (boxType2)
        {
            case BoxType2.Wall:
                return wallBox;
            case BoxType2.Normal:
                return normalBox;
            case BoxType2.Special:
                return specialBox;
            case BoxType2.barrel:
                return barrelBox;
            case BoxType2.None:
                return noneBox;
            case BoxType2.Special2:
                return specialBox2;
            case BoxType2.Special3:
                return specialBox3;
            case BoxType2.Special4:
                return specialBox4;
            default:
                return null;
        }

    }

}
[Serializable]
class SaveObject
{

    public int width;
    public int height;
    public Box2.BoxData[] boxes;
    public Node.NodeData[] nodes;

    public SaveObject(int width, int height, Box2.BoxData[] boxes, Node.NodeData[] nodes)
    {
        this.width = width;
        this.height = height;
        this.boxes = boxes;
        this.nodes = nodes;
    }

}
[Serializable]
class SaveObjectArray
{
    public SaveObject[] saveObjArray;
    public SaveObjectArray(SaveObject[] saveObjArray)
    {
        this.saveObjArray = saveObjArray;
    }
}
