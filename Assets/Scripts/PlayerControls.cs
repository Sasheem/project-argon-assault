using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControls : MonoBehaviour
{
    [Header("General Setup Settings")]
    [SerializeField] InputAction movement;
    [SerializeField] InputAction fire;
    [Tooltip("How fast ship moves up and down based upon player input")] 
    [SerializeField] float controlSpeed = 10f;
    [Tooltip("How far player can move horizontally")]
    [SerializeField] float xRange = 5f;
    [Tooltip("How far player can move vertically")]
    [SerializeField] float yRange = 3.5f;

    [Header("Laser Gun Array")]
    [Tooltip("Add all player lasers here")]
    [SerializeField] GameObject[] lasers;

    [Header("Screen Position Based Tuning")]
    [SerializeField] float positionPitchFactor = -2f;
    [SerializeField] float positionYawFactor = -5f;

    [Header("Player Input Based Tuning")]
    [SerializeField] float controlPitchFactor = -15f;
    [SerializeField] float controlRollFactor = -20f;

    float yThrow, xThrow;
    
    // must do for new input system
    private void OnEnable() {
        movement.Enable();
        fire.Enable();
    }

    // must do for new input system
    private void OnDisable() {
        movement.Disable();
        fire.Disable();
    }
    
    // Update is called once per frame
    void Update()
    {
        ProcessTranslation();
        ProcessRotation();
        ProcessFiring();
    }

    void ProcessRotation() {
        float pitchDueToPosition = transform.localPosition.y * positionPitchFactor;
        float pitchDueToControlThrow = yThrow * controlPitchFactor;

        float pitch = pitchDueToPosition + pitchDueToControlThrow;
        float yaw = transform.localPosition.x * positionYawFactor;
        float roll = xThrow * controlRollFactor;
        
        transform.localRotation = Quaternion.Euler(pitch, yaw, roll);
    }

    void ProcessTranslation() {
        xThrow = movement.ReadValue<Vector2>().x;
        yThrow = movement.ReadValue<Vector2>().y;

        float xOffset = xThrow * Time.deltaTime * controlSpeed;
        float rawXPos = transform.localPosition.x + xOffset;
        float clampedXPos = Mathf.Clamp(rawXPos, -xRange, xRange);

        float yOffset = yThrow * Time.deltaTime * controlSpeed;
        float rawYPos = transform.localPosition.y + yOffset;
        float clampedYPos = Mathf.Clamp(rawYPos, -yRange, yRange);
        
        transform.localPosition = new Vector3 (clampedXPos, clampedYPos, transform.localPosition.z);
    }

    void ProcessFiring() {
        /*
            if pushing fire button
                then print "shooting"
                else don't print "shooting"
        */
        // if (Input.GetButton("Fire1")) - OLD INPUT SYSTEM
        if (fire.ReadValue<float>() > 0.5) {
            // ActivateLasers();
            SetLasersActive(true);
        } else {
            // DeactivateLasers();
            SetLasersActive(false);
        }
    }

    void SetLasersActive(bool isActive) {
        foreach (GameObject laser in lasers) {
            var emissionModule = laser.GetComponent<ParticleSystem>().emission;
            emissionModule.enabled = isActive;
        }
    }

    // void ActivateLasers() {
    //     // for each laser that exist, turn them on (activate)
    //     foreach (GameObject laser in lasers) {
    //         var emissionModule = laser.GetComponent<ParticleSystem>().emission;
    //         emissionModule.enabled = true;
    //     }
    // }

    // void DeactivateLasers() {
    //     // for each laser that exist, turn them off (deactivate)
    //     foreach (GameObject laser in lasers) {
    //         var emissionModule = laser.GetComponent<ParticleSystem>().emission;
    //         emissionModule.enabled = false;
    //     }
    // }
}
