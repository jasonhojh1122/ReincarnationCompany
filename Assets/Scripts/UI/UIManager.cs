using System.Collections;
using UnityEngine;

public class UIManager : MonoBehaviour {

    [SerializeField] float fadeSpeed;
    [SerializeField] GameObject mainCanvas;
    CanvasGroup mainCanvasGroup;
    GameObject otherCanvas;
    CanvasGroup otherCanvasGroup;

    bool OnMain;

    private void Start() {
        OnMain = true;
        mainCanvasGroup = mainCanvas.GetComponent<CanvasGroup>();
    }

    public void FadeUI(bool On) {
        if (On) {
            if (OnMain)
                StartCoroutine(TurnOn(mainCanvas, mainCanvasGroup));
            else
                StartCoroutine(TurnOn(otherCanvas, otherCanvasGroup));
        }
        else {
            if (OnMain)
                StartCoroutine(TurnOff(mainCanvas, mainCanvasGroup));
            else
                StartCoroutine(TurnOff(otherCanvas, otherCanvasGroup));
        }
    }

    IEnumerator TurnOn(GameObject canvas, CanvasGroup canvasGroup) {
        canvas.SetActive(true);
        while (1.0 - canvasGroup.alpha > 0.01f) {
            canvasGroup.alpha += fadeSpeed * Time.deltaTime;
            yield return null;
        }
    }

    IEnumerator TurnOff(GameObject canvas, CanvasGroup canvasGroup) {
        while (canvasGroup.alpha > 0.01f) {
            canvasGroup.alpha -= fadeSpeed * Time.deltaTime;
            yield return null;
        }
        canvas.SetActive(false);
    }

    public void ActivateNewUI(string name) {
        StartCoroutine(TurnOff(mainCanvas, mainCanvasGroup));
        otherCanvas = GameObject.Find(name);
        otherCanvasGroup = otherCanvas.GetComponent<CanvasGroup>();
        StartCoroutine(TurnOn(otherCanvas, otherCanvasGroup));
        OnMain = false;
    }


}