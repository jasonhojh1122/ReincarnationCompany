using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    [SerializeField] private Camera mainCamera;
    [SerializeField] UIManager uiManager;
    private AsyncOperation async = null;

    private void Start() {
        uiManager = GetComponent<UIManager>();
    }

    public void SetCameraPos(float xPos) {
        Vector3 newPos = mainCamera.transform.position;
        newPos.x = xPos;
        mainCamera.transform.position = newPos;
    }

    public void LoadScene(string name) {
        StartCoroutine(LoadGameScene(name));
    }

    private IEnumerator LoadGameScene(string name) {
        async = SceneManager.LoadSceneAsync(name, LoadSceneMode.Additive);
        while (!async.isDone) {
            yield return null;
        }
        OnGameSceneLoaded(name);
    }

    private void OnGameSceneLoaded(string name) {
        SetCameraPos(0);
        uiManager.ActivateNewUI(name+"UI");
    }

}
