using UnityEngine;

public class RiverEnd : MonoBehaviour {




    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.name == "RiverEnd") {
            Destroy(gameObject);
        }
    }
}