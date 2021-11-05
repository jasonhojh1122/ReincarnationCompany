using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class CanvasGroupFader : MonoBehaviour {

    [SerializeField] float fadeSpeed = 10.0f;
    [SerializeField] bool defaultOn = false;
    [SerializeField] bool shouldPause = true;

    CanvasGroup canvasGroup;
    bool isOn;

    private void Awake() {
        canvasGroup = GetComponent<CanvasGroup>();
        if (defaultOn) {
            canvasGroup.alpha = 1;
            canvasGroup.blocksRaycasts = true;
            isOn = true;
            if (shouldPause)
                Time.timeScale = 0.0f;
        }
        else {
            canvasGroup.alpha = 0;
            canvasGroup.blocksRaycasts = false;
            isOn = false;
        }
    }

    public void Toggle() {
        isOn = !isOn;
        if (isOn)
            StartCoroutine(TurnOn());
        else
            StartCoroutine(TurnOff());
    }

    public void FadeOut() {
        if (isOn) {
            isOn = false;
            StartCoroutine(TurnOff());
        }
    }
    public void FadeIn() {
        if (!isOn) {
            isOn = true;
            StartCoroutine(TurnOn());
        }
    }

    IEnumerator TurnOn() {
        while (1.0 - canvasGroup.alpha > 0.01f) {
            canvasGroup.alpha += fadeSpeed * Time.fixedDeltaTime;
            yield return null;
        }
        canvasGroup.blocksRaycasts = true;
        if (shouldPause) {
            Time.timeScale = 0.0f;
        }
    }

    IEnumerator TurnOff() {
        if (shouldPause) {
            Time.timeScale = 1.0f;
        }
        while (canvasGroup.alpha > 0.01f) {
            canvasGroup.alpha -= fadeSpeed * Time.fixedDeltaTime;
            yield return null;
        }
        canvasGroup.blocksRaycasts = false;
    }


}