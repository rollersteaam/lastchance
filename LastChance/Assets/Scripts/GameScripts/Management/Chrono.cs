using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Quick access for doing things after a certain time.
/// </summary>
public class Chrono : Singleton<Chrono>
{
    IEnumerator Timer(float seconds, System.Action action, System.Action<float> perTickAction = null)
    {
        float progressed = 0;

        while (progressed < seconds)
        {
            progressed += Time.deltaTime;

            if (perTickAction != null)
            {
                perTickAction(Mathf.Clamp(progressed / seconds, 0f, 1f));
            }

            yield return null;
        }

        action();
    }

    /// <summary>
    /// Performs an action after an amount of seconds.
    /// </summary>
    /// <param name="seconds"></param>
    /// <param name="action"></param>
    public void After(float seconds, System.Action action)
    {
        StartCoroutine(Timer(seconds, action));
    }

    /// <summary>
    /// Calls the per tick action every iteration it can, passing the normalized
    /// progress to the function each time.
    /// </summary>
    /// <param name="seconds"></param>
    /// <param name="timerEndAction"></param>
    /// <param name="perTickAction"></param>
    public void During(
        float seconds,
        System.Action timerEndAction,
        System.Action<float> perTickAction)
    {
        StartCoroutine(Timer(seconds, timerEndAction, perTickAction));
    }
}
