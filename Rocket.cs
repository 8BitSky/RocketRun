//using System;
//using System.Collections;
//using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Rocket : MonoBehaviour
{
    //References
    Rigidbody rigidBody;
    AudioSource audioSource;
    [SerializeField] LevelCoordinator levelCoordinator;

    [SerializeField] float rcsThrust = 100f;
    [SerializeField] float mainThrust = 100f;
    [SerializeField] float nextLevelTimer = 1f;
    private bool collisionsDisabled = false;

    [SerializeField] AudioClip mainEngine;
    [SerializeField] AudioClip winLevel;
    [SerializeField] AudioClip returnLevel;

    [SerializeField] ParticleSystem mainEngineParticles;
    [SerializeField] ParticleSystem winLevelParticles;
    [SerializeField] ParticleSystem returnLevelParticles;

    enum State { Alive, Dying, Transcending }
    State state = State.Alive;


    // Use this for initialization
    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (state == State.Alive)
        {
            RespondToThrust();
            RespondToRotate();
        }
        if (Debug.isDebugBuild) { 
        RespondToDebug();
        }
    }

    private void RespondToDebug() //Only Active for Dev Builds
    {
        if(Input.GetKeyDown(KeyCode.K))
        {
            collisionsDisabled = !collisionsDisabled;
        }
    }

    private void RespondToRotate()
    {
        rigidBody.angularVelocity = Vector3.zero; //Removes rotation due to physcis

        float rotationThisFrame = rcsThrust * Time.deltaTime;

        if (Input.GetKey(KeyCode.A))
        {

            transform.Rotate(Vector3.forward * rotationThisFrame);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(-Vector3.forward * rotationThisFrame);
        }
    }

    private void RespondToThrust()
    {

        if (Input.GetKey(KeyCode.Space))
        {
            ApplyThrust();
        }
        else
        {
            audioSource.Stop();
            mainEngineParticles.Stop();
        }
    }

    private void ApplyThrust()
    {
        rigidBody.AddRelativeForce(Vector3.up * mainThrust * Time.deltaTime);
        if (!audioSource.isPlaying)
        {
            audioSource.PlayOneShot(mainEngine);
        }
        mainEngineParticles.Play();
    }

    void OnCollisionEnter(Collision collision)
    {
            if (state != State.Alive || collisionsDisabled) { return; }
            switch (collision.gameObject.tag)
            {
                case "Friendly":
                    //Do Nothing for now
                    break;
                case "Finish":
                    StartSuccessSequence();
                    break;
                default:
                    StartDeathSequence();
                    break;
            }
        
    }

    private void StartSuccessSequence()
    {
        state = State.Transcending;
        audioSource.Stop();
        audioSource.PlayOneShot(winLevel);
        winLevelParticles.Play();
        Invoke("LoadNextLevel", nextLevelTimer);
    }

    private void StartDeathSequence()
    {
        state = State.Dying;
        audioSource.Stop();
        audioSource.PlayOneShot(returnLevel);
        returnLevelParticles.Play();
        Invoke("LoadFirstLevel", nextLevelTimer);
    }

    // I want the following to be moved outside of this script and call out
    private void LoadNextLevel()
    {
        levelCoordinator.LoadNextLevel();
    }

    private void LoadFirstLevel()
    {
        levelCoordinator.LoadFirstLevel();
    }

}