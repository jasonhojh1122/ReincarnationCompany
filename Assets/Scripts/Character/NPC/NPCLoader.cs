using UnityEngine;

namespace Character {
    public class NPCLoader : MonoBehaviour {

        [SerializeField] GameManager gameManager;
        [SerializeField] NPC prefab;

        private void Start() {
            LoadNPCs();
        }

        void LoadNPCs() {
            foreach (NPCSetting setting in gameManager.Player.CharacterData.NPCs) {
                var npc = GameObject.Instantiate<NPC>(prefab);
                npc.transform.position = setting.position;
                npc.Init(setting.baseData.itemName);
            }
        }

    }
}