using UnityEngine;
using System.Collections;

public class FadeController : MonoBehaviour
{
    public CanvasGroup fadeGroup;
    public float fadeDuration = 1f;

    void Start()
    {
        //fade-in allo start
        StartCoroutine(FadeIn());
    }

    public IEnumerator FadeIn()
    {
        fadeGroup.alpha = 1;

        float t = 0;
        while (t < fadeDuration)
        {
            t += Time.deltaTime;
            fadeGroup.alpha = 1 - (t / fadeDuration);
            yield return null;
        }

        fadeGroup.alpha = 0;
    }

    public IEnumerator FadeOut()
    {
        fadeGroup.alpha = 0;

        float t = 0;
        while (t < fadeDuration)
        {
            t += Time.deltaTime;
            fadeGroup.alpha = t / fadeDuration;
            yield return null;
        }

        fadeGroup.alpha = 1;
    }
}
