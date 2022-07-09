using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Tooltip("Clamp player position in view. X respectivly Y are symmetrical")]
    [SerializeField] Vector2 clampMinMax;
    [Tooltip("Laser-objects having a particle effect")]
    [SerializeField] GameObject[] lasers;

    [Header("Movement")]
    [Tooltip("Ship-speed in view-plane (up/down & left/right)")]
    [SerializeField] float speed = 30f;
    [Tooltip("How fast the ship rotates when moving left/right")]
    [SerializeField] float rollSpeed = 1f;
    [Tooltip("Ships position based on position")]
    [SerializeField] float pitchPositionFactor = 0.05f;
    [Tooltip("Ships position based on movement")]
    [SerializeField] float pitchMovingFactor = 5f;
    [Tooltip("Ships roll based on position")]
    [SerializeField] float yawPositionFactor = 0.2f;
    [Tooltip("Ships roll based on movement")]
    [SerializeField] float rollMovingFactor = 10f;

    [Header("Input")]
    [SerializeField] InputAction movement;
    [SerializeField] InputAction shootInput;

    float horiz, verti;
    float deadTimer = 0f;
    bool dead = false;

    public void Kill() {
        dead = true;
    }

    void OnEnable() {
        movement.Enable();
        shootInput.Enable();
    }

    void OnDisable() {
        movement.Disable();
        shootInput.Disable();
    }

    void Update()
    {
        horiz = movement.ReadValue<Vector2>().x;
        verti = movement.ReadValue<Vector2>().y;
        
        if (!dead) {
            translate();
            rotate();
            shoot();
        } else {
            deadTimer += Time.deltaTime;
            if (deadTimer > 1f) {
                SceneManager.LoadScene("Level");
            }
        }
    }

    void rotate() {
        // Pitch when moving, and based on position
        float pitch = transform.localPosition.y * pitchPositionFactor - pitchMovingFactor * verti;
        // Yaw based on horizontal position
        float yaw = transform.localPosition.x * -yawPositionFactor;
        // Roll when moving horizontally
        float roll = -rollMovingFactor * horiz;


        Quaternion targetRotation = Quaternion.Euler(pitch, yaw, roll);
        transform.localRotation = Quaternion.RotateTowards(transform.localRotation, targetRotation, rollSpeed);
    }

    void translate()
    {
        // Clamp x- and y-position to prevent player moving out of screen
        float newX = Mathf.Clamp(
            transform.localPosition.x + (speed * horiz * Time.deltaTime),
            -clampMinMax.x,
            clampMinMax.x
        );
        float newY = Mathf.Clamp(
            transform.localPosition.y + (speed * verti * Time.deltaTime),
            -clampMinMax.y,
            clampMinMax.y
        );

        // Move the player by setting the local position
        transform.localPosition = new Vector3(newX, newY, transform.localPosition.z);
    }

    void shoot() {
        bool pressed = shootInput.IsPressed();
        foreach (GameObject laser in lasers) {
            var em =  laser.GetComponent<ParticleSystem>().emission;
            em.enabled = pressed;
        }
    }
}
