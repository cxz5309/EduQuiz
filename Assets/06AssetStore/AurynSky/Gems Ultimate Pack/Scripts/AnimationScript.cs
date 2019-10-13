using UnityEngine;
using System.Collections;
using DG.Tweening;

public class AnimationScript : MonoBehaviour
{
    private GameObject player;
    private bool doMoving;

    void Start()
    {
        player = GameObject.FindWithTag("Player");
        doMove(player.transform.position);
    }

    public void doMove(Vector3 player)
    {
        transform.DORotate(new Vector3(0, 0, 1440), 4, RotateMode.LocalAxisAdd).SetEase(Ease.OutCubic).OnComplete(() =>
        {
            transform.DOJump(player, 3, 2, 2).OnComplete(() =>
            {
                doMoving = false;
                Destroy(this);
                CanvasManager.instance.PopupChange("Pause");
            });
        });
    }
}
