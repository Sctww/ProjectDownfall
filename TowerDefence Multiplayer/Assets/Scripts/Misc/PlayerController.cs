using UnityEngine;
using UnityEngine.Networking;

public class PlayerController : NetworkBehaviour
{
    #region Variables

    public Material mat;
    public Vector3 playerPosition = Vector3.zero;
    public int movementSpeed = 1;
    public int sensitivity = 5;
    int delay = 1;

    #endregion

    void Update()
    {
        if (!isLocalPlayer)
        {
            return;
        }
        PlayerMovement();
    }

    //Network Logic only for debug for now
    public override void OnStartLocalPlayer()
    {
        GetComponentInChildren<MeshRenderer>().material = mat;
    }

    void PlayerMovement()
    {
        //Putting the Controller Axis's into Variables
        float xAxis = Input.GetAxisRaw("Horizontal");
        float yAxis = Input.GetAxisRaw("Vertical");
        delay--;

        //Detecting Input and applying movement
        //Left and Right
        if (xAxis > 0.5f && delay <= 0)
        {
            delay = sensitivity;
            playerPosition.x += movementSpeed;
        }
        if (xAxis < -0.5f && delay <= 0)
        {
            delay = sensitivity;
            playerPosition.x += -movementSpeed;
        }

        //Up and Down
        if (yAxis > 0.5f && delay <= 0)
        {
            delay = sensitivity;
            playerPosition.z += movementSpeed;
        }
        if (yAxis < -0.5f && delay <= 0)
        {
            delay = sensitivity;
            playerPosition.z += -movementSpeed;
        }

        this.transform.position = playerPosition;
    }
}