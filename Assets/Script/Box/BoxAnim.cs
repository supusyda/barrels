using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class BoxAnim : MonoBehaviour
{
    // Start is called before the first frame update
    public Vector3 targetPosition; // Set this to your desired off-screen position
    public float jumpPower = 5f;   // Height of the jump
    public int numJumps = 3;       // Number of times the object "jumps"
    public float duration = .3f;   // Duration of the entire animation
    private Camera mainCamera; // Assign your main camera here
    public float extraDistance = 5f; // How far off-screen the position should be
    private SpriteRenderer _model;

    public int leanAngle = 15;

    public void SetBoxAnim(SpriteRenderer model)
    {
        _model = model;
    }

    public void JumpToPosition(Vector3 pos)
    {
        transform.DOJump(pos, jumpPower, numJumps, duration)
            .SetEase(Ease.OutQuad) // Optional: Use easing for a smoother effect
            .OnComplete(() =>
            {
                transform.position = pos;
            });
    }
    Vector3 GetRandomOffscreenPosition()
    {
        // Get the camera's bounds in world space
        mainCamera = Camera.main;
        float screenLeft = mainCamera.ViewportToWorldPoint(new Vector3(0, 0.5f, 0)).x;
        float screenRight = mainCamera.ViewportToWorldPoint(new Vector3(1, 0.5f, 0)).x;
        float screenTop = mainCamera.ViewportToWorldPoint(new Vector3(0.5f, 1, 0)).y;
        float screenBottom = mainCamera.ViewportToWorldPoint(new Vector3(0.5f, 0, 0)).y;

        // Randomly choose one of the four off-screen sides
        int side = Random.Range(0, 4); // 0 = left, 1 = right, 2 = top, 3 = bottom
        Vector3 position = Vector3.zero;

        switch (side)
        {
            case 0: // Left
                position = new Vector3(screenLeft - extraDistance, Random.Range(screenBottom, screenTop), 0);
                break;
            case 1: // Right
                position = new Vector3(screenRight + extraDistance, Random.Range(screenBottom, screenTop), 0);
                break;
            case 2: // Top
                position = new Vector3(Random.Range(screenLeft, screenRight), screenTop + extraDistance, 0);
                break;
            case 3: // Bottom
                position = new Vector3(Random.Range(screenLeft, screenRight), screenBottom - extraDistance, 0);
                break;
        }

        return position;
    }
    public void SpawnInAnim(Vector3 spawnPos)
    {

        transform.position = GetRandomOffscreenPosition();
        JumpToPosition(spawnPos);
    }
    public void Lean(bool isLeanRight = true)
    {
        // Lean to the right
        if (isLeanRight)
        {
            transform.DORotate(new Vector3(0, 0, -leanAngle), duration / 2, RotateMode.LocalAxisAdd)
                .OnComplete(() =>
                {
                    // Lean back to the original position
                    transform.DORotate(new Vector3(0, 0, leanAngle), duration / 2, RotateMode.LocalAxisAdd).OnComplete(() =>
                {
                    transform.rotation = Quaternion.identity;
                });
                });

        }
        else
        {
            transform.DORotate(new Vector3(0, 0, leanAngle), duration / 2, RotateMode.LocalAxisAdd)
               .OnComplete(() =>
               {
                   // Lean back to the original position
                   transform.DORotate(new Vector3(0, 0, -leanAngle), duration / 2, RotateMode.LocalAxisAdd).OnComplete(() =>
                {
                    transform.rotation = Quaternion.identity;
                }); ;
               });
        }
    }
}
