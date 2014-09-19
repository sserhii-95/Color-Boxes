using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class StaticCubeColor : MonoBehaviour {


    public bool canChanging;

    public Color defaultColor;
    private Color color;
    private float lastTime;

    private List<Color> invalidsColors = new List<Color>();
    private Transformer transformer;


	void Start () {
        lastTime = Time.timeSinceLevelLoad;
        transformer = GetComponent<Transformer>();
        if (defaultColor != null) SetColor(defaultColor);
	}
	
	void Update () {
        if (canChanging)
        {
        }

        if (Time.timeSinceLevelLoad - lastTime > 3) {
            transformer.RotateOnDegrees(Vector3.up);
            lastTime = Time.timeSinceLevelLoad;
        }
	}


    public bool SetColor(Color color) {
        if (Validate(color))
        {
            this.color = color;
            return true;
        }
        return false;
    }

    public void AddInvalidColor(Color color) {
        invalidsColors.Add(color);
    }

    public bool Validate(Color color) {
        for (int i = 0; i < invalidsColors.Count; i++) {
            if (invalidsColors[i].Equals(color)) return false;
        }
        return true;
    }

}

