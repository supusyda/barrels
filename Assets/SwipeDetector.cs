using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SwipeDetector : MonoBehaviour
{
    private Vector2 fingerDown;
    private Vector2 fingerUp;
    public bool detectSwipeOnlyAfterRelease = false;
    SwipeDirection currentSwipeDirection = SwipeDirection.None;
    private bool lockSwipe = false;

    public float SWIPE_THRESHOLD = 20f;

    public static UnityEvent<bool> OnLockSwipe = new();

    // Start is called before the first frame update
    void OnEnable()
    {
        OnLockSwipe.AddListener(SetLockSwipe);
        Event.OnWinLevel.AddListener(OnWinLevel);
    }

    private void OnWinLevel()
    {
        SetLockSwipe(true);
    }

    void OnDisable()
    {
        OnLockSwipe.RemoveListener(SetLockSwipe);
    }
    void SetLockSwipe(bool isLock)
    {
        this.lockSwipe = isLock;
    }
    // Update is called once per frame
    void Update()
    {
        if (lockSwipe) return;
        foreach (Touch touch in Input.touches)
        {

            if (touch.phase == TouchPhase.Began)
            {
                fingerUp = touch.position;
                fingerDown = touch.position;
            }

            //Detects Swipe while finger is still moving
            if (touch.phase == TouchPhase.Moved)
            {
                if (!detectSwipeOnlyAfterRelease)
                {
                    fingerDown = touch.position;
                    checkSwipe();
                }
            }

            //Detects swipe after finger is released
            if (touch.phase == TouchPhase.Ended)
            {
                fingerDown = touch.position;
                if (checkSwipe() == false) return;
                if (GridEditManager.instance.CheckBarrelCanMove(currentSwipeDirection) == false) return;
                MoveBarrelCommand moveBarrelCommand = new MoveBarrelCommand(currentSwipeDirection);
                CommandScheduler.ScheduleCommand(moveBarrelCommand);
                CommandScheduler.Execute();
                // Debug.Log("SWIPE");
                // GameManager.instance.Shift(currentSwipeDirection);
            }
        }
    }

    bool checkSwipe()
    {
        //Check if Vertical swipe
        if (verticalMove() > SWIPE_THRESHOLD && verticalMove() > horizontalValMove())
        {
            //Debug.Log("Vertical");
            if (fingerDown.y - fingerUp.y > 0)//up swipe
            {
                OnSwipeUp();

            }
            else if (fingerDown.y - fingerUp.y < 0)//Down swipe
            {
                OnSwipeDown();
            }
            fingerUp = fingerDown;
            return true;

        }

        //Check if Horizontal swipe
        else if (horizontalValMove() > SWIPE_THRESHOLD && horizontalValMove() > verticalMove())
        {
            //Debug.Log("Horizontal");
            if (fingerDown.x - fingerUp.x > 0)//Right swipe
            {
                OnSwipeRight();
            }
            else if (fingerDown.x - fingerUp.x < 0)//Left swipe
            {
                OnSwipeLeft();
            }
            fingerUp = fingerDown;
            return true;

        }

        //No Movement at-all
        else
        {

            return false;
        }

    }

    float verticalMove()
    {
        return Mathf.Abs(fingerDown.y - fingerUp.y);
    }

    float horizontalValMove()
    {
        return Mathf.Abs(fingerDown.x - fingerUp.x);
    }

    //////////////////////////////////CALLBACK FUNCTIONS/////////////////////////////
    void OnSwipeUp()
    {
        // Debug.Log("Swipe UP");
        currentSwipeDirection = SwipeDirection.Up;
    }

    void OnSwipeDown()
    {
        // Debug.Log("Swipe Down");
        currentSwipeDirection = SwipeDirection.Down;
    }

    void OnSwipeLeft()
    {
        // Debug.Log("Swipe Left");
        currentSwipeDirection = SwipeDirection.Left;
    }

    void OnSwipeRight()
    {
        // Debug.Log("Swipe Right");
        currentSwipeDirection = SwipeDirection.Right;
    }
}
