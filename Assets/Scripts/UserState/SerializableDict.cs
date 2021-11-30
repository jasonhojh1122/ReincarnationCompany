
using UnityEngine;
using System;
using System.Collections.Generic;

// Dictionary<TKey, TValue>
[Serializable]
public class SerializableDict<TKey, TValue> : ISerializationCallbackReceiver{

    [SerializeField]
    List<TKey> keys;

    [SerializeField]
    List<TValue> values;

    Dictionary<TKey, TValue> target;

    public Dictionary<TKey, TValue> ToDictionary() { return target; }

    public SerializableDict(Dictionary<TKey, TValue> target) {
        this.target = target;
    }

    public void OnBeforeSerialize() {
        keys = new List<TKey>(target.Keys);
        values = new List<TValue>(target.Values);
    }

    public void OnAfterDeserialize() {
        var count = Math.Min(keys.Count, values.Count);
        target = new Dictionary<TKey, TValue>(count);
        for (var i = 0; i < count; ++i) {
            target.Add(keys[i], values[i]);
        }
    }

    public void Clear() {
        values.Clear();
        target.Clear();
    }

}

