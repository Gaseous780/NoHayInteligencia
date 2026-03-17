using UnityEngine;

public class EnemyModel : MonoBehaviour
{
    [SerializeField] private float speed;

    [SerializeField] private float rotationSpeed = 33f;

    private GameObject player;

    private void Start()
    {
        player = GameObject.Find("Player");
    }

    public void Pursuit() 
    {
        Vector3 direction = player.transform.position - transform.position;
        direction.y = 0;
        Vector3 moveDirection = direction.normalized;

        transform.position += moveDirection * speed * Time.deltaTime;

        transform.forward = Vector3.Lerp (transform.forward,moveDirection,Time.deltaTime * rotationSpeed);

        Debug.Log("Alle estamos");
    }

    public void Patrol()
    {
        transform.Rotate(0, rotationSpeed * Time.deltaTime, 0);
    }
}
