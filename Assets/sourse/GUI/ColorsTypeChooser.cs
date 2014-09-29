using UnityEngine;
using System.Collections;

public class ColorsTypeChooser : MonoBehaviour {

    public GameObject[] colortypes;
    private int selectedIndex = 0;

	
	void Start () {
        colortypes[selectedIndex].transform.Translate(1, 0, 0);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

   public bool SetIndex(int index) {
        if (!Colors.isLocked(index)){
            colortypes[selectedIndex].transform.Translate(-1, 0, 0);
            selectedIndex = index;
            colortypes[selectedIndex].transform.Translate(1, 0, 0);
            Colors.Init(index);
            return true;
        }
        return false;
    }
}
