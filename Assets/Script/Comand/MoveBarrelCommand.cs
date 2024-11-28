using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBarrelCommand : ICommand
{
    // Start is called before the first frame update
    SwipeDirection swipeDirection;
    public MoveBarrelCommand(SwipeDirection swipeDirection)
    {
        this.swipeDirection = swipeDirection;
    }
    public void Cancel()
    {
        // if (GridEditManager.instance.CheckBarrelCanMove(swipeDirection) == false) return;
        if (swipeDirection == SwipeDirection.Left)
        {
            GridEditManager.instance.Shift(SwipeDirection.Right);
        }
        else if (swipeDirection == SwipeDirection.Right)
        {
            GridEditManager.instance.Shift(SwipeDirection.Left);
        }
        else if (swipeDirection == SwipeDirection.Up)
        {
            GridEditManager.instance.Shift(SwipeDirection.Down);
        }
        else if (swipeDirection == SwipeDirection.Down)
        {
            GridEditManager.instance.Shift(SwipeDirection.Up);
        }
    }

    public void Execute()
    {
        GridEditManager.instance.Shift(swipeDirection);
        AudioManager.Instance.PlaySound("BarrelMove");
    }
}
