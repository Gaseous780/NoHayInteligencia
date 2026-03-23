using UnityEngine;

public class EnemyModel : MonoBehaviour
{
    [SerializeField] private float speed;

    [SerializeField] private float rotationSpeed = 33f;

    private GameObject player;

    //[SerializeField] private Material attackMaterial;
    //private Material defaultMaterial;

    //private MeshRenderer renderer;

    private void Awake()
    {
        //defaultMaterial = GetComponent<MeshRenderer>().material;
    }

    private void Start()
    {
        player = GameObject.Find("Player");

        //renderer = GetComponent<MeshRenderer>();
    }

    public void Pursuit() 
    {
        Vector3 direction = player.transform.position - transform.position;
        direction.y = 0;
        Vector3 moveDirection = direction.normalized;

        transform.position += moveDirection * speed * Time.deltaTime;

        transform.forward = Vector3.Lerp (transform.forward,moveDirection,Time.deltaTime * rotationSpeed);
    }

    public void Patrol()
    {
        transform.Rotate(0, rotationSpeed * Time.deltaTime, 0);
    }

    //public void StartAttack()
    //{
    //    renderer.material = attackMaterial;
    //    Attack();
    //}

    //public void Attack()
    //{
    //    Debug.Log("Empieza a atacar");
    //}

    //public void EndAttack()
    //{
    //    Debug.Log("Deja de atacar");
    //    renderer.material = defaultMaterial;
    //}
}
