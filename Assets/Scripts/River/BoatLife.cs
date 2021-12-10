using UnityEngine;
using System.Collections.Generic;

namespace River {
    public class BoatLife : MonoBehaviour {

        [SerializeField] UnityEngine.UI.Image lifeImagePrefab;
        [SerializeField] Color32 tint;
        [SerializeField] int life;
        List<UnityEngine.UI.Image> lifeImages;

        private void Start() {
            life = GameManager.Instance.ActivePlayer.CharacterData.life;
            Debug.Log("Character Life: " + life.ToString());
            lifeImages = new List<UnityEngine.UI.Image>();
            for (int i = 0; i < life; i++)
            {
                lifeImages.Add(GameObject.Instantiate<UnityEngine.UI.Image>(lifeImagePrefab));
                lifeImages[i].transform.SetParent(this.transform, false);
            }
            /* for (int i = 0; i < life; i++)
            {
                lifeImages[i].transform.position = new Vector3(lifeImages[i].transform.position.x, lifeImages[i].transform.position.y, 0);
                lifeImages[i].transform.localScale = Vector3.one;
            } */
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
}