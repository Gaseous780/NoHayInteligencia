using UnityEngine;

public class PlayerModel : MonoBehaviour
{
    [SerializeField]private Rigidbody rb;
    [SerializeField] private float speed = 33;
    [SerializeField] private float speedRotation = 11;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void Movement(Vector3 direction) 
    { 
        rb.linearVelocity = direction * speed;
    }

    public void Rotate(Vector3 direction)  
    { 
        transform.forward = Vector3.Lerp (transform.forward,direction, speedRotation * Time.deltaTime);
    }
}
