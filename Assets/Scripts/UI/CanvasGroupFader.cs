using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class CanvasGroupFader : MonoBehaviour {

    [SerializeField] float fadeSpeed = 10.0f;
    [SerializeField] bool isOn;

    CanvasGroup canvasGroup;

    private void Awake() {
        canvasGroup = GetComponent<CanvasGroup>();
        if (!isOn) {
            canvasGroup.alpha = 0;
            canvasGroup.blocksRaycasts = false;
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
            canvasGroup.alpha += fadeSpeed * Time.deltaTime;
            yield return null;
        }
        canvasGroup.blocksRaycasts = true;
    }

    IEnumerator TurnOff() {
        while (canvasGroup.alpha > 0.01f) {
            canvasGroup.alpha -= fadeSpeed * Time.deltaTime;
            yield return null;
        }
        canvasGroup.blocksRaycasts = false;
    }


}