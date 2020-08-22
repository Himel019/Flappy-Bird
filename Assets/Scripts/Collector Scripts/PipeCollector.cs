using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeCollector : MonoBehaviour
{
    private GameObject[] pipeHolders;

    [SerializeField]
    private float distanceMin = 2.5f;
    [SerializeField]
    private float distanceMax = 7f;
    private float lastPipeX;
    private float lastPipeY;
    [SerializeField]
    private float pipeMinY = -1.83f;
    [SerializeField]
    private float pipeMaxY = 2.54f;
    [SerializeField]
    private float minCoolDownX = 10f;
    [SerializeField]
    private float maxCoolDownX = 20f;


    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    void Awake()
    {
        pipeHolders = GameObject.FindGameObjectsWithTag("PipeHolder");

        
        for(int i = 0; i < pipeHolders.Length; i++) {
            Vector3 temp = pipeHolders[i].transform.position;
            temp.y = GeneratePipeY(lastPipeY);
            lastPipeY = temp.y;
            pipeHolders[i].transform.position = temp;
        }

        lastPipeX = pipeHolders[0].transform.position.x;

        for(int i = 1; i < pipeHolders.Length; i++){
            if(lastPipeX < pipeHolders[i].transform.position.x) {
                lastPipeX = pipeHolders[i].transform.position.x;
            }
        }

        StartCoroutine(CoolDownCoroutine());
    }

    /// Sent when another object enters a trigger collider attached to this object (2D physics only).
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "PipeHolder") {
            
            Vector3 temp = other.transform.position;

            temp.x = lastPipeX + Random.Range(distanceMin, distanceMax);
            temp.y = GeneratePipeY(lastPipeY);
            
            other.transform.position = temp;
            lastPipeX = temp.x;
        }
    }

    private IEnumerator CoolDownCoroutine() {
        yield return new WaitForSecondsRealtime(Random.Range(15f, 25f));

        lastPipeX += Random.Range(minCoolDownX, maxCoolDownX);

        StartCoroutine(CoolDownCoroutine());
    }

    private float GeneratePipeY(float lastPipeY) {
        float newY = Random.Range(lastPipeY - 2f, lastPipeY + 2f);

        if(newY < pipeMaxY && newY > pipeMinY){
            return newY;
        }

        return Random.Range(pipeMinY, pipeMaxY);
    }
}
