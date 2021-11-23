
using UnityEngine;

public class Sign : MonoBehaviour {

    [SerializeField] GameObject popup;

    private void Start() {
        popup.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.tag == "Player")
            popup.SetActive(true);
    }

    private void OnTriggerExit2D(Collider2D other) {
        if (other.gameObject.tag == "Player")
            popup.SetActive(false);
    }

}