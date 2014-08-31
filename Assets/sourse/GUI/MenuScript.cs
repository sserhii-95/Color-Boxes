using UnityEngine;
using System.Collections;

public class MenuScript : MonoBehaviour {

    public Transform cubeContainer;
    public GameObject cubePrefab;
    public GameObject[] menuItems;
    public float timeBeforeChangingColor = 0.4f;
    private float timer = 0f;
    private float timer2 = 0f;

    public GameObject pointer;
    public int selectedIndex = 1;


    void OnGUI() {
      //  GUI.Label(new Rect(10, 100, 100, 100), (Time.timeSinceLevelLoad - timer) + "");
    }

	void Start () {
        timer = Time.timeSinceLevelLoad;
        timer2 = Time.timeSinceLevelLoad;
        ChangeColors();
        FirstGeneration();
        
        //menuItems[0].renderer.material.color = Color.cyan;
	}
	
	
	void Update () {

        GetInput();

        if (Time.timeSinceLevelLoad - timer > timeBeforeChangingColor)
        {
            timer = Time.timeSinceLevelLoad;
            ChangeColors();
            pointer.GetComponent<CubeColor>().ChangeColor();

            for(int i = 0; i < 15; i++)
                GenerateCube();
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

        bool someAction = false;

        //for PC:
        if (Input.GetKeyDown(KeyCode.UpArrow)) selectedIndex--;
        if (Input.GetKeyDown(KeyCode.DownArrow)) selectedIndex++;
        if (selectedIndex == 0) selectedIndex = menuItems.Length-1;
        if (selectedIndex == menuItems.Length) selectedIndex = 1;

        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            someAction = true;
        }
        string action = "";
        RaycastHit hit = new RaycastHit();
        if (Input.GetMouseButton(0))
            if (Physics.Raycast(camera.ScreenPointToRay(Input.mousePosition), out hit))
            {
                if (hit.collider.gameObject.GetComponent<TextMesh>() != null)
                {
                    action = hit.collider.gameObject.GetComponent<TextMesh>().text;
                }
            }

        //for Sensors without multiTouch yet
        if (Input.touchCount > 0)
            if (Physics.Raycast(camera.ScreenPointToRay(Input.GetTouch(0).position), out hit))
            {
                if (hit.collider.gameObject.GetComponent<TextMesh>() != null)
                {
                    action = hit.collider.gameObject.GetComponent<TextMesh>().text;
                }
            }

        if (action.Length > 0)
        {
            someAction = true;
            switch (action) { 
                case "Play" :
                    selectedIndex = 1;
                    break;
                case "Otions" :
                    selectedIndex = 2;
                    break;
                case "Exit" :
                    selectedIndex = 3;
                    break;
                default :
                    someAction = false;
                    break;
            }
        }

        if (someAction)
        {
            switch (selectedIndex)
            {
                case 1:
                    Debug.Log("Game!!!");
                    Application.LoadLevel(1);
                    break;
                case 2:
                    // Open options
                    break;
                case 3:
                    Application.Quit();
                    break;
            }
        }

    } 

    GameObject GenerateCube() {
        return GenerateCube(cubePrefab, cubeContainer);
    }

    public static GameObject GenerateCube(GameObject goPrefab, Transform container) {

        GameObject go = Instantiate(goPrefab) as GameObject;
        go.GetComponent<CubeColor>().Init(Random.Range(0, CubeColor.TypeCount+1));
        go.GetComponent<Transformer>().translateSpeed = Random.Range(30, 90);
        go.transform.position = new Vector3(-Random.Range(10, 150), Random.Range(-50, 50), 400);
        go.transform.parent = container;
        return go;
    }

    void FirstGeneration() {
        for (int i = 0; i < 40; i++) {
            GameObject go = GenerateCube();
            Vector3 pos = go.transform.localPosition;
            pos.x = Random.Range(-70, 150);
            go.transform.localPosition = pos;
        }
    }
}
