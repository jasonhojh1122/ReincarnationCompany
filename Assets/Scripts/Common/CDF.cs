using UnityEngine;
using System;
using System.Collections.Generic;

[Serializable]
public class CDF {
    [Range(1, 10)] public List<int> priority;
    List<float> cdf;

    public void CalculateCDF() {
        cdf = new List<float>();
        for (int i = 0; i < priority.Count; i++) {
            if (i == 0) {
                cdf.Add(priority[i]);
            }
            else {
                cdf.Add(priority[i] + cdf[i-1]);
            }
        }
        for (int i = 0; i < cdf.Count; i++) {
            cdf[i] /= cdf[cdf.Count-1];
        }
    }

    public int GetRandomID() {
        float rd = UnityEngine.Random.Range(0f, 1f);
        int id = cdf.BinarySearch(rd);
        if (id < 0) {
            id = ~id;
        }
        return id;
    }
}
