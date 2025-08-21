using System.Collections.Generic;
using Unity.Android.Gradle.Manifest;
using UnityEngine;
using UnityEngine.Events;

public class PlayerController : MonoBehaviour
{
    public Animator characterAnimator;

    public List<string> playerActions;

    [SerializeField] private PlayerController target;

    public bool isDodging;

    public void Punch()
    {
        if (canHit(target))
        {
            Debug.Log($"{gameObject.name} punch {target.gameObject.name}");
        }
        else
        {
            Debug.Log($"{gameObject.name} punch missed {target.gameObject.name}");
        }
        characterAnimator.SetTrigger("straightJabTrigger");

        playerActions.Add("Punch");
    }

    public void DodgeRight()
    {
        Debug.Log($"Dodge right action performed on {gameObject.name}, Animator assigned: {characterAnimator != null}, Controller: {characterAnimator?.runtimeAnimatorController}, Animator enabled: {characterAnimator.enabled}, GameObject active: {characterAnimator.gameObject.activeInHierarchy}");
        characterAnimator.SetTrigger("dodgeRightTrigger");

        playerActions.Add("DodgeRight");
    }
    public void DodgeLeft()
    {
        Debug.Log($"Dodge left action performed on {gameObject.name}, Animator assigned: {characterAnimator != null}, Controller: {characterAnimator?.runtimeAnimatorController}, Animator enabled: {characterAnimator.enabled}, GameObject active: {characterAnimator.gameObject.activeInHierarchy}");
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
        Debug.Log($"Dodge state changed to {isDodging} on {gameObject.name}, Animator assigned: {characterAnimator != null}, Controller: {characterAnimator?.runtimeAnimatorController}, Animator enabled: {characterAnimator.enabled}");
    }

    public bool canHit(PlayerController target)
    {
        if (target.isDodging == false)
        {
            return true;
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
