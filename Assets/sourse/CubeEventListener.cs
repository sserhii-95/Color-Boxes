using UnityEngine;
using System.Collections;

/**
 * Event listener for Player.
 */
public class CubeEventListener : MonoBehaviour {

    /**
     * Instance of GameManager
     */
    private GameManager gameManager;

    private Color otherColor;

    /**
     * Mehtod of calculating event, when player collides with other objects.
     */
    void OnTriggerEnter(Collider other) {
        //GameObject.CreatePrimitive(PrimitiveType.Sphere);
        
        if (other.gameObject.name.Equals("cube")) {

            other.gameObject.GetComponent<Transformer>().Scale();
            if (other.gameObject.GetComponent<CubeColor>().ObjectColor.Equals(GetComponent<CubeColor>().ObjectColor) || GetComponent<CubeColor>().Type == -1) {
                other.gameObject.GetComponent<Transformer>().direction.y = 1;
                gameManager.AddPoint();
            }
            else {
                if (gameManager.Points >= gameManager.damageOnCollide /**/ || true /*GOD mod*/)
                {
                    otherColor = other.gameObject.renderer.material.color;
                    DoSomeEffect();
                    gameManager.UsePoints();
                }
                else
                {
                    gameObject.AddComponent<Transformer>().direction = Vector3.back;
                    gameObject.GetComponent<Transformer>().Scale();
                    gameManager.GameOver();
                }
            }
        }
    }


    /**
     * Create some effect, when player collides with cubes, and loses points
     * Now it's a explosion of some number of lightballs
     */
    private void DoSomeEffect() {
        Transform parent = GameObject.Find("player").transform;//gameManager.GetPlayer().transform;
        int k = 10;
        for (int i = 0; i < k; i++)
        {
            GameObject lightObject = Instantiate(gameManager.lightPrefab) as GameObject;
            lightObject.transform.position = parent.position;
            //light.light.color = Colors.GetRandomColor();
            
            Vector3 pos = lightObject.transform.position;
            pos.x += Random.Range(-0.5f, 0.5f);
            pos.z += Random.Range(-0.5f, 0.5f);
            pos.y += 0.3f;

            
            lightObject.transform.position = pos;
            lightObject.light.color = otherColor;//parent.gameObject.renderer.material.color;
            lightObject.rigidbody.AddExplosionForce(400, parent.transform.position, 100);
            lightObject.rigidbody.AddExplosionForce(200, parent.transform.position + Vector3.forward*5, 100);
            lightObject.transform.parent = gameManager.cubeContainer.transform;
            lightObject.name = "Light " + lightObject.light.color + " " + i;
        }
    }

	/**
     * Start method
     */
	void Start () {
        gameManager = GameObject.Find("Main Camera").GetComponent<GameManager>();
	}
}
