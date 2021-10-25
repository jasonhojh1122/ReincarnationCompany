using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class CanvasGroupFader : MonoBehaviour {

    [SerializeField] float fadeSpeed = 5.0f;
    [SerializeField] bool state;

    CanvasGroup canvasGroup;

    private void Awake() {
        canvasGroup = GetComponent<CanvasGroup>();
        if (!state) {
            canvasGroup.alpha = 0;
            canvasGroup.blocksRaycasts = false;
        }
    }

    public void Toggle() {
        state = !state;
        if (state)
            StartCoroutine(TurnOn());
        else
            StartCoroutine(TurnOff());
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