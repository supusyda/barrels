using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class Node : MonoBehaviour
{
    // Start is called before the first frame update
    Box _currentBox;
    bool _isCanOnNode = false;
    bool _isCanNotOnNode = false;
    [SerializeField] Box2 _currentBox2;
    [SerializeField] NodeType _nodeType;


    [SerializeField] Vector3 cc;

    [SerializeField] Vector3 _position => transform.position;

    void Awake()
    {
        Init();
    }

    void Update()
    {
        cc = _position;
    }
    public Vector3 GetPosition()
    {
        return _position;
    }
    public Box GetOccupiedBox()
    {
        return _currentBox;
    }
    public Box2 GetOccupiedBox2()
    {
        return _currentBox2;
    }
    public bool IsOccupied()
    {
        return _currentBox != null || _currentBox2 != null;
    }
    public void SetBox(Box box)
    {
        _currentBox = box;
    }
    public virtual void SetBox(Box2 box2)
    {
        _currentBox2 = box2;


    }
    public virtual void SetNotOccupied()
    {

        _currentBox = null;
        _currentBox2 = null;

    }
    public void DestroyME()
    {
        Destroy(gameObject);
    }
    public NodeType GetNodeType()
    {
        return _nodeType;
    }
    public virtual void Init()
    {

    }
    public NodeData ToNodeData()
    {
        return new NodeData((int)GetPosition().x, (int)GetPosition().y, _nodeType);
    }
    [System.Serializable]
    public class NodeData
    {
        public int x;
        public int y;
        public NodeType nodeType;

        public NodeData(int x, int y, NodeType nodeType)
        {
            this.x = x;
            this.y = y;
            this.nodeType = nodeType;
        }
    }
    // public static Node GetNodeByType(NodeType nodeType)
    // {

    // }

}
