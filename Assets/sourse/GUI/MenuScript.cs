using UnityEngine;
using System.Collections;

public class MenuScript : MonoBehaviour {

    public GameObject[] menuItems;
    public float timeBeforeChangingColor = 0.4f;
    private float timer = 0f;

    public GameObject pointer;
    public int selectedIndex = 1;


    void OnGUI() {
        GUI.Label(new Rect(10, 100, 100, 100), (Time.timeSinceLevelLoad - timer) + "");
    }

	void Start () {
        timer = Time.timeSinceLevelLoad;
        ChangeColors();
        
        //menuItems[0].renderer.material.color = Color.cyan;
	}
	
	
	void Update () {

        GetInput();

        if (Time.timeSinceLevelLoad - timer > timeBeforeChangingColor)
        {
            timer = Time.timeSinceLevelLoad;
            ChangeColors();
            pointer.GetComponent<CubeColor>().ChangeColor();
        }
        Vector3 pos = pointer.transform.localPosition;
        pos.y = menuItems[selectedIndex].transform.localPosition.y;
        pointer.transform.localPosition = pos;

	}

    private void ChangeColors(){

        foreach (GameObject go in menuItems)
            go.GetComponent<CubeColor>().ChangeColor();
    }

    void GetInput() { 
        //for PC:
        if (Input.GetKeyDown(KeyCode.UpArrow)) selectedIndex--;
        if (Input.GetKeyDown(KeyCode.DownArrow)) selectedIndex++;
        if (selectedIndex == 0) selectedIndex = menuItems.Length-1;
        if (selectedIndex == menuItems.Length) selectedIndex = 1;

        if (Input.GetKeyDown(KeyCode.KeypadEnter) || Input.GetKeyDown("enter"))
        {
            switch (selectedIndex) { 
                case 1: 
                    Debug.Log("Game!!!");
                    Application.LoadLevel(0);
                    break;
            }
            
        }
    }
}
