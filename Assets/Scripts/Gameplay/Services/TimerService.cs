using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class TimerService : MonoBehaviour
{
    private Coroutine _coroutine;

    public void StartTimer(float duration, Action onTimerEndCallback)
    {
        StartCoroutine(TimerCoroutine(duration, onTimerEndCallback));
    }

    private IEnumerator TimerCoroutine(float duration, Action onTimerEndCallback)
    {
        yield return new WaitForSeconds(duration);
        onTimerEndCallback?.Invoke();
    }
}