using UnityEngine;
using TMPro;
using System.Collections;

public class Timewarning : MonoBehaviour
{
    public float totalTime = 120f;
    public TMP_Text timerText;
    public AudioSource warningAudio; 
    public CanvasGroup redFlashOverlay;

    private bool warningStarted = false;

    void Update()
    {
        totalTime -= Time.deltaTime;

        if (totalTime < 0)
            totalTime = 0;

        timerText.text = Mathf.CeilToInt(totalTime).ToString();

        if (totalTime <= 30f && !warningStarted)
        {
            warningStarted = true;
            warningAudio.Play();
            StartCoroutine(BlinkTimerText());
            StartCoroutine(FlashRedOverlay());
        }
    }

    IEnumerator BlinkTimerText()
    {
        while (totalTime > 0)
        {
            timerText.color = Color.red;
            yield return new WaitForSeconds(0.5f);
            timerText.color = Color.white;
            yield return new WaitForSeconds(0.5f);
        }
    }

    IEnumerator FlashRedOverlay()
    {
        while (totalTime > 0)
        {
            redFlashOverlay.alpha = 0.4f;
            yield return new WaitForSeconds(1f);
            redFlashOverlay.alpha = 0f;
            yield return new WaitForSeconds(1f);
        }
    }
}