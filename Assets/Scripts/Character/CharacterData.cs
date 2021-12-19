
using UnityEngine;
using System;
using System.Collections.Generic;

namespace Character {

    public enum RuleState {
        INCLUDE,
        EXCLUDE
    }

    [Serializable]
    public struct Rule {
        public RuleState ruleState;
        public BaseItemData itemData;
        [Range(0,10)] public int minAmount;
        [Range(0,10)] public int maxAmount;
    }

    [Serializable]
    public struct NPCSetting {
        public BaseItemData baseData;
        public Vector3 position;
    }

    [CreateAssetMenu(fileName = "CharacterData", menuName = "ReincarnationCompany/CharacterData", order = 0)]
    public class CharacterData : ScriptableObject {

        public BaseItemData baseData;
        public List<Rule> rules;
        public bool withStory = false;
        public Sprite artWords;
        public List<NPCSetting> NPCs;
        public int life = 3;
        public float speed = 12;
        public int defaultMoney = 500;

    }

}