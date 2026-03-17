using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private PlayerModel model;
    private float movHor;
    private float movVer;

    private void Awake()
    {
        model = GetComponent<PlayerModel>();
    }

    private void Update()
    {
        movHor = Input.GetAxis("Horizontal");
        movVer = Input.GetAxis("Vertical");

        Vector3 direction = new Vector3(movHor, 0, movVer);

        model.Movement(direction);

        if (movHor != 0 || movVer != 0) 
        {
            model.Rotate(direction);
        }
    }
}
