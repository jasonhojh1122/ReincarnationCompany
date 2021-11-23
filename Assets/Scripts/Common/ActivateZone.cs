
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider2D))]
public class ActivateZone : MonoBehaviour {

    [SerializeField] string targetName;
    [SerializeField] string targetTag;
    public UnityEvent onEnter;
    public UnityEvent onExit;

    private void OnTriggerEnter2D(Collider2D other) {
        if ( MatchTarget(other.gameObject) ) {
            onEnter.Invoke();
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if (MatchTarget(other.gameObject)) {
            onExit.Invoke();
        }
    }

    bool MatchTarget(GameObject go) {
        if ( (targetName != null && go.gameObject.name != targetName) ||
             (targetTag != null && go.gameObject.tag != targetTag) ) {
            return false;
        }
        else {
            return true;
        }
    }

}