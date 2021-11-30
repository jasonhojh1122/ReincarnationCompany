using UnityEngine;

namespace Utils {
    public class Loader {

        static string characterDataPath = "CharacterData/";
        static string driftingItemDataPath = "DriftingItems/";
        static string dialogueDataPath = "DialogueData/";

        public static T Load<T>(string path) where T : UnityEngine.Object {
            return Resources.Load<T>(path);
        }

        public static Character.CharacterData LoadCharacterData(string name) {
            return Load<Character.CharacterData>(characterDataPath + name);
        }
        public static River.DriftingItemData LoadDriftingItemData(string name) {
            return Load<River.DriftingItemData>(driftingItemDataPath + name);
        }
        public static TextAsset LoadDialogueData(string name) {
            return Load<TextAsset>(dialogueDataPath + name);
        }

    }
}