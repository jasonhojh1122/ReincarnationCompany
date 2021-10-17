using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    [SerializeField] private Camera mainCamera;
    [SerializeField] List<GameObject> unpersistentGameobjects;
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
            yield return null;
        }
        OnGameSceneLoaded();
    }

    private void OnGameSceneLoaded() {
        SetCameraPos(0);
        foreach (GameObject go in unpersistentGameobjects) {
            go.SetActive(false);
        }
    }

}
