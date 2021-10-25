using UnityEngine;
using System;
using System.Collections.Generic;

[Serializable]
public class UserState {

    [SerializeField]
    public string curCharacter;

    [SerializeField]
    public SerializableHashSet<string> usedCharacter;

    [SerializeField]
    public BackpackData backpack;
    //public SerializableDict<string, int> backpack;

    [SerializeField]
    public int money;

    public UserState(string curCharacter, SerializableHashSet<string> usedCharacter,
            BackpackData backpack, int money) {
        this.curCharacter = curCharacter;
        this.usedCharacter = usedCharacter;
        this.backpack = backpack;
        this.money = money;
    }

    public UserState() {
        curCharacter = "DefaultCharacter";
        money = 0;
        usedCharacter = new SerializableHashSet<string>( new HashSet<string>() );
        backpack = new BackpackData();
    }

}