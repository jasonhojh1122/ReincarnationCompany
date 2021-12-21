
using UnityEngine;
using System.IO;
using System.Collections.Generic;

public sealed class UserStateManager {

    private static UserStateManager _Instance = null;
    public static UserStateManager Instance {
        get {
            if (_Instance == null) {
                _Instance = new UserStateManager();
            }
            return _Instance;
        }
    }

    UserState state;
    string dest;

    public BackpackData Backpack {
        get => state.backpack;
    }

    public Dictionary<string, SerializableHashSet<string>> FinishedConversation {
        get => state.finishedConversation.ToDictionary();
    }

    public string CurCharacter {
        get => state.curCharacter;
        set => state.curCharacter = value;
    }

    public string NewCharacter {
        get => state.newCharacter;
        set => state.newCharacter = value;
    }

    public int Money {
        get => state.money;
        set {
            if (value < 0)
                state.money = 0;
            else
                state.money = value;
        }
    }

    public bool IsNewGame {
        get => state.isNewGame;
        set => state.isNewGame = value;
    }

    private UserStateManager() {
        dest = Application.persistentDataPath + "/save.dat";
        LoadState();
        // LogState();
    }

    public string json;

    /* void Awake() {
        dest = Application.persistentDataPath + "/save.dat";
        LoadState();

        state.backpack.AddItemToBackpack("ABC", 5);
        state.backpack.AddItemToBackpack("DEF", 1);
        state.usedCharacter.ToHashSet().Add("111222");
        state.money = 10;

        SaveState();
    } */

    void LoadState() {
        if (File.Exists(dest)) {
            json = File.ReadAllText(dest);
            state = JsonUtility.FromJson<UserState>(json);
        }
        else {
            state = new UserState();
            SaveState();
        }
    }

    public void SaveState() {
        json = JsonUtility.ToJson(state);
        File.WriteAllText(dest, json);
    }

    public void ResetState() {
        state = new UserState();
        SaveState();
    }

    public void LogState() {
        Debug.Log(CurCharacter);

        string log = "";
        foreach (KeyValuePair<string, SerializableHashSet<string>> p in FinishedConversation) {
            log += p.Key + ":\n";
            foreach (string s in p.Value.ToHashSet()) {
                log += s + ", ";
            }
            log += "\n";
        }
        Debug.Log("Used Character: \n" + log);

        log = "";
        Dictionary<string, int> backpackDict = Backpack.backpackDict;
        foreach (KeyValuePair<string ,int> pair in backpackDict) {
            log += pair.Key + ":" + pair.Value.ToString() + ", ";
        }
        Debug.Log("Backpack: " + log);
        Debug.Log("Money: " + Money.ToString());
    }

}