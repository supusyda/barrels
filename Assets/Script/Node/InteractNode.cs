using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractNode : Node
{
    // Start is called before the first frame update
    [SerializeField] protected Transform indicator;
    void OnDisable()
    {
        // OnMoveBarrel
    }

    public override void Init()
    {
        indicator.gameObject.SetActive(true);
        // NodeEvent.OnInteractNodeOccupied.AddListener(OnInteractNodeOccupied);
        // GridEditManager.OnMoveBarrel.AddListener(OnBarrelMove);
    }


}
