using UnityEngine;
using System.Collections;

namespace River {

public class DriftingItemGenerator : MonoBehaviour, IGenerator {

    [SerializeField] MonsterGenerator monsterGenerator;
    [SerializeField] float minInterval, maxInterval;
    [SerializeField] float minZ, maxZ;

    bool toGenerate;

    void Start() {
        toGenerate = true;
        StartCoroutine(KeepGenerate());
    }

    IEnumerator KeepGenerate() {
        while (true) {
            if (toGenerate) {
                Generate();
            }
            yield return Wait(UnityEngine.Random.Range(minInterval, maxInterval));
        }
    }

    IEnumerator Wait(float sec) {
        yield return new WaitForSeconds(sec);
    }

    public DriftingItem Generate() {
        DriftingItem di = monsterGenerator.Generate();
        Vector3 pos = new Vector3(transform.position.x, 0, UnityEngine.Random.Range(minZ, maxZ));
        di.transform.position = pos;
        di.transform.rotation = Quaternion.Euler(45, 0, 0);
        return di;
    }

}

}