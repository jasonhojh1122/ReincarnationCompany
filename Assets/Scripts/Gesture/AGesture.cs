using UnityEngine;
using System;

namespace Gesture {

public abstract class AGesture {

    public Action OnSatisfied;
    public Action OnFailed;
    protected GestureData gestureData;
    protected float gestureStartTime;
    protected string name;
    protected int targetCount, remainCount;
    protected Indicator indicator;

    public abstract void UpdateTouch(Touch touch);

    public void SetData(GestureData gestureData) {
        this.gestureData = gestureData;
    }

    public virtual void StartGesture(Indicator indicator) {
        targetCount = UnityEngine.Random.Range(gestureData.minCount, gestureData.maxCount+1);
        remainCount = targetCount;
        gestureStartTime = Time.time;

        this.indicator = indicator;
        indicator.Init(gestureData.name, remainCount, gestureData.indicateSprite);
    }

    public abstract bool IsFailed();
    public abstract bool IsSatisfied();

    public string GetName() {
        return name;
    }

}

}