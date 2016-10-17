using UnityEngine;
public class Base : Structure {

    #region Variables
    private ConstructionManager conman;
    public GameObject playerSphere;
    public Material mat;
    #endregion

    public override void Start()
    {
        conman = FindObjectOfType<ConstructionManager>();
        conman.basePlaced = true;
    }
}