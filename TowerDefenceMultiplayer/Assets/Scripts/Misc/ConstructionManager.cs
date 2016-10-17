using UnityEngine;
using UnityEngine.Networking;
using System.Collections.Generic;

public class ConstructionManager : NetworkBehaviour
{
    #region Variables
    public bool basePlaced = false;

    public GameObject structure;
    public GameObject playerBase;

    #endregion

    void Start()
    {
        structure = playerBase;
    }

    void Update()
    {
        if (!isLocalPlayer)
        {
            return;
        }
        CmdPlaceStructure();
    }

    [Command]
    private void CmdPlaceStructure()
    {
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Joystick1Button0))
        {
            /*if (structure == playerBase && basePlaced == true)
            {
                structure = null;
            }*/
            GameObject buildStructure = Instantiate(structure, this.transform.position, Quaternion.identity) as GameObject;
            NetworkServer.Spawn(buildStructure);
        }
        if (structure == null)
        {
            Debug.Log("No structure selected. Tell the player to select a turret!");
        }
    }

    public void SetTowerType(GameObject tower)
    {
        if (basePlaced == true)
        {
            structure = tower;
        }
    }
}