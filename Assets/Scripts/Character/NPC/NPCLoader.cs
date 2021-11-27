using UnityEngine;

namespace Character.NPC {
    public class NPCLoader : MonoBehaviour {

        [SerializeField] NPC prefab;

        private void Start() {
            LoadNPCs();
        }

        void LoadNPCs() {
            foreach (NPCSetting setting in GameManager.Instance.Player.CharacterData.NPCs) {
                var npc = GameObject.Instantiate<NPC>(prefab);
                npc.transform.position = setting.position;
                npc.Init(setting.baseData.itemName);
            }
        }

    }
}