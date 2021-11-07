using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Camera mainCamera;
    [SerializeField] Canvas canvas;
    [SerializeField] List<CanvasGroupFader> faders;
    [SerializeField] Gesture.GestureManager gestureManager;
    private AsyncOperation async = null;
    private string additiveScene;

    public void SetCameraPos(float xPos) {
        Vector3 newPos = mainCamera.transform.position;
        newPos.x = xPos;
        mainCamera.transform.position = newPos;
    }

    public void LoadScene(string name) {
        StartCoroutine(LoadGameScene(name));
    }

    public void UnloadScene() {
        SetCameraPos(80.0f);
        FadeInUI();
        UserStateManager.Instance.SaveState();
        UserStateManager.Instance.LogState();
        SceneManager.UnloadSceneAsync(additiveScene);
        SceneManager.SetActiveScene(SceneManager.GetSceneByName("_Main"));
        gestureManager.ToggleIndicator(false);
        gestureManager.ClearQueue();
        Time.timeScale = 1.0f;
    }

    private IEnumerator LoadGameScene(string sceneName) {
        additiveScene = sceneName;
        async = SceneManager.LoadSceneAsync(additiveScene, LoadSceneMode.Additive);
        while (!async.isDone) {
            yield return null;
        }
        FadeOutUI();
        SetCameraPos(0);
        SceneManager.SetActiveScene(SceneManager.GetSceneByName(additiveScene));
        Debug.Log("Active Scene : " + SceneManager.GetActiveScene().name);
    }

    private void FadeOutUI() {
        foreach (CanvasGroupFader cgf in faders) {
            cgf.FadeOut();
        }
    }

    private void FadeInUI() {
        foreach (CanvasGroupFader cgf in faders) {
            cgf.FadeIn();
        }
    }

    public void ToggleUI(bool state) {
        canvas.gameObject.SetActive(state);
    }

    public void EndGame() {
        Debug.Log("End Game");
        UserStateManager.Instance.SaveState();
        Application.Quit();
    }

}
