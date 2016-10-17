using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnTable : MonoBehaviour
{

    [Range(0, 30)]
    public float turnspeed = 10f;

    void Update()
    {
        this.transform.Rotate(0, turnspeed * Time.deltaTime, 0);
    }
}