using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundCollector : MonoBehaviour
{
    private GameObject[] backgrounds;
    private GameObject[] grounds;

    private float lastBackgroundX;
    private float lastGroundX;

    // Start is called before the first frame update
    void Start()
    {
        backgrounds = GameObject.FindGameObjectsWithTag("Background");
        grounds = GameObject.FindGameObjectsWithTag("Ground");

        lastBackgroundX = backgrounds[0].transform.position.x;
        lastGroundX = grounds[0].transform.position.x;

        for(int i = 0; i < backgrounds.Length; i++) {
            if(lastBackgroundX < backgrounds[i].transform.position.x) {
                lastBackgroundX = backgrounds[i].transform.position.x;
            }
        }

        for(int i = 0; i < grounds.Length; i++) {
            if(lastGroundX < grounds[i].transform.position.x) {
                lastGroundX = grounds[i].transform.position.x;
            }
        }
    }

    /// <summary>
    /// Sent when another object enters a trigger collider attached to this
    /// object (2D physics only).
    /// </summary>
    /// <param name="other">The other Collider2D involved in this collision.</param>
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Background") {
            Vector3 temp = other.transform.position;
            float width = ((BoxCollider2D)other).size.x;

            temp.x = lastBackgroundX + width;
            other.transform.position = temp;

            lastBackgroundX = temp.x;
        } else if(other.tag == "Ground") {
            Vector3 temp = other.transform.position;
            float width = ((BoxCollider2D)other).size.x;

            temp.x = lastGroundX + width;
            other.transform.position = temp;

            lastGroundX = temp.x;
        }
    }
}
