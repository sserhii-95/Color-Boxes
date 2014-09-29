using UnityEngine;
using System.Collections;

public class CustomColorListener : MonoBehaviour
{

    public int index;
    private int dir;
    
    // Use this for initialization
    void Start()
    {
        dir = Random.Range(0, 1) * -2 + 1;
    }

    // Update is called once per frame
    void Update() {
        if (Random.Range(0f, 10000f) < 5) dir = -dir;

        renderer.material.color = Colors.customColors[index];
        transform.Rotate(Vector3.forward * Time.deltaTime * 100 * dir);
	}
}
