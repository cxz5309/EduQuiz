using UnityEngine;
using System.Collections;
using DG.Tweening;

public class AnimationScript : MonoBehaviour
{

    void Start()
    {
        doMove();
    }

    public void doMove()
    {
        transform.DORotate(new Vector3(0, 0, 1440), 4, RotateMode.LocalAxisAdd).SetEase(Ease.OutCubic);
    }
}
