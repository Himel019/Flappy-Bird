using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeCollector : MonoBehaviour
{
    private GameObject[] pipeHolders;

    [SerializeField]
    private float distanceMin = 2.5f;
    [SerializeField]
    private float distanceMax = 4f;
    private float lastPipeX;
    [SerializeField]
    private float pipeMinY = -1.83f;
    [SerializeField]
    private float pipeMaxY = 2.54f;


    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    void Awake()
    {
        pipeHolders = GameObject.FindGameObjectsWithTag("PipeHolder");

        for(int i = 0; i < pipeHolders.Length; i++) {
            Vector3 temp = pipeHolders[i].transform.position;
            temp.y = Random.Range(pipeMinY, pipeMaxY);
            pipeHolders[i].transform.position = temp;
        }

        lastPipeX = pipeHolders[0].transform.position.x;

        for(int i = 1; i < pipeHolders.Length; i++){
            if(lastPipeX < pipeHolders[i].transform.position.x) {
                lastPipeX = pipeHolders[i].transform.position.x;
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
        if(other.tag == "PipeHolder") {
            Vector3 temp = other.transform.position;

            temp.x = lastPipeX + Random.Range(distanceMin, distanceMax);
            temp.y = Random.Range(pipeMinY, pipeMaxY);
            
            other.transform.position = temp;
            lastPipeX = temp.x;
        }
    }
}
