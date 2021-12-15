
using UnityEngine;

namespace Gesture {


[CreateAssetMenu(fileName = "GestureData", menuName = "ReincarnationCompany/GestureData", order = 0)]
public class GestureData : ScriptableObject {

    public string gestureName;
    public int minCount;
    public int maxCount;

    [Header("Time setting in seconds")]
    public float singleDuration;

}

}