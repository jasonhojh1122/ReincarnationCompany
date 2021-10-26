using UnityEngine;
using System.Collections.Generic;

public class BoatLife : MonoBehaviour {

    [SerializeField] List<UnityEngine.UI.Image> lifeImages;
    [SerializeField] Color32 tint;
    int life;

    private void Awake() {
        life = lifeImages.Count;
    }

    public void AddToLife(int amount) {
        if (amount < 0) {
            for (int i = 0; i < amount && life > 0; i++) {
                lifeImages[life].color = tint;
                life--;
            }
        }
        else {
            for (int i = 0; i < amount && life < lifeImages.Count; i++) {
                lifeImages[life].color = Color.white;
                life++;
            }
        }
    }

    public bool IsAlive() {
        return life > 0;
    }

}