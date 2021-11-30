using UnityEngine;

public class StartUI : MonoBehaviour {

    public void StartGame() {
        if (UserStateManager.Instance.IsNewGame) {
            GameManager.Instance.LoadSceneAndClose("_Select");
        }
        else {
            GameManager.Instance.LoadSceneAndClose("_Main");
        }
    }


}