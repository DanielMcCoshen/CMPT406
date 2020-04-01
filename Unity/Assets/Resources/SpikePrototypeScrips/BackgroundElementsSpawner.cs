using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundElementsSpawner : MonoBehaviour
{
    public GameObject[] smokeElements;
    public Transform Camera;
    public Vector2[] speeds;
    public float[] spawnPositions;
    public int[] initialPositions;
    public int initialLayer;
    public Vector2 spawnVariance;

    void Start()
    {
        for(int x = 0; x < spawnPositions.Length; x++)
        {
            foreach( float initialPosition in initialPositions)
            {
                int index = Random.Range(0, smokeElements.Length);
                GameObject element = Instantiate(smokeElements[index], new Vector3(Camera.position.x + initialPosition + Random.Range(-1.0f, 1.0f),
                Camera.position.y + spawnPositions[x] + Random.Range(-0.5f, 0.5f), 0), Quaternion.identity);

                element.GetComponent<ScrollAcrossThenDestroy>().SetVelocityAndLayer(Random.Range(speeds[x].x, speeds[x].y), x+initialLayer);

                StartCoroutine(Spawner(Random.Range(0.1f, 2.1f), spawnPositions[x], x, speeds[x]));
            }
            
        }
    }

    private IEnumerator Spawner(float waitTime, float middlePosition, int layer, Vector2 speed)
    {
        yield return new WaitForSeconds(waitTime);
        int index = Random.Range(0, smokeElements.Length);
        GameObject element = Instantiate(smokeElements[index], new Vector3(Camera.position.x-15f, Camera.position.y + middlePosition + Random.Range(-0.5f, 0.5f), 0), Quaternion.identity);
        element.GetComponent<ScrollAcrossThenDestroy>().SetVelocityAndLayer(Random.Range(speed.x, speed.y), layer+ initialLayer);
        StartCoroutine(Spawner(Random.Range(spawnVariance.x, spawnVariance.y), middlePosition, layer, speed));
    }
}
