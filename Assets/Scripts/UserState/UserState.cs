using UnityEngine;
using System;
using System.Collections.Generic;

[Serializable]
public class UserState {

    [SerializeField]
    public string curCharacter;

    [SerializeField]
    public string newCharacter;

    [SerializeField]
    public SerializableDict<string, SerializableHashSet<string>> finishedConversation;

    [SerializeField]
    public BackpackData backpack;

    [SerializeField]
    public int money;

    [SerializeField]
    public bool isNewGame;

    public static string DefaultCharacterName = "無名人類";

    public UserState(string curCharacter, SerializableDict<string, SerializableHashSet<string>> finishedConversation,
            BackpackData backpack, int money, bool isNewGame) {
        this.curCharacter = curCharacter;
        this.finishedConversation = finishedConversation;
        this.backpack = backpack;
        this.money = money;
        this.isNewGame = isNewGame;
    }

    public UserState() {
        curCharacter = DefaultCharacterName;
        finishedConversation = new SerializableDict<string, SerializableHashSet<string>>(new Dictionary<string, SerializableHashSet<string>>());
        finishedConversation.ToDictionary().Add(DefaultCharacterName, new SerializableHashSet<string>(new HashSet<string>()));
        backpack = new BackpackData();
        money = 0;
        isNewGame = true;
    }

}