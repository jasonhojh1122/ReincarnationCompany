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
    protected float singleDuration;

    public GestureData GestureData {
        get => gestureData;
        set {
            gestureData = value;
            TargetCount = UnityEngine.Random.Range(gestureData.minCount, gestureData.maxCount+1);
            singleDuration = gestureData.singleDuration;
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

    public float SingleDuration {
        get => singleDuration;
        set => singleDuration = value;
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