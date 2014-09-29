using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GUIScript : MonoBehaviour {

    public GameObject cubePrefab;
    public GameObject timePrefab;
    private ArrayList cubes;
    private Color transparent = new Color(0, 0, 0, 0);

    public float startPosition = 2f;
    private int countVisibleCubes = 0;
    private string oldResult = "";

    private float oldTime = 0f;
    public  TextMesh text;
    private GameObject timeCube;


	void Start () {
        startPosition = GameObject.Find("GUICamera").GetComponent<Camera>().orthographicSize * Screen.width / Screen.height * 0.8f;
       // Camera cam = GetComponent<Camera>();
       // cam.transform.position.ToV + 0.9f * cam.exte
        
	    cubes = new ArrayList();
        for (int i = 0; i < 5; i++)
            CreateCube(i);
        Debug.Log(cubes.Count);
        for (int i = 0; i < cubes.Count; i++)
        {
            Debug.Log(cubes[i]);
        }

        CreateGUITimer();
	}

    private void CreateGUITimer() {
        timeCube = Instantiate(timePrefab) as GameObject;
        timeCube.transform.parent = transform;
        timeCube.transform.localScale = Vector3.one / 5;
        timeCube.transform.localPosition = new Vector3(-startPosition , 0.8f, 1);
        timeCube.transform.Rotate(0, 90, 0);
        timeCube.GetComponent<CubeColor>().ChangeColor();
        timeCube.transform.rotation = Quaternion.AngleAxis(0f, Vector3.zero);
        oldTime = 0f;

        text.transform.localPosition = new Vector3(timeCube.transform.localPosition.x + 1f * timeCube.renderer.bounds.size.x, 0.8f, 1f);
        text.transform.localScale = Vector3.one / 5;
             
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
       // if (result < 0) result *= -1; // must be deleted!

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

    public void UpdateGUITimer(float newTime) {
        newTime = Mathf.Round(newTime * 10f) / 10f;
        text.text = newTime + "";
        if (newTime - oldTime > 1f) {
            timeCube.GetComponent<CubeColor>().ChangeColor();
            oldTime = Mathf.Round(newTime);
        }
    }

    private void SetTextForCube(GameObject go, string text)
    {

        for (int i = 0; i < (go as GameObject).transform.childCount; i++)
            if ((go as GameObject).transform.GetChild(i).name.Equals("front"))
            {
                (go as GameObject).transform.GetChild(i).GetComponent<TextMesh>().text = text;
                //(go as GameObject).transform.GetChild(i).transform.Rotate(0f, 360f, 0f);
            }
    }

    private void CreateCube(int index) { 
        float xPosition = 0;
        if (index == 0) {
            xPosition = startPosition;
        } else
        {
            xPosition = (cubes[index - 1] as GameObject).transform.localPosition.x - cubePrefab.renderer.bounds.size.x * 1.1f / 5;
        }

        GameObject go = Instantiate(cubePrefab) as GameObject;
        go.transform.parent = transform;
        go.transform.localPosition = new Vector3(xPosition, 0.8f, 1);
        go.transform.localScale = Vector3.one / 5;
        go.GetComponent<CubeColor>().Init(0);
        go.GetComponent<CubeColor>().SetRendererActive(false);
        go.transform.GetChild(0).light.range = go.transform.GetChild(0).light.range / 5;
        
        cubes.Add(go);
    }


}
