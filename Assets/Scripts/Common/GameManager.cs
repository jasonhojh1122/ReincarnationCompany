using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    public static GameManager Instance {
        get => _instance;
    }

    [SerializeField] private Camera mainCamera;
    [SerializeField] Canvas canvas;
    [SerializeField] List<CanvasGroupFader> faders;
    [SerializeField] GameObject vCam;
    [SerializeField] Character.Player player;
    private AsyncOperation async = null;
    private string additiveScene;

    public Character.Player Player {
        get => player;
    }

    private void Awake() {
        _instance = this;
    }

    public void SetCameraPos(float yPos) {
        Vector3 newPos = mainCamera.transform.position;
        newPos.x = yPos;
        mainCamera.transform.position = newPos;
    }

    public void LoadScene(string name) {
        StartCoroutine(LoadGameScene(name));
    }

    public void UnloadScene() {
        FadeInUI();
        UserStateManager.Instance.SaveState();
        UserStateManager.Instance.LogState();
        SceneManager.UnloadSceneAsync(additiveScene);
        SceneManager.SetActiveScene(SceneManager.GetSceneByName("_Main"));
        Gesture.GestureManager.Instance.ClearQueue();
        Time.timeScale = 1.0f;
        vCam.SetActive(true);
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
        vCam.SetActive(false);
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
