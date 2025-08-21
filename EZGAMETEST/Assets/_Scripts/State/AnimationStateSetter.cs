using UnityEngine;

public class AnimationStateSetter : StateMachineBehaviour
{
    public string booleanParameterName;
    public string booleanParameterAntiSpam;


    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (!string.IsNullOrEmpty(booleanParameterName) && booleanParameterAntiSpam != "isPunching")
        {
            animator.SetBool(booleanParameterName, true);
            animator.SetBool(booleanParameterAntiSpam, true);
        }
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (!string.IsNullOrEmpty(booleanParameterName))
        {
            animator.SetBool(booleanParameterName, false);
            animator.SetBool(booleanParameterAntiSpam, false);
        }
    }
}
