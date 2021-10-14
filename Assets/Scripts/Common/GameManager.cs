using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    [SerializeField] private Camera mainCamera;
    [SerializeField] GameObject unpersistentUI;
    private AsyncOperation async = null;

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
            Debug.Log(async.progress);
            yield return null;
        }
        OnGameSceneLoaded();
    }

    private void OnGameSceneLoaded() {
        SetCameraPos(0);
        unpersistentUI.SetActive(false);
    }

}
