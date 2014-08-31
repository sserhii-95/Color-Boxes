using UnityEngine;
using System.Collections;
/**
 * Component, wich can manage transform of gameObject
 */
public class Transformer : MonoBehaviour {

    /**
     * Speed of translating of gameObject
     */
    public float translateSpeed = 10f;
    /**
     * Time of rotating of gameObject
     */
    public float rotateTime = 0.1f;
    /**
     * Speed of scaling of gameObject 
     */
    public float scaleSpeed = -10f;

    public float angleDef;

    /**
     * Direction in wich gameObject will be translating
     */
    public Vector3 direction  = Vector3.zero;

    /**
     * Numbers, wich will be add to localScale of gameObject
     */
    public Vector3 deltaScale = Vector3.zero;

    /**
     * Rotation vector
     */
    public Vector3 rotation   = Vector3.zero;

    /**
     * flag of possibility of translating of gameObject
     */
    public bool canTranslate = true;

    /**
     * flag of possibility of rotating of gameObject
     */
    public bool canRotate = true;

    /**
     * flag of possibility of scaling of gameObject
     */
    public bool canScale = true;

    private Vector3 deltaRotate;

    /**
     * Instance of a Timer
     */
    private Timer timer;

    public void Scale() {
        deltaScale = new Vector3(1f, 1f, 1f);
        scaleSpeed = -20;
    }

    public void RotateOnDegrees(Vector3 direction) {
        RotateOnDegrees(direction, angleDef);
    }

    public void RotateOnDegrees(Vector3 direction, float AngleInDegrees) {
        rotation = direction * AngleInDegrees;
        deltaRotate = rotation / rotateTime;
    }

    /**
     * Start method
     */
	void Start () {
        timer = GameObject.Find("Main Camera").GetComponent<Timer>();
	}
	
	/**
     * Update method
     */
	void Update () {
        if (canTranslate) { 
            translateSpeed = Numbers.GetTranslateSpeed(Time.timeSinceLevelLoad - timer.TimeSinceRestart);
            transform.position = transform.position + direction * translateSpeed * Time.deltaTime;
       }

        if (canScale)
        {
            Vector3 scale = transform.localScale + deltaScale * scaleSpeed * Time.deltaTime;

            if (scale.x < 0) scale.x = 0;
            if (scale.y < 0) scale.y = 0;
            if (scale.z < 0) scale.z = 0;

            transform.localScale = scale;

            if (transform.childCount == 1)
            {
                transform.GetChild(0).light.range = Mathf.Max(0, transform.GetChild(0).light.range + deltaScale.x * scaleSpeed * Time.deltaTime / 2);
            }
        }

        if (canRotate)
        {
            if (rotation.x > 0 || rotation.y > 0 || rotation.z > 0)
            {
                /*
                Vector3 delta = Vector3.zero;
                if (rotation.x > 0) delta.x = Mathf.Max(1f, rotation.x / rotateTime * Time.deltaTime);//Mathf.Min(rotation.x, rotarotateSpeed * Time.deltaTime);
                if (rotation.y > 0) delta.y = Mathf.Max(1f, rotation.y / rotateTime * Time.deltaTime);//Mathf.Min(rotation.y, rotateSpeed * Time.deltaTime);
                if (rotation.z > 0) delta.z = Mathf.Max(1f, rotation.z / rotateTime * Time.deltaTime);//Mathf.Min(rotation.z, rotateSpeed * Time.deltaTime);
                 */
                transform.Rotate(deltaRotate * Time.deltaTime);
                rotation -= deltaRotate*Time.deltaTime;
            }
            else
            {
                transform.Rotate(rotation);
                rotation = Vector3.zero;
                transform.rotation = Quaternion.AngleAxis(0f, Vector3.zero);
                //rotation = Vector3.zero;
//                
            }
        }
	}
        
}
