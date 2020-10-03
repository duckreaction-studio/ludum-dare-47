using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, IPlayer
{
    public Vector3 GetPosition()
    {
        return transform.position;
    }
}
