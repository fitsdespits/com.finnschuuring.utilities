using System.Collections;
using Cysharp.Threading.Tasks;
using UnityEngine;

public static class CoroutineHelper {
    public static UniTask CoroutineTask(MonoBehaviour gameObject, IEnumerator coroutine) {
        var uniTask = new UniTaskCompletionSource<bool>();
        gameObject.StartCoroutine(WaitForCompletion(coroutine, uniTask));
        return uniTask.Task;
    }

    private static IEnumerator WaitForCompletion(IEnumerator coroutine, UniTaskCompletionSource<bool> UniTask) {
        yield return coroutine;
        UniTask.TrySetResult(true);
    }
}
