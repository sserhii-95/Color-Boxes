using UnityEngine;
using System.Collections;

/**
 * Class, wich manages material of gameObject
 * Has some strategies:
 * 0) Color of gameObject changes color only when it collides with colored lines;
 * 1) Color of gameObject changes color when distance from 
 *    it to the player lowwer then value of field distanceOfChange and changes type to 0;
 * 2) if distance from gameObject to the player more then value
 *    of field distanceOfChange, color is blinking, else it changes type to 0;
 * 3) if distance from gameObject to the player more then value
 *    of field distanceOfChange, renderer component of gameObject
 *    is deactive, else it actives rendere component, and changes type to 0;
 * Player also has this component and allways has type 0
 */
public class CubeColor : MonoBehaviour {

    /**
     * Color of gameObject
     */
    private Color newColor = Color.white;

    /**
     * Previos color of gameObject
     */
    private Color oldColor = Color.black;

    /**
     * Process of changing color of gameObject
     * in range from 0 to timeForGhangingColor
     */
    private float processTime;
    private float processTime1;


    /**
     * Time for wich color of gameObject will be changing
     */
    public float timeForChangingColor = 0.4f;
    public float timeForTypeMinusOne = 5f;

    /**
     * Flag of changing color of gameObject
     */
    private bool isChanging = false;

    /**
     * Distance after wich, color of gameObject will change
     */
    public float distanceOfChange;

    /**
     * Type of strategy of gameObject
     */
    public int type = 0; //must be private!!

    /**
     * Instance of Transform of player
     */
    private Transform playerTransform;

    /**
     * Public read only access to color of gameObject
     */
    public Color ObjectColor {
        get {
            return newColor;
        }
    }

    /**
     * Public read/write access to type of strategy
     */
    public int Type {
        get {
            return type;
        }
        set {
            type = value;
            processTime = 0;
        }
    }

    /**
     * Public read only access to count of strategies
     */
    public static int TypeCount {
        get {
            return 4;
        }
    }

    /**
     * Inits some strategy with some color
     */
    public void Init(int type, Color color) {
        this.type = type;
        this.newColor = color;
        if (type == 3) SetRendererActive(false);
        ChangeColor();
        Paint(color);
    }

    /**
     * Inits some strategy
     */
    public void Init(int type) {
        Init(type, Colors.GetColor(Random.Range(0, 100)));
    }
	
    /**
     * Start method
     */
	void Start () {
        playerTransform = GameObject.Find("player").transform;
        processTime1 = 0;
        processTime = 0;
	}

    /**
     * Changes type to 0 and color
     */ 
    public void ChangeColorInTypeZero()
    {
        type = 0;
        ChangeColor();
    }

    /**
     * Change color of gameObject to other different color
     */
    public void ChangeColor() {
        ChangeColor(Colors.GetDiffColor(newColor, Random.Range(0, 100)));
    }
    
    /**
     * Change color of gameObject to some color
     */
    public void ChangeColor(Color color) {
        oldColor = newColor;
        newColor = color;
        if (type == 0)
        {
            isChanging = true;
            processTime = 0f;
        }
        if (gameObject.tag == "player")
            GetComponent<Transformer>().RotateOnDegrees(new Vector3(1f, 0f, 0f), 180f);
        else
            GetComponent<Transformer>().RotateOnDegrees(Vector3.up, 45f);

    }

   
    /**
     * Change color in visualization of gameObject on some color
     */
    public void Paint(Color color) {
        switch (tag) { 
            case "cube" :
                renderer.material.color = color;
                transform.GetChild(0).light.color = color;
                break;
            case "wall" :
                transform.GetChild(0).renderer.material.color = color;
                transform.GetChild(1).renderer.material.color = color;
                break;
            case "player" :
                renderer.material.color = color;
                break;
        }
    }
	
	/**
     * Update method
     */
	void Update () {
        float dist = Vector3.Distance(transform.position, playerTransform.position);
        Paint(newColor);
        switch (type) { 
            case 0 :
                if (isChanging)
                {
                    processTime += Time.deltaTime;
                    Paint(Color.Lerp(oldColor, newColor, Mathf.Min(1f, processTime / timeForChangingColor)));
                    if (processTime >= timeForChangingColor)
                    {
                        isChanging = false;
                        processTime = 0f;
                    }
                }
                else
                    Paint(newColor);
                break;
            case 1 :
                if (dist < distanceOfChange)
                {
                    ChangeColorInTypeZero();
                }
                break;
            case 2 :
                if (dist < distanceOfChange) 
                {
                    ChangeColorInTypeZero();
                }
                else
                {
                    if (processTime > timeForChangingColor)
                    {
                        processTime = 0f;
                        oldColor = newColor;
                        newColor = newColor.GetDiffColor(Random.Range(0, 100));
                        Paint(newColor);
                    }
                    else if (processTime > timeForChangingColor / 2f)
                    {
                        Paint(newColor);
                        processTime += Time.deltaTime;
                    }
                    else
                    {
                        processTime += Time.deltaTime;
                        Paint(Color.Lerp(oldColor, newColor, processTime/timeForChangingColor*2));
                    }
                }
                break;
            case 3 :
                if (dist < distanceOfChange)
                {
                    SetRendererActive(true);
                    ChangeColorInTypeZero();
                } 
                break;
            case -1 :
                processTime1 += Time.deltaTime;
                if (processTime1 >= timeForTypeMinusOne)
                {
                    ChangeColorInTypeZero();
                    processTime1 = 0f;
                }
                else
                {
                    if (processTime > timeForChangingColor/2)
                    {
                        oldColor = newColor;
                        newColor = newColor.GetDiffColor(Random.Range(0, 100));
                        processTime = 0f;
                    }
                    else
                    {
                        Paint(Color.Lerp(oldColor, newColor, processTime / timeForChangingColor));
                        processTime += Time.deltaTime;
                    }
                }
                    
                break;
        }
	}

    /**
     * Makes object visibility or not visibility depending on value
     */
    public void SetRendererActive(bool value){
        switch (gameObject.tag) { 
            case "cube" :
                renderer.enabled = value;
                transform.GetChild(0).light.enabled = value;
                break;
            case "wall" :
                transform.GetChild(0).light.enabled = value;
                transform.GetChild(0).light.enabled = value;
                break;
            case "player" :
                renderer.enabled = value;
                break;
        }

    }
}
