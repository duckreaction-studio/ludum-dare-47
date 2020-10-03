using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cow : MonoBehaviour
{
    [SerializeField]
    private ParticleSystem explosion;
    [SerializeField]
    private Transform body;

    public void Awake()
    {
        explosion.gameObject.SetActive(false);
    }

    public void Die()
    {
        body.gameObject.SetActive(false);
        explosion.gameObject.SetActive(true);
        explosion.loop = false;
        explosion.Play();
    }
}
