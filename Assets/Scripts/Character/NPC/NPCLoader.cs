using UnityEngine;

namespace Character.NPC {
    public class NPCLoader : MonoBehaviour {

        [SerializeField] NPC prefab;

        private void Start() {
            StartCoroutine(LoadNPCs());
        }

        System.Collections.IEnumerator LoadNPCs() {
            yield return new WaitForEndOfFrame();
            foreach (NPCSetting setting in GameManager.Instance.ActivePlayer.CharacterData.NPCs) {
                var npc = GameObject.Instantiate<NPC>(prefab);
                npc.transform.position = setting.position;
                npc.Init(GameManager.Instance.ActivePlayer.CharacterData.baseData.itemName, setting.baseData.itemName);
            }
        }

    }
}