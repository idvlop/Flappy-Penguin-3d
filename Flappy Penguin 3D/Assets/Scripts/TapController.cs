using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

[RequireComponent(typeof(Rigidbody))]
public class TapController : MonoBehaviour
{
    public delegate void PlayerDelegate();
    public static event PlayerDelegate OnPlayerDied;
    public static event PlayerDelegate OnPlayerScored;

    public AudioSource TapSound;

    //public static Vector3 InitialVelocity = new Vector3(10, 0, 0);
    public Vector3 initialVelocity = new Vector3(0, 0, 0);
    public Vector3 initialPosition;
    public Quaternion initialRotation;
    public float tapForce = 4000;
    public float smoothRotation = 2.5f;

    public static Rigidbody birdRigidbody;
    public static bool IsGameStarted = false;

    Quaternion downRotation;
    Quaternion upRotation;

    void Start()
    {
        birdRigidbody = GetComponent<Rigidbody>();
        downRotation = Quaternion.Euler(90, 90, 0);
        upRotation = Quaternion.Euler(-25, 90, 0);
        birdRigidbody.velocity = initialVelocity;
        birdRigidbody.ResetCenterOfMass();
        // transform.position = initialPosition;
    }

    void Update()
    {
        
        if(Input.GetMouseButtonDown(0) && IsGameStarted)
        {
            TapSound.Play();
            transform.rotation = Quaternion.Lerp(transform.rotation, upRotation, 0.95f);
            birdRigidbody.velocity = initialVelocity;
            birdRigidbody.AddForce(Vector2.up * tapForce, ForceMode.Force);
        }
        transform.rotation = Quaternion.Lerp(transform.rotation, downRotation, smoothRotation * Time.deltaTime);
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Score Zone"))
        {
            other.gameObject.tag = "Passed Score Zone";
            OnPlayerScored();
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Dead Zone"))
        {
            //birdRigidbody.freezeRotation = true;
            OnPlayerDied();
        }
    }

    void OnEnable()
    {
        GameManager.OnGameStarted += OnGameStarted;
        GameManager.OnGameOverConfirmed += OnGameOverConfirmed;
    }

    void OnDisable()
    {
        GameManager.OnGameStarted -= OnGameStarted;
        GameManager.OnGameOverConfirmed -= OnGameOverConfirmed;
    }

    void OnGameStarted()
    {
        birdRigidbody.velocity = initialVelocity;
        transform.position = initialPosition;
        transform.rotation = initialRotation;
        Time.timeScale = 1;
        IsGameStarted = true;
    }


    void OnGameOverConfirmed()
    {
        birdRigidbody.velocity = initialVelocity;
        transform.position = initialPosition;
        transform.rotation = initialRotation;
        Time.timeScale = 1;
        IsGameStarted = true;
    }
}
