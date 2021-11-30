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

    [SerializeField] Material switchSceneMat;
    [SerializeField] Canvas baseCanvas;
    [SerializeField] BaseUI baseUI;
    [SerializeField] JoyStick joyStick;
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
        if (UserStateManager.Instance.IsNewGame) {
            StartCoroutine(LoadGameScene("_Start", false));
        }
        else {
            StartCoroutine(LoadGameScene("_Main", false));
        }
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
        UserStateManager.Instance.SaveState();
        UserStateManager.Instance.LogState();
        SceneManager.UnloadSceneAsync(activeSceneName);
        SceneManager.SetActiveScene(scenes.Peek());
        Gesture.GestureManager.Instance.ClearQueue();
        Time.timeScale = 1.0f;
        baseUI.Set(sceneSettings.Peek());
        joyStick.Target = sceneSettings.Peek().player.MovingTarget;
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
        SceneManager.SetActiveScene(scenes.Peek());
        sceneSettings.Push(SceneSetting.activeSceneSetting);
        baseUI.Set(sceneSettings.Peek());
        joyStick.Target = sceneSettings.Peek().player.MovingTarget;
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
