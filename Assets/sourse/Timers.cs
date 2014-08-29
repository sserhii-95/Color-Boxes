using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public static class Timers{

    private static ArrayList timers = new ArrayList();

    public static void AddTimer(Timer timer){
        timers.Add(timer);
    }

	public static bool ResetTimers(){
        try
        {
            for (int i = 0; i < timers.Count; i++)
            {

                (timers[i] as Timer).Reset(Time.timeSinceLevelLoad);
            }
        }
        catch (UnityException e) {
            Debug.LogError(e.ToString());
            return false;
        }

        return true;
    }
}
