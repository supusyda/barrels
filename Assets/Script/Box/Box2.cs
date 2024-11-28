using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
public class Box2 : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _sprite;
    [SerializeField] protected BoxType2SO _boxType2SO;
    [SerializeField] private Node currentNodeOn;
    public BoxAnim boxAnim;
    Vector3 position => transform.position;
    private const float _travelTime = .2f;
    // Update is called once per frame
    public Vector3 GetPosition()
    {
        return position;
    }

    public BoxType2SO GetBox()
    {
        return _boxType2SO;
    }
    public void Init(BoxType2SO boxType2SO, Node startNode)
    {
        boxAnim = GetComponent<BoxAnim>();
        SetMyBoxType(boxType2SO);
        SetNodeOn(startNode);
    }
    void OnDisable()
    {
        _boxType2SO.OnDisable();
    }
    void Update()
    {
        _boxType2SO.Update();

    }
    public void SetMyBoxType(BoxType2SO newBoxType)
    {
        newBoxType.Init(transform);
        _boxType2SO = newBoxType;
        _sprite.sprite = newBoxType?.tileSprite;
    }
    // private void SetMyBoxType(BoxType2 newBoxType)
    // {

    //     _boxType = newBoxType;
    //     number.text = _boxType.number.ToString();
    //     _sprite.color = newBoxType.color;
    // }
    public void SetNodeOn(Node node)
    {
        if (currentNodeOn != null)
        {
            SetCurrentNodeOnNotOccupied();

        }
        currentNodeOn = node;
        currentNodeOn.SetBox(this);
        MoveToPos(currentNodeOn.GetPosition());
    }
    public void SetCurrentNodeOnNotOccupied()
    {
        currentNodeOn.SetNotOccupied();
        currentNodeOn = null;

    }
    public Node GetCurrentNodeOn()
    {
        return currentNodeOn;
    }
    private void MoveToPos(Vector3 newPos)
    {
        transform.DOMove(newPos, _travelTime);
        bool isMoveRight = newPos.x - GetPosition().x > 0 ? true : false;
        boxAnim.Lean(isMoveRight);
        // boxAnim.JumpToPosition(newPos);
    }
    public void DestroyMe()
    {
        Destroy(gameObject);
    }
    public BoxType2 GetBoxType()
    {
        return _boxType2SO.boxType;
    }
    public BoxData ToBoxData()
    {
        return new BoxData((int)GetPosition().x, (int)GetPosition().y, _boxType2SO.boxType);
    }

    [System.Serializable]
    public class BoxData
    {
        public int x;
        public int y;
        public BoxType2 boxType2;

        public BoxData(int x, int y, BoxType2 boxType2)
        {
            this.x = x;
            this.y = y;
            this.boxType2 = boxType2;
        }
    }

}

