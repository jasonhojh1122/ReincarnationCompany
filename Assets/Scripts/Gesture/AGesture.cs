using UnityEngine;
using UnityEngine.Events;
using System;

namespace Gesture {

public abstract class AGesture {

    public UnityEvent OnStart;
    public UnityEvent OnSatisfied;
    public UnityEvent OnSingleSatisfied;
    public UnityEvent OnReset;
    public UnityEvent OnFailed;

    protected GestureData gestureData;
    protected float gestureStartTime;
    protected string name;
    protected int targetCount, remainCount;
    protected Indicator indicator;

    public Indicator Indicator {
        get => indicator;
        set {
            indicator = value;
            indicator.Init(gestureData.name, remainCount, gestureData.indicateSprite);
        }
    }

    public GestureData GestureData {
        get => gestureData;
        set {
            gestureData = value;
            TargetCount = UnityEngine.Random.Range(gestureData.minCount, gestureData.maxCount+1);
        }
    }

    public int TargetCount {
        get => targetCount;
        set {
            targetCount = value;
            remainCount = targetCount;
        }
    }

    public int RemainCount {
        get => remainCount;
    }

    public AGesture() {
        OnStart = new UnityEvent();
        OnSatisfied = new UnityEvent();
        OnSingleSatisfied = new UnityEvent();
        OnReset = new UnityEvent();
        OnFailed = new UnityEvent();
    }

    public abstract void UpdateTouch(Touch touch);
    public abstract bool IsFailed();
    public abstract bool IsSatisfied();
    public virtual void ResetGesture() {
        OnReset.Invoke();
        remainCount = targetCount;
        gestureStartTime = Time.timeSinceLevelLoad;
    }

    public virtual void StartGesture() {
        OnStart.Invoke();
        gestureStartTime = Time.timeSinceLevelLoad;
    }

    public string GetName() {
        return name;
    }

}

}