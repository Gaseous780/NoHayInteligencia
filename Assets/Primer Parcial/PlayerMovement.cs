using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public InputAction moveAction;
    public float speed = 5f;
    public float speedRotation = 10f;

    private void Awake()
    {
        moveAction.Enable();
    }

    void RotatePlayer(Vector3 direction)
    {
        if (direction == Vector3.zero) return;
        float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
        float angle = Mathf.MoveTowardsAngle(transform.eulerAngles.y,targetAngle,speedRotation * 500f * Time.deltaTime);
        transform.rotation = Quaternion.Euler(0, angle, 0);
    }

    private void Update()
    {
        Vector2 moveInput = moveAction.ReadValue<Vector2>();
        Vector3 move = new Vector3(moveInput.x, 0, moveInput.y);
        transform.position += move.normalized * speed * Time.deltaTime;
        if (move != Vector3.zero)
        {
            RotatePlayer(move);
        }
    }
}