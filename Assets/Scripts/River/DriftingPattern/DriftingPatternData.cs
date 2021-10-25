using UnityEngine;

namespace River {

[CreateAssetMenu(fileName = "DriftingPatternData", menuName = "ReincarnationCompany/DriftingPatternData", order = 0)]
public class DriftingPatternData : ScriptableObject {

    public string patternName;
    public float minY;
    public float maxY;
    public float minSpeed;
    public float maxSpeed;

}

}