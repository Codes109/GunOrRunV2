using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollideExplosion : MonoBehaviour
{
    public GameObject explosion;

    public void OnCollisionEnter(Collision collision)
    {
        AudioSource source;
        GameObject explosionInstance;
        explosionInstance = Instantiate(explosion, transform.position, transform.rotation);
        source = explosionInstance.GetComponent<AudioSource>();
        source.Play();
        Destroy(gameObject);
        Destroy(explosionInstance, 3);
    }
}
