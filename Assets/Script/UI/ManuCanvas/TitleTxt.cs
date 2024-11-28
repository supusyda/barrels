using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class TitleTxt : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private float rotationAngle = 15f; // Angle to rotate left and right
    [SerializeField] private float duration = 1f; // Duration of one rotation cycle

    void Start()
    {
        transform.DORotate(new Vector3(0, 0, -rotationAngle), duration / 2) // Rotate to the left
                      .SetEase(Ease.InOutSine) // Smooth easing
                      .OnComplete(() =>
                      {
                          // Rotate to the right after reaching the left
                          transform.DORotate(new Vector3(0, 0, rotationAngle), duration / 2)
                                   .SetEase(Ease.InOutSine)
                                   .SetLoops(-1, LoopType.Yoyo); // Oscillate between left and right
                      });
    }
}
