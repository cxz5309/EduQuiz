using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UniconController : MonoBehaviour
{
    public static UniconController instance;
    private Animator animator;

    private Transform playerTr = null;   // 플레이어 위치
    public Vector3 distanceVector;
    public float speed = 2;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        animator = this.GetComponent<Animator>();
        if (GameObject.FindWithTag("Player") != null)
        {
            playerTr = GameObject.FindWithTag("Player").GetComponent<Transform>();
        }
        else if (GameObject.FindWithTag("Player") == null)
        {
            Debug.Log("플레이어를 찾을 수 없습니다. 게임을 종료합니다.");
        }
    }

    private void Update()
    {
        transform.position = Vector3.Lerp(transform.position, playerTr.position + distanceVector, Time.deltaTime * speed);
    }
    
    public void UniconPosition(float x, float y, float z)
    {
        distanceVector = new Vector3(x, y, z);
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
