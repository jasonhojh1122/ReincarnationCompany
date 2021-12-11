using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndingBox : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "Player")
        {
            UserStateManager.Instance.IsNewGame = true;
            GameManager.Instance.EndGame();
        }
    }
}
