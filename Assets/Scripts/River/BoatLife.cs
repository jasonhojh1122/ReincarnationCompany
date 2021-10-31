using UnityEngine;
using System.Collections.Generic;

public class BoatLife : MonoBehaviour {

    [SerializeField] List<UnityEngine.UI.Image> lifeImages;
    [SerializeField] Color32 tint;
    [SerializeField] int life;

    private void Awake() {
        life = lifeImages.Count;
    }

    public void AddToLife(int amount) {
        if (amount < 0) {
            amount = -amount;
            for (int i = 0; i < amount && life > 0; i++) {
                lifeImages[life-1].color = tint;
                life--;
            }
        }
        else {
            for (int i = 0; i < amount && life < lifeImages.Count; i++) {
                lifeImages[life-1].color = Color.white;
                life++;
            }
        }
    }

    public bool IsAlive() {
        return life > 0;
    }

}