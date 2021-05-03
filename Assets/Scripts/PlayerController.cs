using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] InputAction movement;
    [SerializeField] float speed = 10f;

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

        // Move the player by setting the local position
        transform.localPosition = new Vector3(
            transform.localPosition.x + (speed * horiz * Time.deltaTime),
            transform.localPosition.y + (speed * verti * Time.deltaTime),
            transform.localPosition.z
        );
    }
}
