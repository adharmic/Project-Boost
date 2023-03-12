using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    private Rigidbody physControl;
    
    [SerializeField] private float thrustMag = 1000f;
    [SerializeField] private float rotationMag = 1000f;

    // Start is called before the first frame update
    void Start()
    {
        physControl = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        ProcessThrust();
        ProcessRotation();
    }

    void ProcessThrust() {
        if (Input.GetKey(KeyCode.Space)) {
            physControl.AddRelativeForce(Vector3.up * thrustMag * Time.deltaTime);
        }
    }

    void ProcessRotation() {
        if (Input.GetKey(KeyCode.A))
        {
            ApplyRotation(rotationMag);
        }
        else if (Input.GetKey(KeyCode.D)) {
            ApplyRotation(-rotationMag);
        }
    }

    void ApplyRotation(float rotationThisFrame)
    {
        physControl.freezeRotation = true; // Freezing physics rotation to override with manual controls.
        transform.Rotate(Vector3.forward * rotationThisFrame * Time.deltaTime);
        physControl.freezeRotation = false;
    }
}
