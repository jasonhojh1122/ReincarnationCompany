using System.Collections.Generic;

namespace Gesture {
public class GesturePool {

    Dictionary<string, System.Type> pool;

    public GesturePool() {
        pool = new Dictionary<string, System.Type>();
    }

    public System.Type GetType(GestureData data) {
        System.Type gestureType;
        if (pool.ContainsKey(data.gestureName)) {
            gestureType = pool[data.gestureName];
        }
        else {
            gestureType = System.Type.GetType(typeof(AGesture).Namespace + '.' + data.gestureName);
            pool.Add(data.gestureName, gestureType);
        }
        return gestureType;
    }

    public AGesture InstantiateGesture(GestureData data) {
        System.Type gestureType = GetType(data);
        AGesture gesture = (AGesture)System.Activator.CreateInstance(gestureType);
        gesture.GestureData = data;
        return gesture;
    }
}

}