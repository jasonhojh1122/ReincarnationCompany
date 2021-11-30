using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    public static GameManager Instance {
        get => _instance;
    }

    [SerializeField] Material switchSceneMat;
    [SerializeField] Canvas baseCanvas;
    [SerializeField] BaseUI baseUI;
    [SerializeField] JoyStick joyStick;
    [SerializeField] public TextMeshProUGUI debugText;
    private AsyncOperation async = null;
    private string activeSceneName;
    Stack<SceneSetting> sceneSettings;
    Stack<Scene> scenes;
    public Character.Player ActivePlayer {
        get => SceneSetting.activeSceneSetting.player;
    }

    private void Awake() {
        _instance = this;
        sceneSettings = new Stack<SceneSetting>();
        scenes = new Stack<Scene>();
        // debugText.text = UserStateManager.Instance.json;
        LoadScene("_Start");
    }

    public void LoadSceneAndClose(string name) {
        StartCoroutine(LoadGameScene(name, true));
    }

    public void LoadScene(string name) {
        StartCoroutine(LoadGameScene(name, false));
    }

    public void UnloadScene() {
        sceneSettings.Pop();
        scenes.Pop();
        UserStateManager.Instance.LogState();
        SceneManager.UnloadSceneAsync(activeSceneName);
        if (scenes.Count > 0) {
            SceneManager.SetActiveScene(scenes.Peek());
            OnSceneChange();
        }
    }

    private IEnumerator LoadGameScene(string sceneName, bool closeOld) {
        if (closeOld) {
            UnloadScene();
        }
        UserStateManager.Instance.SaveState();
        async = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
        while (!async.isDone) {
            yield return null;
        }
        activeSceneName = sceneName;
        scenes.Push(SceneManager.GetSceneByName(activeSceneName));
        sceneSettings.Push(SceneSetting.activeSceneSetting);
        SceneManager.SetActiveScene(scenes.Peek());
        OnSceneChange();
    }

    private void OnSceneChange() {
        Time.timeScale = 1.0f;
        Gesture.GestureManager.Instance.ClearQueue();
        baseUI.Set(sceneSettings.Peek());
        if (sceneSettings.Peek().player != null)
            joyStick.Target = sceneSettings.Peek().player.MovingTarget;
        // debugText.text = "Active Scene : " + activeSceneName;
        Debug.Log("Active Scene : " + activeSceneName);
    }

    public void ToggleUI(bool state) {
        baseCanvas.gameObject.SetActive(state);
    }

    public void EndGame() {
        Debug.Log("End Game");
        UserStateManager.Instance.SaveState();
        Application.Quit();
    }

}
