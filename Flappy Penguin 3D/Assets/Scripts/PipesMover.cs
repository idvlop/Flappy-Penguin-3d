using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipesMover : MonoBehaviour
{
    PipesSpawnManager spawnManager;
    Rigidbody PipePool;
    public Vector3 StandartVelocity;
    public Vector3 StandartInitPosition;

    private void Start()
    {
        PipePool = GetComponent<Rigidbody>();
        spawnManager = GameObject.Find("PipesSpawnManager").GetComponent<PipesSpawnManager>();
    }

    private void OnEnable()
    {
        GameManager.OnGameStarted += OnGameStarted;
    }

    private void OnDisable()
    {
        GameManager.OnGameStarted += OnGameStarted;
    }

    public void OnGameStarted()
    {
        transform.position = StandartInitPosition;
        PipePool.velocity = StandartVelocity;
    }
}
