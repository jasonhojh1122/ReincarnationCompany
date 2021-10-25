using System;
using UnityEngine;
using System.Collections.Generic;

[Serializable]
public class BackpackData {

    [SerializeField]
    SerializableDict<string, int> _backpack;

    public Dictionary<string, int> backpackDict {
        get => _backpack.ToDictionary();
    }

    public BackpackData() {
        _backpack = new SerializableDict<string, int>( new Dictionary<string, int>() );
    }

    public BackpackData(SerializableDict<string, int> backpack) {
        this._backpack = backpack;
    }

    public void AddItemToBackpack(string itemName, int amount) {
        if (backpackDict.ContainsKey(itemName)) {
            backpackDict[itemName] += amount;
        }
        else {
            backpackDict.Add(itemName, amount);
        }
    }

    public int GetItemNum(string itemName) {
        if (backpackDict.ContainsKey(itemName)) {
            return backpackDict[itemName];
        }
        else {
            return 0;
        }
    }

}