using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileVisual : MonoBehaviour
{
    public Color gizCol = new Vector4();

    void OnDrawGizmos()
    {
        Gizmos.color = gizCol;
        Gizmos.DrawWireSphere(this.transform.position, 0.05f);
    }
}
