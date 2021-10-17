using UnityEngine;
using TMPro;
using System.Collections;

namespace Gesture {

public class Indicator : MonoBehaviour {
    [SerializeField] SpriteRenderer sr;
    [SerializeField] TextMeshPro gestureName;
    [SerializeField] TextMeshPro count;
    [SerializeField] float enlargeFactor;
    [SerializeField] float enlargeTime;

    Vector3 enlargeScale;

    private void Start() {
        enlargeScale = new Vector3(enlargeFactor, enlargeFactor, enlargeFactor);
    }

    public void Init(string name, int count, Sprite sprite) {
        SetGestureName(name);
        SetCount(count);
        sr.sprite = sprite;
        transform.localScale = Vector3.one;
    }

    public void SetGestureName(string name) {
        gestureName.text = name;
    }

    public void SetCount(int i) {
        count.text = "X" + i.ToString();
    }

    public void UpdateCount(int i) {
        SetCount(i);
        StartCoroutine(Enlarge());
    }

    IEnumerator Enlarge() {
        transform.localScale = enlargeScale;
        yield return new WaitForSeconds(enlargeTime);
        transform.localScale = Vector3.one;
    }

}

}