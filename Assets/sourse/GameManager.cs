using UnityEngine;
using System.Collections;

/**
 * Game manager
 */
public class GameManager : MonoBehaviour {

    /**
     * Game mode: playing
     */
    private const int WORK = 0;
    /**
     * Game mode: pause
     */
    private const int PAUSE = 1;
    /**
     * Game mode: game over
     */
    private const int GAME_OVER = 2;

    /**
     * Prefab, wich will used for create colored cubes 
     */
    public GameObject cubePrefab;
    /**
     * Prefab, wich will used for create colored lines on the floor
     */
    public GameObject wallPrefab;
    /**
     * Prefab for player
     */
    public GameObject playerPrefab;
    /**
     * Prefab for light balls
     */
    public GameObject lightPrefab;

    /**
     * Container for cubes
     */
    public Transform cubeContainer;

    /**
     * Start position for cubes
     */
    public Vector3 startPosition = new Vector3(0, 0, 20);

    /**
     * Width of 1 line of the road
     */
    public float roadWidth = 1.5f;

    /**
     * Time interval between cubes generation
     */
    public float timeInterval = 0.5f;

    /**
     * Number of points, wich will be losed when player collides with cubes
     */
    public int damageOnCollide = 10;
    

    public GUIStyle style1;
    public GUIStyle style2;

    /**
     * Game points
     */
    public int points;

    /**
     * Timer
     */
    private Timer timer;

    /**
     * Instance of player
     */
    private GameObject player;

    /**
     * Width of display in px
     */
    private float dWidth = 1000;

    /**
     * Height of display in px
     */
    private float dHeight = 800;

    /**
     * Game Status, can be: Work, Pause, GameOver
     */
    public int gameStatus;

    /**
     * Number of cubes wich will be generated between creation of colored lines
     */ 
    public int cubesCount = 10;

    /**
     * Buffer, which calc count of cubes
     */
    private int cubesCountBuffer = 0;

    /**
     * public read only access to points
     */
    public int Points {
        get {
            return points;
        }
    }

    private GameObject GUICamera;

    private bool changingResults = false;

    /**
     * Method, wich manages GUI
     */
    void OnGUI() {

        switch (gameStatus)
        {
            case WORK:
                GUI.Label(new Rect(10, 10, 1000, 50), points + " "+ GetGameTime() + " " + Numbers.GetTranslateSpeed(GetGameTime()), style2);
                TestTypeMinusOne();
                if (changingResults) {
                    GUICamera.GetComponent<GUIScript>().UpdateResult(points);
                    changingResults = false;
                }
                break;
            case PAUSE:
                break;
            case GAME_OVER:
                GUI.Label(new Rect(20, 20, 100, 100), "GAME OVER", style2);
                GUI.Label(new Rect(20, 50, 100, 100), "Your score: "+points, style2);

                if (GUI.Button(new Rect(20, 80, 100, 50), "Restart", style2)) {
                    StartGame();
                }
                break;
        }
        
    }

    private void TestTypeMinusOne() {
        if (GUI.Button(new Rect(10, 30, 100, 40), "MultiColor", style2))
        {
            player.GetComponent<CubeColor>().Type = -1;
        }
    }

    /**
     * Increments game points
     */ 
    public void AddPoint() {
        points++;
        changingResults = true;
    }

    /**
     * player loses points in size equals damageOnCollide
     */ 
    public void UsePoints()
    {
        points -= damageOnCollide;
        changingResults = true;
    }

	/**
	 * Start method
	 */
	void Start () {
        timer = GetComponent<Timer>();
        GUICamera = GameObject.Find("GUICamera");
        StartGame();
	}
	
	/**
	 * Update method
	 */
	void Update () {
        if (timer.GetTime() > timeInterval) {
            if (cubesCountBuffer > cubesCount)
            {
                if (player.GetComponent<CubeColor>().Type != -1) 
                    GenerateWall();
                cubesCountBuffer = 0;
            }
            else
            {
                Generate();
                cubesCountBuffer++;
            }

            //for (int i = 2; i < 5; i++ )
             //   Generate(i * (Random.Range(0, 2) * -2 + 1) * roadWidth);
            
            timer.Reset();
        }

	}

	/**
	 * Method, wich generates cubes
	 */
    public GameObject Generate()
    {
        GameObject go;
        int r = Random.Range(-1, 2);
        go = Generate(r * roadWidth);

        if (Random.Range(0, 3) < 1)
        {
            int l = (r + 2) % 3  - 1;
            Generate(l * roadWidth);
        }

        /* Decorations
        for (int i = 2; i < 7; i++)
        {
            Generate(i * roadWidth * (Random.Range(0, 2) * (-2) + 1));
        }
         */
            return go;
    }

	/**
	 * Method, wich generates cubes with position.x = xPosition
	 */
	public GameObject Generate(float xPosition) {
        GameObject go = Instantiate(cubePrefab) as GameObject;
        go.transform.parent = cubeContainer;
        go.transform.position = startPosition;
        go.name = "cube";

        Vector3 pos = go.transform.position;
        pos.x = xPosition;
        go.transform.position = pos;
        go.GetComponent<CubeColor>().Init(Numbers.GetCubeType(GetGameTime()));
        
        return go;
    }

	/**
	 * Method, wich generates a colored line
	 */
    public GameObject GenerateWall() {
        GameObject go = Instantiate(wallPrefab) as GameObject;
        go.transform.parent = cubeContainer;
        go.transform.position = startPosition;
        go.name = "wall";

        go.GetComponent<CubeColor>().Init(0);
        return go;
    }

	/**
	 * Change game status to pause
	 */
    public void StopGame() {
        gameStatus = PAUSE;
    }

	/**
	 * Change game status to playing
	 */
    public void ResumeGame() {
        gameStatus = WORK;
    }

	/**
	 * Change game status to game over
	 */
    public void GameOver() {
        gameStatus = GAME_OVER;
    }

	/**
	 * Inits game
	 */
    public void StartGame() {
        ClearScene();
        player = GetComponent<CubeController>().CreatePlayer();
        points = 0;
        changingResults = true;
        Timers.ResetTimers();
        ResumeGame();
    }

	/**
	 * Destroyes all GameObjects in scene
	 */
    private void ClearScene() {
        for (int i = 0; i < cubeContainer.transform.GetChildCount(); i++)
        {
            Destroy(cubeContainer.transform.GetChild(i).gameObject);
        }
    }

	/**
	 * Returns time since start game
	 */
    private float GetGameTime() {
        return Time.timeSinceLevelLoad - timer.TimeSinceRestart;
    }
}
