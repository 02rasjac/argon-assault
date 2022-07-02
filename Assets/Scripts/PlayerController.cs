using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Tooltip("X respectivly Y are symmetrical")]
    [SerializeField] Vector2 clampMinMax;
    [SerializeField] float speed = 30f;

    [SerializeField] float pitchPositionFactor = 0.05f;
    [SerializeField] float pitchMovingFactor = 5f;
    [SerializeField] float yawPositionFactor = 0.2f;
    [SerializeField] float rollMovingFactor = 10f;

    [SerializeField] InputAction movement;

    float horiz, verti;
    float tPitch = 0.5f, tRoll = 0.5f;

    void OnEnable() {
        movement.Enable();
    }

    void OnDisable() {
        movement.Disable();
    }

    void Update()
    {
        horiz = movement.ReadValue<Vector2>().x;
        verti = movement.ReadValue<Vector2>().y;
        
        translate();
        rotate();
    }

    void rotate() {
        // Pitch when moving, and based on position
        float pitch = transform.localPosition.y * pitchPositionFactor - pitchMovingFactor * verti;
        // Yaw based on horizontal position
        float yaw = transform.localPosition.x * -yawPositionFactor;
        // Roll when moving horizontally
        float roll = -rollMovingFactor * horiz;


        transform.localRotation = Quaternion.Euler(pitch, yaw, roll);
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
}
