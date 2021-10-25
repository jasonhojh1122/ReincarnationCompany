using UnityEngine;
using System;
using System.Linq;
using System.Collections.Generic;

[Serializable]
public class SerializableHashSet<TKey> : ISerializationCallbackReceiver {

    [SerializeField]
    List<TKey> keys;

    HashSet<TKey> target;

    public HashSet<TKey> ToHashSet() { return target; }

    public SerializableHashSet(HashSet<TKey> target) {
        this.target = target;
    }

    public void OnBeforeSerialize() {
        keys = target.ToList();
    }

    public void OnAfterDeserialize() {
        target = new HashSet<TKey>();
        for (var i = 0; i < keys.Count; ++i) {
            target.Add(keys[i]);
        }
    }

}
