using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    private Rigidbody physControl;
    private AudioSource thrustNoise;
    
    [SerializeField] private float thrustMag = 1000f;
    [SerializeField] private float rotationMag = 1000f;
    [SerializeField] private AudioClip mainEngine;

    [SerializeField] private ParticleSystem leftBoosterParticles;
    [SerializeField] private ParticleSystem rightBoosterParticles;
    [SerializeField] private ParticleSystem mainBoosterParticles;

    // Start is called before the first frame update
    void Start()
    {
        physControl = GetComponent<Rigidbody>();
        thrustNoise = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        ProcessThrust();
        ProcessRotation();
    }

    void ProcessThrust() {
        if (Input.GetKey(KeyCode.Space)) {
            StartThrusting();
        }
        else
        {
            StopThrusting();
        }
    }

    void ProcessRotation() {
        if (Input.GetKey(KeyCode.A))
        {
            RotateLeft();
        }
        else if (Input.GetKey(KeyCode.D))
        {
            RotateRight();
        }
        else
        {
            StopRotating();
        }
    }

    private void StopThrusting()
    {
        mainBoosterParticles.Stop();
        thrustNoise.Stop();
    }

    private void StartThrusting() {
        if (!mainBoosterParticles.isPlaying) {
            mainBoosterParticles.Play();
        }
        if (!thrustNoise.isPlaying) {
            thrustNoise.PlayOneShot(mainEngine);
        }
        physControl.AddRelativeForce(Vector3.up * thrustMag * Time.deltaTime);
    }

    private void RotateLeft()
    {
        if (!rightBoosterParticles.isPlaying)
        {
            rightBoosterParticles.Play();
        }
        ApplyRotation(rotationMag);
        leftBoosterParticles.Stop();
    }
    private void RotateRight()
    {
        if (!leftBoosterParticles.isPlaying)
        {
            leftBoosterParticles.Play();
        }
        ApplyRotation(-rotationMag);
        rightBoosterParticles.Stop();
    }

    
    private void StopRotating()
    {
        leftBoosterParticles.Stop();
        rightBoosterParticles.Stop();
    }

    void ApplyRotation(float rotationThisFrame)
    {
        physControl.freezeRotation = true; // Freezing physics rotation to override with manual controls.
        transform.Rotate(Vector3.forward * rotationThisFrame * Time.deltaTime);
        physControl.freezeRotation = false;
    }
}
