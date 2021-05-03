using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Tooltip("X respectivly Y are symmetrical")]
    [SerializeField] Vector2 clampMinMax;
    [SerializeField] float speed = 10f;
    [SerializeField] InputAction movement;

    void OnEnable() {
        movement.Enable();
    }

    void OnDisable() {
        movement.Disable();
    }

    void Update()
    {
        float horiz = movement.ReadValue<Vector2>().x;
        float verti = movement.ReadValue<Vector2>().y;

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
