using System.Collections.Generic;

namespace River {
public class DriftingPatternPool {

    Dictionary<string, System.Type> pool;

    public DriftingPatternPool() {
        pool = new Dictionary<string, System.Type>();
    }

    public System.Type GetType(DriftingPatternData data) {
        System.Type patternType;
        if (pool.ContainsKey(data.patternName)) {
            patternType = pool[data.patternName];
        }
        else {
            patternType = System.Type.GetType(typeof(ADriftingPattern).Namespace + '.' + data.patternName);
            pool.Add(data.patternName, patternType);
        }
        return patternType;
    }

    public ADriftingPattern InstantiateDriftingPattern(DriftingPatternData data) {
        System.Type patternType = GetType(data);
        ADriftingPattern pattern = (ADriftingPattern)System.Activator.CreateInstance(patternType);
        return pattern;
    }


}

}