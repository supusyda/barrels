using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class Box : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] SpriteRenderer _sprite;
    [SerializeField] TMP_Text number;
    private Node currentNodeOn;
    BoxType _boxType;


    Vector3 position => transform.position;


    // Update is called once per frame
    public Vector3 GetPosition()
    {
        return position;
    }

    public BoxType GetBox()
    {
        return _boxType;
    }
    public void Init(BoxType boxType, Node startNode)
    {
        SetMyBoxType(boxType);
        currentNodeOn = startNode;

    }

    private void SetMyBoxType(BoxType newBoxType)
    {
        _boxType = newBoxType;
        number.text = _boxType.number.ToString();
        _sprite.color = newBoxType.color;
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
    private void MoveToPos(Vector3 newPos)
    {
        // transform.DOMove(newPos, GameManager.instance._travelTimeToOtherNode);
    }
    void DestroyMe()
    {
        Destroy(gameObject);
    }
    public static void MerginBox(Box OccupiedBox, Box mergingBox)
    {
        // return;
        // int newNumber = OccupiedBox.GetBox().number + mergingBox.GetBox().number;
        // BoxType newBoxType = GameManager.instance.GetBlokTypeByValue(newNumber);
        // OccupiedBox.SetMyBoxType(newBoxType);
        // mergingBox.SetCurrentNodeOnNotOccupied();
        // mergingBox.transform.DOMove(OccupiedBox.position, GameManager.instance._travelTimeToOtherNode).OnComplete(() =>
        // {
        //     mergingBox.DestroyMe();
        // });
        // mergingBox.SetNotOccupied();
    }

}
