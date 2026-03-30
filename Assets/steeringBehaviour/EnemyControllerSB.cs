using UnityEngine;

public class EnemyControllerSB : MonoBehaviour
{
    public enum Mode
    {
        Seek,
        Flee,
        Arrive,
        Persue
    }
    [SerializeField] private Mode mode;
    [SerializeField] private Transform player;
    private Rigidbody playerRB;
    [SerializeField] float speed = 3f;
    [SerializeField] float rotationSpeed = 5f;

    private void Awake()
    {
        playerRB = GetComponent<Rigidbody>();
    }
    private void Update()
    {
        Vector3 dir = Vector3.zero;
        switch (mode)
        {
            case Mode.Seek:
                dir = SteeringBehaviours.Seek(transform, player.position);
                break;
            case Mode.Flee:
                dir = SteeringBehaviours.Flee(transform, player.position);
                break;
            case Mode.Arrive:
                dir = SteeringBehaviours.Arrive(transform, player.position, 5f);
                break;
            case Mode.Persue:
                dir = SteeringBehaviours.Persue(transform, player,playerRB, 2f);
                break ;
        }
        Move(dir);

    }
    private void Move(Vector3 dir)
    {
        transform.position += dir * speed * Time.deltaTime;

        if(dir!= Vector3.zero)
        {
            transform.forward=Vector3.Lerp(transform.forward, dir, Time.deltaTime * rotationSpeed);
        }
    }
}
