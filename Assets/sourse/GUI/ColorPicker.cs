using UnityEngine;
using System.Collections;

public class ColorPicker : MonoBehaviour {


    public Color defColor = Color.black;

    [Range(0f, 15f)]
    public float radiusRed = 6;
    [Range(0f, 15f)]
    public float radiusGreen = 4.5f;
    [Range(0f, 15f)]
    public float radiusBlue = 3;

    [Range(5, 200)]
    public int count = 45;

    [Range(0.01f, 3f)]
    public float cubeScale = 0.7f;

    private Color redColor;
    private Color greenColor;
    private Color blueColor;

    private bool isCreated = false;

    [Range(0f, 1f)]
    public float px;
    [Range(0f, 1f)]
    public float py;
    [Range(0f, 1f)]
    public float pz;

    public Vector3 progress;

    private Color color;

    public GameObject cubePrefab;
    public GameObject colorTarget;
    public GameObject container;

    private int colorChoosing = -1;


    public void Init(Color color) {
        px = color.r;
        py = color.g;
        pz = color.b;
        CalcColors();
        Repaint();
    }
	
	void Start () {
        if (cubePrefab == null) cubePrefab = GameObject.CreatePrimitive(PrimitiveType.Cube);
        if (colorTarget == null) colorTarget = gameObject;
        if (container == null) container = new GameObject();
        
        DrawCircle(0);
        DrawCircle(1);
        DrawCircle(2);
	}
	
	
	void Update () {
        GetInput();
        CalcColors();
        Repaint();
	}

    void Repaint() {

        for (int i = 0; i < container.transform.childCount; i++) {
            GameObject go;
            string[] pars = (go = container.transform.GetChild(i).gameObject).name.Split(' ');

            float prog = 0;
            Color color = Color.black;

            int param = int.Parse(pars[1]);
            int position = int.Parse(pars[2]);
            int count = int.Parse(pars[3]);

            switch (param) { 
                case 0:
                    prog = progress.x;
                    color = this.redColor;
                    break;
                case 1:
                    prog = progress.y;
                    color = this.greenColor;
                    break;
                case 2:
                    prog = progress.z;
                    color = this.blueColor;
                    break;
            }

            if (1f * (position - 1) / count <= prog){
                go.renderer.material.color = color;
            }
            else{

                go.renderer.material.color = defColor;
            }
            

        }

    }


    void DrawCircle(int param) {
        float radius;
        Color color;
        int count = this.count;
        float progress;
    
        switch (param) { 
            case 0:
                radius = radiusRed;
                color = redColor;
                progress = this.progress.x;
            break;
            case 1:
                radius = radiusGreen;
                color = greenColor;
                count = (int)(count * 0.75);
                progress = this.progress.y;
            break;
            case 2:
                radius = radiusBlue;
                color = blueColor;
                count = (int)(count * 0.5);
                progress = this.progress.z;
            break;
            default:
                radius = 0;
                color = Color.black;
                count = 1;
                progress = 0;
                break;
        }

        
        for (int i = 0; i < count; i++) {
            float angle = Mathf.PI * 2 * i / count;
            Vector3 position = new Vector3(Mathf.Cos(angle), Mathf.Sin(angle), 0) * radius;
            GameObject cube = Instantiate(cubePrefab) as GameObject;
            cube.name = "cp "+ param + " " + i + " " + count;
            cube.transform.parent = container.transform;
            cube.transform.localScale = new Vector3(cubeScale, cubeScale, cubeScale);
            cube.transform.localPosition = position;
            cube.transform.LookAt(transform.position, position * 2);
            
            //if (count * progress > i)
            //   cube.renderer.material.color = color;
            //else
            cube.renderer.material.color = defColor;
        }

    }

    void ClearSceen(){
        for (int i = 0; i < container.transform.childCount; i++)
        {
            Destroy(container.transform.GetChild(i).gameObject);
        }
    }


    void CalcColors() {
        progress = new Vector3(px, py, pz);
        redColor = new Color(0.3f + 0.6f * progress.x, 0, 0);
        greenColor = new Color(0, 0.3f + 0.6f * progress.y, 0);
        blueColor = new Color(0, 0, 0.3f + 0.6f * progress.z);
        //color = new Color(progress.x, progress.y, progress.z);
        color = new Color(progress.x, progress.y, progress.z);
        colorTarget.renderer.material.color = color;
    }

    #region Input
    void GetInput() {
        if (!Input.GetMouseButton(0) || Input.touchCount == 0)
            colorChoosing = -1;

        if (Input.GetMouseButton(0) || Input.touchCount > 0) {
            Ray ray;
            if (Input.GetMouseButton(0))
                ray = Camera.mainCamera.ScreenPointToRay(Input.mousePosition);
            else
                ray = Camera.mainCamera.ScreenPointToRay(Input.GetTouch(0).position);

            RaycastHit hit = new RaycastHit();
            if (Physics.Raycast(ray, out hit, 20)) {
                GameObject go = hit.collider.gameObject;
            

                if (go.name.StartsWith("cp")) {

                    string name = go.name;

                    string[] q = name.Split(' ');

                    int param = int.Parse(q[1]);
                    float position = int.Parse(q[2]);
                    int count = int.Parse(q[3]);

                    if (colorChoosing == -1) 
                        colorChoosing = param;

                    if (colorChoosing == param)
                    switch (param) { 
                        case 0:
                            px = position / count;
                            break;
                        case 1:
                            py = position / count;
                            break;
                        case 2:
                            pz = position / count;
                            break;
                    }
              
                }
            }
            if (colorChoosing >= 0)
            {
                Vector3 cursorPosition;
                if (Input.GetMouseButton(0))
                    cursorPosition = Input.mousePosition;
                else
                    cursorPosition = Input.GetTouch(0).position;

                float angle = Vector3.Angle(Vector3.right, cursorPosition - Camera.mainCamera.WorldToScreenPoint(colorTarget.transform.position));

                if (cursorPosition.y < Camera.mainCamera.WorldToScreenPoint(colorTarget.transform.position).y) angle = 360 - angle;
                
                Debug.Log(angle);

                switch (colorChoosing)
                {
                    case 0:
                        px = angle / 360.0f;
                        break;
                    case 1:
                        py = angle / 360.0f;
                        break;
                    case 2:
                        pz = angle / 360.0f;
                        break;
                }
            }

 

        }
    }

    #endregion
}

