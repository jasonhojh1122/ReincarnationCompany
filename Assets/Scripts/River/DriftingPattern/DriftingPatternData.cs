using UnityEngine;

namespace River {

[CreateAssetMenu(fileName = "DriftingPatternData", menuName = "ReincarnationCompany/DriftingPatternData", order = 0)]
public class DriftingPatternData : ScriptableObject {

    public string className;
    public float minZ;
    public float maxZ;
    public float minSpeed;
    public float maxSpeed;
    [Range(0, 10)] public int priority;

}

}