using System.Threading.Tasks;
using UnityEngine;

public static class AnimatorExtensions {
    public static async Task WaitForCompletionAsync(this Animator animator, string trigger) {
        if (animator == null || string.IsNullOrEmpty(trigger)) {
            Debug.LogWarning("Animator or trigger is null/empty.");
            return;
        }
        animator.SetTrigger(trigger);
        while (!IsAnimatorPlaying(animator)) {
            await Task.Yield();
        }
        while (IsAnimatorPlaying(animator)) {
            await Task.Yield();
        }
    }

    private static bool IsAnimatorPlaying(this Animator animator) {
        if (animator.IsInTransition(0)) return true;
        var stateInfo = animator.GetCurrentAnimatorStateInfo(0);
        return stateInfo.normalizedTime < 1 && stateInfo.loop == false;
    }
}