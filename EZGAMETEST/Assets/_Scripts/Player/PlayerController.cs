using System.Collections.Generic;
using Unity.Android.Gradle.Manifest;
using UnityEngine;
using UnityEngine.Events;

public class PlayerController : MonoBehaviour
{
    public Animator characterAnimator;

    public List<string> playerActions;

    [SerializeField] private PlayerController target;

    public bool isDodging, isPunching;

    public int health = 100;

    public void Punch()
    {
        if (canHit(target))
        {
            Debug.Log($"{gameObject.name} punch {target.gameObject.name}");
            playerActions.Add("Punch");
        }
        else
        {
            Debug.Log($"{gameObject.name} punch missed {target.gameObject.name}");
        }
        characterAnimator.SetTrigger("straightJabTrigger");

    }

    public void leftHook()
    {
        if (canHit(target))
        {
            Debug.Log($"{gameObject.name} left hook {target.gameObject.name}");
            playerActions.Add("LeftHook");
        }
        else
        {
            Debug.Log($"{gameObject.name} left hook missed {target.gameObject.name}");
        }
        characterAnimator.SetTrigger("leftHookTrigger");
    }

    public void rightHook()
    {
        if (canHit(target))
        {
            Debug.Log($"{gameObject.name} right hook {target.gameObject.name}");
            playerActions.Add("RightHook");
        }
        else
        {
            Debug.Log($"{gameObject.name} right hook missed {target.gameObject.name}");
        }
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

    public void Block()
    {
        Debug.Log($"Block action performed on {gameObject.name}, Animator assigned: {characterAnimator != null}, Controller: {characterAnimator?.runtimeAnimatorController}, Animator enabled: {characterAnimator.enabled}, GameObject active: {characterAnimator.gameObject.activeInHierarchy}");

        playerActions.Add("Block");
    }

    public void dodgeStateChange()
    {
        isDodging = !isDodging;
        // Debug.Log($"Dodge state changed to {isDodging} on {gameObject.name}, Animator assigned: {characterAnimator != null}, Controller: {characterAnimator?.runtimeAnimatorController}, Animator enabled: {characterAnimator.enabled}");
    }

    public void punchStateChange()
    {
        isPunching = !isPunching;
        // Debug.Log($"Punch state changed to {isPunching} on {gameObject.name}, Animator assigned: {characterAnimator != null}, Controller: {characterAnimator?.runtimeAnimatorController}, Animator enabled: {characterAnimator.enabled}");
    }

    public bool canHit(PlayerController target)
    {
        if (isPunching && target.isDodging)
        {
            return false;
        }
        else if (target.isDodging)
        {
            return false;
        }
        else
        {
            return false;
        }
    }

    private void OnDisable()
    {
        Debug.Log($"{gameObject.name} DISABLED at {Time.time}\n{System.Environment.StackTrace}");
    }

    
}
