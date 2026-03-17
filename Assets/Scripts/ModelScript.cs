using UnityEngine;

public class ModelScript : MonoBehaviour
{
    [SerializeField] private float speedRotation = 33f;

    public void Patrol()
    {
        transform.Rotate(0, speedRotation * Time.deltaTime, 0); 
    }
}
