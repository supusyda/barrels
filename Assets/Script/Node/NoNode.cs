using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoNode : Node
{
    // Start is called before the first frame update

    [SerializeField] Transform outLine;


    public override void SetNotOccupied()
    {
        base.SetNotOccupied();
        Debug.Log("GET OUT OF NO NODE BRUH");
        NodeEvent.OnInteractNoNode.Invoke(-1);
        outLine.gameObject.SetActive(false);
    }
    public override void SetBox(Box2 box2)
    {
        base.SetBox(box2);
        Debug.Log("ON NO NODE BRUH");
        NodeEvent.OnInteractNoNode.Invoke(1);
        outLine.gameObject.SetActive(true);
    }
}
