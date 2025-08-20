using System.Collections.Generic;
using Unity.Android.Gradle.Manifest;
using UnityEngine;
using UnityEngine.Events;

public class PlayerController : MonoBehaviour
{
    public Animator characterAnimator;

    public List<string> playerActions;

    public void Punch()
    {
        Debug.Log($"Punch action performed on {gameObject.name}, Animator assigned: {characterAnimator != null}, Controller: {characterAnimator?.runtimeAnimatorController}, Animator enabled: {characterAnimator.enabled}, GameObject active: {characterAnimator.gameObject.activeInHierarchy}");
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
        Debug.Log("Block action performed");

        playerActions.Add("Block");
    }

    private void OnDisable()
    {
        Debug.Log($"{gameObject.name} DISABLED at {Time.time}\n{System.Environment.StackTrace}");
    }
}
