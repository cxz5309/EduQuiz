using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class CountEvent : StateMachineBehaviour
{
    int CountNum = 3;
    
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if(CountNum==0)
            animator.gameObject.GetComponent<TextMeshPro>().text = ("Start!");
        else
            animator.gameObject.GetComponent<TextMeshPro>().text = CountNum.ToString();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        switch (SceneManager.GetActiveScene().name)
        {
            case "EnglishScene":
                WaveManager_E.instance.timerItemFlag = true;
                break;
        }
    }

    //OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (CountNum > 0)
        {
            CountNum--;
            animator.Play("Count");
        }
        else
        {
            animator.gameObject.SetActive(false);
            switch (SceneManager.GetActiveScene().name)
            {
                case "BasicScene":
                case "MathScene":
                    WaveManager_B.instance.StartWave();
                    break;
                case "EnglishScene":
                    WaveManager_E.instance.StartWave();
                    WaveManager_E.instance.timerItemFlag = false;
                    break;
            }
        }
    }

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
