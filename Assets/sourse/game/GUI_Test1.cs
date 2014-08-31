using UnityEngine;
using System.Collections;

public class GUI_Test1 : MonoBehaviour {


    public static GameObject cubePrefab;

    private void OnGUI(){
        if (GUI.Button(new Rect(1, 1, 100, 50), "Change Color"))
        {
            GameObject.Find("Cube").GetComponent<CubeColor>().ChangeColor();
        }

        if (GUI.Button(new Rect(1, 52, 100, 50), "Destroy")) {
            GameObject.Find("Cube").GetComponent<Transformer>().Scale();
        }
    }

	// Use this for initialization
	void Start () {
        Colors.Init(new Color[]{
            Color.blue,
            Color.cyan,
            Color.green,
            Color.red,
            Color.yellow
        });
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
