using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UniconController : MonoBehaviour
{
    public static UniconController instance;
    private Animator animator;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        animator = this.GetComponent<Animator>();
    }

    public void ClickAniEvent(int behavior)
    {
        switch (behavior)
        {
            case 1:
                animator.Play("Happy", -1, 0);
                break;
            case 2:
                animator.Play("No", -1, 0);
                break;
            case 3:
                animator.Play("Walk", -1, 0);
                break;
            case 4:
                animator.Play("Eating", -1, 0);
                break;
            case 5:
                animator.Play("Horn_Atk", -1, 0);
                break;
            case 6:
                animator.Play("Flying", -1, 0);
                break;
            case 7:
                animator.Play("Jump", -1, 0);
                break;
        }
    }


    private void OnDestroy()
    {
        instance = null;
    }
}
