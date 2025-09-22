// Sam Robichaud 
// NSCC Truro 2025
// This work is licensed under CC BY-NC-SA 4.0 (https://creativecommons.org/licenses/by-nc-sa/4.0/)

using System.Collections;
using UnityEngine;

public class MainMenuBallRespawner : MonoBehaviour
{

    
    [SerializeField] private GameObject ball;
    [SerializeField] private int StartingBalls = 120;


    [Header("New Initial Spawn Area")]
    [SerializeField] private Transform ballSpawnArea;

    public float BallSpawnDelay = 0.08f;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < StartingBalls; i++)
        {
            StartCoroutine(SpawnBallWithDelay(i * BallSpawnDelay));
        }




    }

    private void SpawnBallInRotatedArea()
    {
        Vector3 localPos = new Vector3(
            Random.Range(-0.5f, 0.5f),
            Random.Range(-0.5f, 0.5f),
            Random.Range(-0.5f, 0.5f)
        );

        Vector3 worldPos = ballSpawnArea.TransformPoint(localPos);

        GameObject go = Instantiate(ball, worldPos, transform.rotation, this.transform);
        go.GetComponentInChildren<MeshRenderer>().material.color = new Color(
            Random.Range(0f, 1f),
            Random.Range(0f, 1f),
            Random.Range(0f, 1f),
            Random.Range(0f, 1f)
        );


    }


   
    



    private void OnTriggerEnter(Collider other)
    {        
        Destroy(other.gameObject);

        SpawnBallInRotatedArea();
        
    }

    private IEnumerator SpawnBallWithDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        SpawnBallInRotatedArea();
    }





}
