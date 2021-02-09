using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class PipesSpawnManager : MonoBehaviour
{
    public  GameObject PipePrefab;
    public Transform PipesManager;
    public Queue<GameObject> pipesQueue;

    public  int minRangeBetween = 25;
    public int maxRangeBetween = 50;

    public Vector3 InitialSpawnPosition;
    private  Vector3 PrevPos;
    private int passedPipesCount = 0;

    public  readonly float spawnEdgeY = 17;

    public float spawnRate;
    public float spawnTimer;

    //public delegate GameObject SpawnDelegate(Vector3 position);
    //public delegate void UnSpawnDelegate(GameObject spawned);

    private void OnEnable()
    {
        //GameManager.OnGameStarted += OnGameStarted;
        GameManager.OnGameOverConfirmed += OnGameOverConfirmed;
        TapController.OnPlayerScored += OnPlayerScored;
    }

    private void OnDisable()
    {
        //GameManager.OnGameStarted += OnGameStarted;
        GameManager.OnGameOverConfirmed -= OnGameOverConfirmed;
        TapController.OnPlayerScored += OnPlayerScored;
    }

    void Start()
    {
        pipesQueue = new Queue<GameObject>();
        PrevPos = InitialSpawnPosition;
        //SpawnObject();
    }

    void Update()
    {
        spawnTimer += Time.deltaTime;
        if (spawnTimer >= spawnRate && pipesQueue.Count < 5)
        {
            SpawnObject();
            spawnTimer = 0;
        }
        else if (spawnTimer >= spawnRate)
        {
            spawnTimer = 0;
        }
    }

    private  float GetRandomX(float x)
    {
        var random = Random.Range(x + minRangeBetween, x + maxRangeBetween);
        var delta = random - x;
        //Debug.Log("x = " + x);
        //Debug.Log("delta = " + delta);
        //Debug.Log("random = " + random);

        return random;
    }

    private  float GetRandomY()
    {
        return Random.Range(-spawnEdgeY, spawnEdgeY);
    }

    private  Vector3 GetPosition()
    {
        var a = new Vector3(GetRandomX(PrevPos.x), GetRandomY(), 0);
        //Debug.Log("position = " + a);

        return a;
    }

    public void SpawnObject()
    {
        if (TapController.IsGameStarted)
        {
            var tmpObj = Instantiate(PipePrefab, GetPosition(), Quaternion.identity, PipesManager);
            //Debug.Log("tmpObj1 = " + tmpObj.transform.position);
            tmpObj.SetActive(true);
            //Debug.Log("tmpObj1 = " + tmpObj.transform.position);
            pipesQueue.Enqueue(tmpObj);
            //Debug.Log("tmpObj1 = " + tmpObj.transform.position);
            //Debug.Log("_______________");
            PrevPos = tmpObj.transform.position;
        }
    }

    public void UnSpawnObject(GameObject spawnedObj)
    {
        //Debug.Log("UnSpawning GameObject " + spawnedObj.name);
        spawnedObj.SetActive(false);
        Destroy(spawnedObj);
    }

    void OnGameOverConfirmed()
    {
        //Debug.Log(pipesQueue.Count);
        while (pipesQueue.Count != 0)
            UnSpawnObject(pipesQueue.Dequeue());
        PrevPos = InitialSpawnPosition;
        passedPipesCount = 0;
        //Debug.Log(pipesQueue.Count);
    }

    void OnPlayerScored()
    {
        passedPipesCount++;
        if (passedPipesCount > 2)
            UnSpawnObject(pipesQueue.Dequeue());
    }

    //void OnGameStarted()
    //{
    //    PrevPos = new Vector3(-10, 0, 0);
    //    SpawnObject();
    //}
}