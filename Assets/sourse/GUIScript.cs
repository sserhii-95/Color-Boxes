using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GUIScript : MonoBehaviour {

    public GameObject cubePrefab;
    private ArrayList cubes;
    private Color transparent = new Color(0, 0, 0, 0);

    public float startPosition = 1.8f;
    private int countVisibleCubes = 0;
    private string oldResult = "";
    

	void Start () {
	    cubes = new ArrayList();
        for (int i = 0; i < 4; i++)
            CreateCube(i);
        Debug.Log(cubes.Count);
        for (int i = 0; i < cubes.Count; i++)
        {
            Debug.Log(cubes[i]);
        }

	}

	void Update () {
	
	}

    private string ReverseString(string ins) {
        string outs = "";
        for (int i = 0; i < ins.Length; i++)
            outs = ins[i] + outs;
        return outs;
    }

    public void UpdateResult(int result) {
        if (result < 0) result *= -1; // must be deleted!

        string resString = ReverseString(result.ToString());
        
        for (int index = 0; index < resString.Length; index++)
        {
            Debug.Log(index + " " + resString + " " + index);

            if (index == countVisibleCubes)
            {
                (cubes[index] as GameObject).GetComponent<CubeColor>().SetRendererActive(true);
                countVisibleCubes++;
                oldResult += "#";
            }

            if (oldResult[index] != resString[index])
            {
                (cubes[index] as GameObject).GetComponent<CubeColor>().ChangeColor();
                SetTextForCube(cubes[index] as GameObject, resString[index]+"");
            }

            //sadasfafblablbalbalba
        }

        for (int i = resString.Length; i < cubes.Count; i++)
        {
            (cubes[i] as GameObject).GetComponent<CubeColor>().SetRendererActive(false);
            SetTextForCube(cubes[i] as GameObject, "");
        }


        countVisibleCubes = resString.Length;
        oldResult = resString;
    }

    private void SetTextForCube(GameObject go, string text)
    {

        for (int i = 0; i < (go as GameObject).transform.childCount; i++)
            if ((go as GameObject).transform.GetChild(i).name.Equals("front"))
            {
                (go as GameObject).transform.GetChild(i).GetComponent<TextMesh>().text = text;
            }
    }

    private void CreateCube(int index) { 
        float xPosition = 0;
        if (index == 0) {
            xPosition = startPosition; 
        } else
        {
            xPosition = (cubes[index - 1] as GameObject).transform.localPosition.x - cubePrefab.renderer.bounds.size.x * 1.1f;
        }

        GameObject go = Instantiate(cubePrefab) as GameObject;
        go.transform.parent = transform;
        go.transform.localPosition = new Vector3(xPosition, 0, 1);
        go.GetComponent<CubeColor>().Init(0);
        go.GetComponent<CubeColor>().SetRendererActive(false);

        cubes.Add(go);
    }


}
