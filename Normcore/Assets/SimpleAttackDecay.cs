using System.Collections;
using System;
using UnityEngine;

public class SimpleAttackDecay: MonoBehaviour
{
    public void PlayAudio(float attack, AudioSource source)
    {
        source.volume = 0;
        source.Play();
        StartCoroutine(Fade(attack, source, true));
    }

    public void StopAudio(float decay, AudioSource source)
    {
        StartCoroutine(Fade(decay, source, false, () => source.Stop()));
    }

    IEnumerator Fade(float ms, AudioSource source, bool positive, Action callback = null)
    {
        float timer = 0;
        float second = ms / 1000;
        int multiplier = 1;
        if (!positive)
            multiplier = -1;
        while (timer < second)
        {
            timer += Time.deltaTime;
            source.volume += multiplier * Time.deltaTime / second;
            yield return null;
        }
        if (callback != null)
            callback.Invoke();
    }

}
