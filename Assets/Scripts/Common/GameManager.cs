using System.Collections;
using System.Linq;
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

    [SerializeField] Canvas baseCanvas;
    [SerializeField] BaseUI baseUI;
    [SerializeField] JoyStick joyStick;
    [SerializeField] public TextMeshProUGUI debugText;

    [SerializeField] UnityEngine.Rendering.Universal.ForwardRendererData rendererData;
    [SerializeField] Material switchSceneMat;
    [SerializeField] float switchDur;
    [SerializeField] float holdDur;
    [SerializeField] AudioSource bgmSource;
    SwitchSceneRendererFeature ssRendererFeature;

    float pastTime;
    private AsyncOperation async = null;
    private string activeSceneName;
    Stack<SceneSetting> sceneSettings;
    Stack<Scene> scenes;
    public Character.Player ActivePlayer {
        get => SceneSetting.activeSceneSetting.player;
    }
    public Stack<SceneSetting> SceneSettings {
        get => sceneSettings;
    }

    private void Awake() {
        _instance = this;
        sceneSettings = new Stack<SceneSetting>();
        scenes = new Stack<Scene>();
        ssRendererFeature = rendererData.rendererFeatures.OfType<SwitchSceneRendererFeature>().FirstOrDefault();
        StartCoroutine(LoadStart());
    }

    public void LoadSceneAndClose(string name) {
        StartCoroutine(LoadGameSceneAndCloseAnim(name));
    }

    public void LoadScene(string name) {
        StartCoroutine(LoadGameSceneAnim(name));
    }

    public void UnloadScene() {
        UserStateManager.Instance.SaveState();
        StartCoroutine(UnloadSceneAnim());
    }

    void UnloadSceneFromAdditive() {
        sceneSettings.Pop();
        scenes.Pop();
        SceneManager.UnloadSceneAsync(activeSceneName);
    }

    private IEnumerator UnloadSceneAnim() {
        yield return StartCoroutine(ToggleMask(true));
        UnloadSceneFromAdditive();
        Gesture.GestureManager.Instance.ClearQueue();
        if (scenes.Count > 0)
            OnSceneChange();
        yield return new WaitForSeconds(holdDur);
        yield return StartCoroutine(ToggleMask(false));
    }

    private IEnumerator LoadGameSceneAnim(string sceneName) {
        pastTime = 0.0f;
        Gesture.GestureManager.Instance.ClearQueue();
        yield return StartCoroutine(ToggleMask(true));
        yield return StartCoroutine(LoadGameScene(sceneName));
        yield return new WaitForSeconds(Mathf.Clamp(holdDur - pastTime, 0.0f, holdDur));
        yield return StartCoroutine(ToggleMask(false));
        OnSceneChange();
    }

    private IEnumerator LoadGameSceneAndCloseAnim(string sceneName) {
        pastTime = 0.0f;
        yield return StartCoroutine(ToggleMask(true));
        UnloadSceneFromAdditive();
        Gesture.GestureManager.Instance.ClearQueue();
        yield return StartCoroutine(LoadGameScene(sceneName));
        yield return new WaitForSeconds(Mathf.Clamp(holdDur - pastTime, 0.0f, holdDur));
        yield return StartCoroutine(ToggleMask(false));
        OnSceneChange();
    }

    private IEnumerator LoadStart() {
        switchSceneMat.SetFloat("_OpenHeight", -0.1f);
        yield return StartCoroutine(LoadGameScene("_Start"));
        OnSceneChange();
        switchSceneMat.SetFloat("_OpenHeight", 1.0f);
    }

    private IEnumerator LoadGameScene(string sceneName) {
        UserStateManager.Instance.SaveState();
        async = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
        while (!async.isDone) {
            pastTime += Time.deltaTime;
            yield return null;
        }
        activeSceneName = sceneName;
        scenes.Push(SceneManager.GetSceneByName(activeSceneName));
        sceneSettings.Push(SceneSetting.activeSceneSetting);
        if (sceneSettings.Peek().videoPlayer != null)
        {
            sceneSettings.Peek().videoPlayer.Prepare();
            while (!sceneSettings.Peek().videoPlayer.isPrepared)
            {
                Debug.Log("Not Prepared.");
                pastTime += Time.deltaTime;
                yield return null;
            }
            sceneSettings.Peek().videoPlayer.Play();
        }
    }

    private void OnSceneChange() {
        SceneManager.SetActiveScene(scenes.Peek());
        Time.timeScale = 1.0f;
        // Gesture.GestureManager.Instance.ClearQueue();
        baseUI.Set(sceneSettings.Peek());
        if (sceneSettings.Peek().player != null)
            joyStick.Target = sceneSettings.Peek().player.MovingTarget;
        if (sceneSettings.Peek().bgm != null) {
            AudioManager.Instance.PlayBGM(sceneSettings.Peek().bgm);
        }
        else {
            AudioManager.Instance.StopBGM();
        }
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

    IEnumerator ToggleMask(bool turnOn)
    {
        float t = 0.0f;
        float start, target;
        if (turnOn)
        {
            ssRendererFeature.settings.IsEnabled = true;
            rendererData.SetDirty();
            start = 1.0f;
            target = -0.1f;
        }
        else
        {
            start = -0.1f;
            target = 1.0f;
        }
        while (t < switchDur)
        {
            t += Time.deltaTime;
            pastTime += Time.deltaTime;
            float p = t / switchDur;
            switchSceneMat.SetFloat("_OpenHeight", Mathf.Lerp(start, target, p));
            yield return null;
        }
        switchSceneMat.SetFloat("_OpenHeight", target);
        if (!turnOn)
        {
            ssRendererFeature.settings.IsEnabled = false;
            rendererData.SetDirty();
        }
    }

    void OnDestroy() {
        Debug.Log("Destroyed");
    }


}
