using UnityEngine;

namespace River {

[CreateAssetMenu(fileName = "DriftingPatternData", menuName = "ReincarnationCompany/DriftingPatternData", order = 0)]
public class DriftingPatternData : ScriptableObject {

    public string patternName;
    public float minZ;
    public float maxZ;
    public float minSpeed;
    public float maxSpeed;

}

}