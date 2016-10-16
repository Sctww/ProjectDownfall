using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Unit : NetworkBehaviour {

    public int tileX;
    public int tileY;
    public List<TileMap.Node> currentPath = null;

}
