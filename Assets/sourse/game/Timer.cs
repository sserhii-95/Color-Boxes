using UnityEngine;
using System.Collections;

public class Timer : MonoBehaviour {


    private float time;
    private float timeSinceLevelRestart;

    public float TimeSinceRestart {
        get {
            return timeSinceLevelRestart;
        }
        set {
            timeSinceLevelRestart = value;
        }
    }
	
	public void Start () {
        time = 0;
        Timers.AddTimer(this);
	}
	
	
	public void Update () {
        time += Time.deltaTime;
	}

    public float GetTime() {
        return time;
    }

    public void Reset() {
        time = 0;
    }

    public void Reset(float lastTime) {
        time = 0;
        timeSinceLevelRestart = lastTime;
    }
}
