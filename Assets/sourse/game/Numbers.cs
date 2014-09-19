using UnityEngine;
using System.Collections;

/**
 * Static class for calculating different numbers and probabilities in the game 
 */
public static class Numbers{

    /**
     * Returns random index of elements, wich have probabilities as parametr of this method
     */
    public static int GetIndex(float[] probabilities){
        float random = Random.Range(0f, 1f);
        float sum = probabilities[0];
        int index = 0;
        while (random > sum){
            if (index + 1 == probabilities.Length) return index;
            sum += probabilities[++index];
        }
        return index;
    }

    /**
     * Returns probablity for some type of cube in some time of game
     */
    public static float GetProbabiltyOfCube(int type, float time) {
        switch (type) { 
            case 0 :
                if (time < 10) return 1f;
                else if (time < 20) return 0.8f;
                else if (time < 30) return 0.5f;
                else if (time < 40) return 0.4f;
                else return 0.25f;
                break;
            case 1:
                if (time < 10) return 0;//0
                else if (time < 20) return 0.2f;//0.2
                else if (time < 30) return 0.1f;
                else if (time < 40) return 0.2f;
                else return 0.25f;
                break;
            case 2:
                if (time < 20) return 0f;
                else if (time < 30) return 0.1f;
                else if (time < 40) return 0.2f;
                else return 0.25f;
                break;
            case 3:
                if (time < 30) return 0f;
                else if (time < 40) return 0.2f;
                else return 0.25f;
                break;
        }
        return 0;
    }

    /**
     * Returns random type of cubes in some time
     */
    public static int GetCubeType(float time) { 
        float[] probs = new float[CubeColor.TypeCount];
        for (int i = 0; i < probs.Length; i++) {
            probs[i] = GetProbabiltyOfCube(i, time);
        }
        return GetIndex(probs);
    }

    public static float GetProbabiltyOfWall(int type)
    {
        return 0;
    }

    /**
     * Return speed of objects in some time
     */
    public static float GetTranslateSpeed(float time)
    {
        float minSpeed = 20;
        float maxSpeed = 30;
        float maxTime = 40;
//        if (true)
//           return 25;
        if (time > maxTime) return maxSpeed;
        else
            return minSpeed + time / maxTime * (maxSpeed - minSpeed);
    }


}
