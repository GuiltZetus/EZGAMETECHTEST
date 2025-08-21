using System.Collections.Generic;
using Unity.Android.Gradle.Manifest;
using UnityEngine;
using UnityEngine.Events;

public class PlayerController : MonoBehaviour
{
    public Animator characterAnimator;

    public List<string> playerActions;

    [SerializeField] private PlayerController target;

    public int playerHealth = 100;
    public float playerSpeed = 5f;

    public void Punch()
    {
        playerActions.Add("Punch");
        characterAnimator.SetTrigger("straightJabTrigger");

    }

    public void leftHook()
    {
        playerActions.Add("LeftHook");
        characterAnimator.SetTrigger("leftHookTrigger");
    }

    public void rightHook()
    {
        playerActions.Add("RightHook");
        characterAnimator.SetTrigger("rightHookTrigger");
    }

    public void DodgeRight()
    {
        // Debug.Log($"Dodge right action performed on {gameObject.name}, Animator assigned: {characterAnimator != null}, Controller: {characterAnimator?.runtimeAnimatorController}, Animator enabled: {characterAnimator.enabled}, GameObject active: {characterAnimator.gameObject.activeInHierarchy}");
        characterAnimator.SetTrigger("dodgeRightTrigger");
        playerActions.Add("DodgeRight");
    }
    public void DodgeLeft()
    {
        // Debug.Log($"Dodge left action performed on {gameObject.name}, Animator assigned: {characterAnimator != null}, Controller: {characterAnimator?.runtimeAnimatorController}, Animator enabled: {characterAnimator.enabled}, GameObject active: {characterAnimator.gameObject.activeInHierarchy}");
        characterAnimator.SetTrigger("dodgeLeftTrigger");
        playerActions.Add("DodgeLeft");
    }

    // public void Block()
    // {
    //     Debug.Log($"Block action performed on {gameObject.name}, Animator assigned: {characterAnimator != null}, Controller: {characterAnimator?.runtimeAnimatorController}, Animator enabled: {characterAnimator.enabled}, GameObject active: {characterAnimator.gameObject.activeInHierarchy}");

    //     playerActions.Add("Block");
    // }

    public bool canHit(PlayerController target)
    {
        if (target.characterAnimator.GetBool("isDodgingLeft") && (characterAnimator.GetBool("isPunchingLeft") || characterAnimator.GetBool("isPunchingMiddle")))
        {
            Debug.Log($"{gameObject.name} cannot hit {target.gameObject.name} because they are dodging left");
            return false;
        }
        if (target.characterAnimator.GetBool("isDodgingRight") && (characterAnimator.GetBool("isPunchingRight") || characterAnimator.GetBool("isPunchingMiddle")))
        {
            Debug.Log($"{gameObject.name} cannot hit {target.gameObject.name} because they are dodging Right");
            return false;
        }
        return true;
    }

    public void setPunchState(string boolName)
    {
        characterAnimator.SetBool(boolName, true);
        characterAnimator.SetBool("isPunching", true);

        if (canHit(target))
        {
            Debug.Log($"{gameObject.name} hit {target.gameObject.name}!");
        }
        else
        {
            Debug.Log($"{gameObject.name} punch missed {target.gameObject.name}!");
        }
    }


    

    private void OnDisable()
    {
        Debug.Log($"{gameObject.name} DISABLED at {Time.time}\n{System.Environment.StackTrace}");
    }

    
}
