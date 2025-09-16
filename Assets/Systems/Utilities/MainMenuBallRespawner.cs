using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Sam Robichaud 
// NSCC Truro 2022

public class MainMenuBallRespawner : MonoBehaviour
{

    
    [SerializeField] private GameObject ball;
    [SerializeField] private int StartingBalls = 120;


    [Header("Initial Spawn Area")]
    [SerializeField] private int InitialSpawnXmin = -25;
    [SerializeField] private int InitialSpawnXmax = 25;
    [SerializeField] private int InitialSpawnYmin = 2;
    [SerializeField] private int InitialSpawnYmax = 6;
    [SerializeField] private int InitialSpawnZmin = 28;
    [SerializeField] private int InitialSpawnZmax = 34;

    [Header("Respawn Area")]
    [SerializeField] private int respawnXmin = -25;
    [SerializeField] private int RespawnXmax = 25;
    [SerializeField] private int RespawnYmin = 7;
    [SerializeField] private int RespawnYmax = 10;
    [SerializeField] private int RespawnZmin = 40;
    [SerializeField] private int RespawnZmax = 45;


   

    // Start is called before the first frame update
    void Start()
    {
        // Initial Spawn      

        
        for (int i = 0; i < StartingBalls; i++)
        {
            SpawnBall(InitialSpawnXmin, InitialSpawnXmax, InitialSpawnYmin, InitialSpawnYmax, InitialSpawnZmin, InitialSpawnZmax);

             //int RandomX = Random.Range(-25, 25);
             //int RandomY = Random.Range(4, 125);
             //int RandomZ = Random.Range(-30, 10);

             //GameObject go = Instantiate(ball, new Vector3(RandomX, RandomY, RandomZ), transform.rotation);

             //go.GetComponentInChildren<MeshRenderer>().material.color = new Color(Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f));
        }    
        
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(new Vector3(InitialSpawnXmin + (InitialSpawnXmax - InitialSpawnXmin) / 2, InitialSpawnYmin + (InitialSpawnYmax - InitialSpawnYmin) / 2, InitialSpawnZmin + (InitialSpawnZmax - InitialSpawnZmin) / 2), new Vector3(InitialSpawnXmax - InitialSpawnXmin, InitialSpawnYmax - InitialSpawnYmin, InitialSpawnZmax - InitialSpawnZmin));
        
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(new Vector3(respawnXmin + (RespawnXmax - respawnXmin) / 2, RespawnYmin + (RespawnYmax - RespawnYmin) / 2, RespawnZmin + (RespawnZmax - RespawnZmin) / 2), new Vector3(RespawnXmax - respawnXmin, RespawnYmax - RespawnYmin, RespawnZmax - RespawnZmin));
    }
    



    private void OnTriggerEnter(Collider other)
    {        
        Destroy(other.gameObject);

        SpawnBall(respawnXmin, RespawnXmax, RespawnYmin, RespawnYmax, RespawnZmin, RespawnZmax);

        //int RandomX = Random.Range(SpawnXmin, SpawnXmax);
        //int RandomY = Random.Range(SpawnYmin, SpawnYmax);
        //int RandomZ = Random.Range(SpawnZmin, SpawnZmax);                

        //GameObject go = Instantiate(ball, new Vector3(RandomX, RandomY, RandomZ), transform.rotation, this.transform);

        //go.GetComponentInChildren<MeshRenderer>().material.color = new Color(Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f));
    }

    private void SpawnBall(int Xmin, int Xmax, int Ymin, int Ymax, int Zmin, int Zmax)
    {
        int RandomX = Random.Range(Xmin, Xmax);
        int RandomY = Random.Range(Ymin, Ymax);
        int RandomZ = Random.Range(Zmin, Zmax);

        GameObject go = Instantiate(ball, new Vector3(RandomX, RandomY, RandomZ), transform.rotation, this.transform);

        go.GetComponentInChildren<MeshRenderer>().material.color = new Color(Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f));
    }


}
