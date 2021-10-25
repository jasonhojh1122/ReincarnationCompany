
using UnityEngine;
using System.IO;
using System.Collections.Generic;

public class UserStateManager : MonoBehaviour {

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

    void Awake() {
        dest = Application.persistentDataPath + "/save.dat";
        LoadState();

        state.backpack.AddItemToBackpack("ABC", 5);
        state.backpack.AddItemToBackpack("DEF", 1);
        state.usedCharacter.ToHashSet().Add("111222");
        state.money = 10;

        SaveState();
    }

    public void LoadState() {
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

    public void AddToBackpack(string name, int amount) {

    }

    void LogState() {
        Debug.Log(state.curCharacter);

        string log = "";
        HashSet<string> usedCharacter = state.usedCharacter.ToHashSet();
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
        Debug.Log(state.money.ToString());
    }

}