using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public InputAction moveAction;
    public float speed = 5f;
    public float speedRotation = 3f;
    public Rigidbody rb;

    private void Awake()
    {
        moveAction.Enable(); //activa input
    }
    void RotatePlayer(Vector3 direction)
    {
        transform.forward = Vector3.Lerp(transform.forward, direction, speedRotation * Time.deltaTime);
        rb.linearVelocity = direction * speed;
    }
    private void Update()
    {
        Vector2 moveInput = moveAction.ReadValue<Vector2>(); //lee el input del movimiento segn el vector 2 (up,down,right,left)
        Vector3 move = new Vector3(moveInput.x, 0, moveInput.y); //lo pasa a vector 3 para que este en xyz 
        transform.position += move * speed* Time.deltaTime; //mueve pos
        if (move != Vector3.zero)
        {
            RotatePlayer(move);
        }
    }
}
