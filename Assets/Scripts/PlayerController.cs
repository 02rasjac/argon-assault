using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] InputAction movement;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void OnEnable() {
        movement.Enable();
    }

    void OnDisable() {
        movement.Disable();
    }

    // Update is called once per frame
    void Update()
    {
        float horiz = movement.ReadValue<Vector2>().x;
        float verti = movement.ReadValue<Vector2>().y;

        Debug.Log(horiz);
        Debug.Log(verti);
    }
}
