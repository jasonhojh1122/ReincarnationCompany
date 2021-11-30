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

    [SerializeField]
    public int money;

    [SerializeField]
    public bool isNewGame;

    public static string DefaultCharacterName = "無名人類";

    public UserState(string curCharacter, SerializableHashSet<string> usedCharacter,
            BackpackData backpack, int money) {
        this.curCharacter = curCharacter;
        this.usedCharacter = usedCharacter;
        this.backpack = backpack;
        this.money = money;
    }

    public UserState() {
        curCharacter = DefaultCharacterName;
        usedCharacter = new SerializableHashSet<string>( new HashSet<string>() );
        usedCharacter.ToHashSet().Add(DefaultCharacterName);
        backpack = new BackpackData();
        money = 0;
        isNewGame = true;
    }

}