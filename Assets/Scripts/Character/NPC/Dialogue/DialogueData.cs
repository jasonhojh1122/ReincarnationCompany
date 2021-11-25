
using UnityEngine;
using System.Collections.Generic;

namespace Dialogue {

    public enum DialoguePosition {
        LEFT,
        RIGHT
    }

    [CreateAssetMenu(fileName = "DialogueData", menuName = "ReincarnationCompany/DialogueData", order = 0)]
    public class DialogueData : ScriptableObject {
        public DialoguePosition position;
        public List<string> lines;
        public DialogueData next;
    }

}