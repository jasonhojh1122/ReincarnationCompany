
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider2D))]
public class ActivateZone : MonoBehaviour {

    [SerializeField] protected string targetName;
    [SerializeField] protected string targetTag;
    public UnityEvent onEnter;
    public UnityEvent onExit;

    protected void OnTriggerEnter2D(Collider2D other) {
        if ( MatchTarget(other.gameObject) ) {
            onEnter.Invoke();
        }
    }

    protected void OnTriggerExit2D(Collider2D other) {
        if (MatchTarget(other.gameObject)) {
            onExit.Invoke();
        }
    }

    protected bool MatchTarget(GameObject go) {
        if ( (targetName != null && targetName != "" && go.gameObject.name != targetName) ||
             (targetTag != null && targetTag != "" && go.gameObject.tag != targetTag) ) {
            return false;
        }
        else {
            return true;
        }
    }

}