using UnityEngine;
using System.Collections;

public class LifeTimer : MonoBehaviour {

    private float time;
    public float timeOfLife = 10;

	void Start () {
        time = 0;
	}
	

	void Update () {
        time += Time.deltaTime;
        if (timeOfLife > 0)
        {
            if (time >= timeOfLife) {
                Destroy(gameObject);
            }
        }
	}

    public void Reset() {
        time = 0;
    }

    public float GetTime() {
        return time;
    }
}
