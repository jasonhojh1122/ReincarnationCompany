using UnityEngine;

public class SceneLoader : MonoBehaviour {

    public void LoadScene(string sceneName) {
        GameManager.Instance.LoadScene(sceneName);
    }

    public void LoadSceneAndClose(string sceneName) {
        GameManager.Instance.LoadSceneAndClose(sceneName);
    }

}