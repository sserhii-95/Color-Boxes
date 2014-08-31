using UnityEngine;
using System.Collections;

/**
 * Event listener for colored lines
 */
public class WalEventListener : MonoBehaviour
{


    /**
     * Calculates events when gameObject collides with other objects
     */
    void OnTriggerEnter(Collider other)
    {
        GameObject otherGO = other.gameObject;
        if (otherGO.GetComponent<CubeColor>() != null)
        {
            otherGO.GetComponent<CubeColor>().Type = 0;
            otherGO.GetComponent<CubeColor>().ChangeColor(GetComponent<CubeColor>().ObjectColor);

        }

        if (otherGO.name.Equals("player"))
        {
            if (Random.Range(0, 10) < 1)
                if (Random.Range(0, 2) == 0)
                {
                    Transformer transformer = GetComponent<Transformer>();
                    transformer.direction = Vector3.forward;
                    transformer.translateSpeed = 20;
                }
                else
                {
                    otherGO.GetComponent<CubeColor>().Type = -1;
                }
        }
    }
}
