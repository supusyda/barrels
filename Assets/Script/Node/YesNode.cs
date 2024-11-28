using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YesNode : Node
{
    // Start is called before the first frame update

    [SerializeField] Transform outLine;
    public override void SetNotOccupied()
    {

        base.SetNotOccupied();
        NodeEvent.OnInteractYesNode.Invoke(-1);
        outLine.gameObject.SetActive(false);
    }
    public override void SetBox(Box2 box2)
    {


        base.SetBox(box2);
        NodeEvent.OnInteractYesNode.Invoke(1);
        outLine.gameObject.SetActive(true);
    }

}
