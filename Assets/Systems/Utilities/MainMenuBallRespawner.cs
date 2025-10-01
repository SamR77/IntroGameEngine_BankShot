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

        Color saturatedColor = Color.HSVToRGB(
            Random.Range(0f, 1f), // Hue: full spectrum
            Random.Range(0.8f, 1f), // Saturation: high values only
            Random.Range(0.8f, 1f)  // Value (brightness): high values only
        );
        
        saturatedColor.a = 1f; // Optional: set alpha to fully opaque

        go.GetComponentInChildren<MeshRenderer>().material.color = saturatedColor;



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
