
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

    public HashSet<string> UsedCharacter {
        get => state.usedCharacter.ToHashSet();
    }

    public string CurCharacter {
        get => state.curCharacter;
        set => state.curCharacter = value;
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

    private UserStateManager() {
        dest = Application.persistentDataPath + "/save.dat";
        LoadState();
        LogState();
    }

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
            string json = File.ReadAllText(dest);
            state = JsonUtility.FromJson<UserState>(json);
        }
        else {
            state = new UserState();
        }
    }

    public void SaveState() {
        string json = JsonUtility.ToJson(state);
        File.WriteAllText(dest, json);
    }

    public void LogState() {
        Debug.Log(CurCharacter);

        string log = "";
        HashSet<string> usedCharacter = UsedCharacter;
        foreach (string s in usedCharacter) {
            log += s;
            log += ", ";
        }
        Debug.Log("Used Character: " + log);

        log = "";
        Dictionary<string, int> backpackDict = Backpack.backpackDict;
        foreach (KeyValuePair<string ,int> pair in backpackDict) {
            log += pair.Key + ":" + pair.Value.ToString() + ", ";
        }
        Debug.Log("Backpack: " + log);
        Debug.Log("Money: " + Money.ToString());
    }

}