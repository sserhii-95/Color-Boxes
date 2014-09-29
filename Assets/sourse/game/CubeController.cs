using UnityEngine;
using System.Collections;

/**
 * Controlles a player
 */
public class CubeController : MonoBehaviour {

    /**
     * Instance of player
     */
    public GameObject player;

    /**
     * Line in wich player must be situate
     */
    public float line;

    /**
     * Width of 1 line of a road
     */
    private float roadWidth = 1.5f;

    private Touch touch;
    private bool isSwiping = false;

    private bool leftSwipe, rightSwipe;


    /**
     * public read only access to line of gameObject
     */
    public float Line {
        get {
            return line * roadWidth;
        }
    }

	
    /**
     * Start method
     */
	void Start () {
        line = 0;
        if (player == null)
        {
            CreatePlayer();
        }
        
	}

    /**
     * Creates player if it doesn't exists else updates player's transform to default
     */
    public GameObject CreatePlayer() {
        if (player == null)
        {
            player = Instantiate(camera.GetComponent<GameManager>().playerPrefab) as GameObject;
            player.name = "player";
            player.GetComponent<CubeColor>().ChangeColor();
        }
        else
        {
            GameObject prefab = camera.GetComponent<GameManager>().playerPrefab;
            player.transform.position = prefab.transform.position;
            player.transform.rotation = prefab.transform.rotation;
            player.transform.localScale= prefab.transform.localScale;
            //while(player.GetComponent<Transformer>() != null)
            //Destroy(player.GetComponent<Transformer>());
            player.GetComponent<Transformer>().deltaScale = Vector3.zero;

        }

        player.transform.position = Vector3.back * 8;
        line = 0;
        return player;
    }
	
	/**
     * Update method
     */
	void Update () {
        GetInput();
        Move();
	}

    private void Swipes() {
        leftSwipe = false;
        rightSwipe = false;

        if (Input.touchCount > 0) {
            switch(Input.GetTouch(0).phase){
                case TouchPhase.Began:
                    touch = Input.GetTouch(0);
                    isSwiping = true;
                break;
                /*case TouchPhase.Moved:
                if (isSwiping && Input.GetTouch(0).)
                {
                    Vector3 diff = Input.GetTouch(0).position - touch.position;
                    if (Vector3.Distance(Vector3.zero, diff) > 0.05)
                    {
                        if (diff.x > 0.05) rightSwipe = true;
                        else if (diff.x < -0.05) leftSwipe = true;
                    }
                    isSwiping = false;
                }
                break; */
                case TouchPhase.Ended:
                if (Input.GetTouch(0).tapCount == 1) 
                {
                    Vector3 pos = Input.GetTouch(0).position;
                    if (pos.y < Screen.height / 2)
                    if (pos.x > Screen.width / 2)
                        rightSwipe = true;
                    else
                        leftSwipe = true;

                }
                break;
            }
        }
    }

    /**
     * Changes line if was input commands to change it
     */
    private void GetInput() {

        Swipes();

            if (MoveLeft())
            {
                if (line > -1)
                    line--;
                else line = 1;
            }
            else if (MoveRight())
            {
                if (line < 1)
                    line++;
                else line = -1;
            }
    }

    /**
     * Translate player to line
     */
    void Move() {
        player.transform.Translate((Line - player.transform.position.x) * 10 * Time.deltaTime, 0, 0);
    }

    /**
     * Returns true if was pressed key LeftArrow
     */
    bool MoveLeft() {
        return (Input.GetKeyDown(KeyCode.LeftArrow) || leftSwipe);
    }

    /**
     * Returns true if was pressed key RightArrow
     */
    bool MoveRight()
    {
        return (Input.GetKeyDown(KeyCode.RightArrow) || rightSwipe);
    }
}
