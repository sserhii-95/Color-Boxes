using UnityEngine;
using System.Collections;

public class MenuScript : MonoBehaviour
{

    #region Public Variables

    public GameObject cubePrefab;
    public Transform cubeContainer;
    public GameObject[] menuItems;
    public GameObject[] windowContainers;


    #endregion


    #region Private Variables

    public float timeBeforeChangingColor = 0.4f;
    private float timer = 0f;
    private float timer2 = 0f;
    private int windowId = 0;
    private Vector3 deltaPosition;

    #endregion

    public GameObject pointer;
    public int selectedIndex = 1;

	void Start () {
        Colors.Init(Colors.colorsM);
        timer = Time.timeSinceLevelLoad;
        timer2 = Time.timeSinceLevelLoad;
        ChangeColors();
        FirstGeneration();
        
        //menuItems[0].renderer.material.color = Color.cyan;
	}
	
	
	void Update () {

        Transform windowTransform = windowContainers[windowId].transform;
        Vector3 windowPos = windowTransform.localPosition;
        float f = Mathf.Abs(windowPos.x) +Mathf.Abs(windowPos.y);

        if (f > 1e-1){

            float dX = Time.deltaTime * 3 * deltaPosition.x;
            float dY = Time.deltaTime * 3 * deltaPosition.y; 
            if (f < 2)
            {
                dX = Time.deltaTime * 10 * -windowPos.x;
                dY = Time.deltaTime * 10 * -windowPos.y;
            }
            


            for(int i = 0; i < windowContainers.Length; i++)
                windowContainers[i].transform.Translate(dX, dY, 0);


        } else{
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
	}

    private void ChangeColors(){

        foreach (GameObject go in menuItems)
        {
            go.GetComponent<CubeColor>().ChangeColor();
            Debug.Log(go.name);
        }

    }

    #region Input
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
                    action = hit.collider.gameObject.name;
                }
            }

        //for Sensors without multiTouch yet
        if (Input.touchCount > 0)
            if (Physics.Raycast(camera.ScreenPointToRay(Input.GetTouch(0).position), out hit))
            {
                if (hit.collider.gameObject.GetComponent<TextMesh>() != null)
                {
                    action = hit.collider.gameObject.name;
                }
            }

        if (action.Length > 0)
        {
            Debug.Log(action);
            someAction = true;
            switch (action) { 
                case "Play" :
                    selectedIndex = 1;
                    break;
                case "Options" :
                    selectedIndex = 2;
                    break;
                case "Exit" :
                    selectedIndex = 3;
                    break;
                case "BackToMainMenu":
                    selectedIndex = 4;
                    break;
                case "ColorsProperties":
                    selectedIndex = 5;
                    break;
                case "BackToOptions":
                    selectedIndex = 6;
                    break;
                case "StandartSet":
                    selectedIndex = 7;
                    break;
                case "StandartSet+":
                    selectedIndex = 8;
                    break;
                case "GoldSet":
                    selectedIndex = 9;
                    break;
                case "CustomSet":
                    selectedIndex = 10;
                    break;
                case "BackToColorProperties":
                    selectedIndex = 11;
                    break;
                default :
                    someAction = false;
                    break;
            }
        }

        

        if (someAction)
        {
            Debug.Log(action);

            switch (selectedIndex)
            {
                case 1:
                    Debug.Log("Game!!!");
                    Application.LoadLevel(1);
                    break;
                case 2:
                    NextWindow(1);
                    break;
                case 3:
                    Application.Quit();
                    break;
                case 4:
                    NextWindow(0);
                    break;
                case 5:
                    NextWindow(2);
                    break;
                case 6:
                    NextWindow(1);
                    break;
                case 7:
                    Colors.Init(Colors.colorsS);
                    break;
                case 8:
                    Colors.Init(Colors.colorsM);
                    break;
                case 9:
                    Colors.Init(Colors.colorsG);
                    break;
                case 10:
                    NextWindow(3);
                    break;
                case 11:
                    NextWindow(2);
                    break;
            }
        }

    }
    #endregion

    #region BackGround Cubes
    GameObject GenerateCube() {
        return GenerateCube(cubePrefab, cubeContainer);
    }

    public static GameObject GenerateCube(GameObject goPrefab, Transform container) {

        GameObject go = Instantiate(goPrefab) as GameObject;
        go.GetComponent<CubeColor>().Init(Random.Range(0, CubeColor.TypeCount+1));
        go.GetComponent<Transformer>().translateSpeed = Random.Range(30, 90);
        go.GetComponent<LifeTimer>().timeOfLife = 10f;
        go.transform.position = new Vector3(-Random.Range(10, 150), Random.Range(-50, 50), 400);
        go.transform.parent = container;
        return go;
    }

    void FirstGeneration() {
        for (int i = 0; i < 40; i++) {
            GameObject go = GenerateCube();
            Vector3 pos = go.transform.localPosition;
            pos.x = Random.Range(-70, 200);
            go.transform.localPosition = pos;
        }
    }
    #endregion


    #region Windows

    public void NextWindow(int next) {
        windowId = next;
        deltaPosition = -windowContainers[windowId].transform.localPosition;
    }

    #endregion
}
