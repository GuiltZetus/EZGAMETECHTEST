using Unity.Android.Gradle.Manifest;
using UnityEngine;
using UnityEngine.Events;

public class PlayerController : MonoBehaviour
{
    public Animator characterAnimator;

    public void Punch()
    {
        Debug.Log($"Punch action performed on {gameObject.name}, Animator assigned: {characterAnimator != null}, Controller: {characterAnimator?.runtimeAnimatorController}, Animator enabled: {characterAnimator.enabled}, GameObject active: {characterAnimator.gameObject.activeInHierarchy}");
        characterAnimator.SetTrigger("straightJabTrigger");
    }

    public void DodgeRight()
    {
        Debug.Log($"Dodge right action performed on {gameObject.name}, Animator assigned: {characterAnimator != null}, Controller: {characterAnimator?.runtimeAnimatorController}, Animator enabled: {characterAnimator.enabled}, GameObject active: {characterAnimator.gameObject.activeInHierarchy}");
        characterAnimator.SetTrigger("dodgeRightTrigger");
    }
    public void DodgeLeft()
    {
        Debug.Log($"Dodge left action performed on {gameObject.name}, Animator assigned: {characterAnimator != null}, Controller: {characterAnimator?.runtimeAnimatorController}, Animator enabled: {characterAnimator.enabled}, GameObject active: {characterAnimator.gameObject.activeInHierarchy}");
        characterAnimator.SetTrigger("dodgeLeftTrigger");
    }

    public void Block()
    {
        Debug.Log($"Block action performed on {gameObject.name}, Animator assigned: {characterAnimator != null}, Controller: {characterAnimator?.runtimeAnimatorController}, Animator enabled: {characterAnimator.enabled}, GameObject active: {characterAnimator.gameObject.activeInHierarchy}");
        Debug.Log("Block action performed");
    }

    private void OnDisable()
    {
        Debug.Log($"{gameObject.name} DISABLED at {Time.time}\n{System.Environment.StackTrace}");
    }
}
