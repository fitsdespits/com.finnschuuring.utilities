using System.Collections;
using System.Threading.Tasks;
using UnityEngine;

public static class CoroutineHelper {
    public static Task CoroutineTask(MonoBehaviour gameObject, IEnumerator coroutine) {
        var task = new TaskCompletionSource<bool>();
        gameObject.StartCoroutine(WaitForCompletion(coroutine, task));
        return task.Task;
    }

    private static IEnumerator WaitForCompletion(IEnumerator coroutine, TaskCompletionSource<bool> task) {
        yield return coroutine;
        task.SetResult(true);
    }
}