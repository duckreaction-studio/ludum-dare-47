using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class FakePlayer : MonoBehaviour, IPlayer
{
    CowAI testCow;

    [Inject]
    public void Construct(CowAI cow)
    {
        testCow = cow;
    }
    public Vector3 GetPosition()
    {
        return transform.position;
    }

    [ContextMenu("Test nav mesh")]
    public void TestNavMesh()
    {
        testCow.StartFollowPlayer();
    }
}
