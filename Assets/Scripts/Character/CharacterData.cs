
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
    [Range(0,10)]public int amount;
}

[CreateAssetMenu(fileName = "CharacterData", menuName = "ReincarnationCompany/CharacterData", order = 0)]
public class CharacterData : ScriptableObject {

    public BaseItemData baseData;

    public List<Rule> rules;

}

}