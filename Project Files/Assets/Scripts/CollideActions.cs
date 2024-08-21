using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CollideActions : MonoBehaviour
{
    public enum OwnerType { Enemy, Player, Missile, Item }
    public OwnerType ownerType;

    CollideExplosion collideExplosion;
    GameObject explosion;

    private void Start()
    {

    }

    private void OnCollisionEnter(Collision collision)
    {

        collideExplosion = GetComponent<CollideExplosion>();
        explosion = collideExplosion.explosion;
        if (ownerType == OwnerType.Enemy)
        {
            collideExplosion.OnCollisionEnter(collision);
            if (collision.collider.tag == "PlayerBullet")
            {
                GameObject player = GameObject.FindWithTag("Player");
                player.GetComponent<PlayerScript>().score += 2;
            }
        }
        else if (ownerType == OwnerType.Player)
        {

        }
        else if (ownerType == OwnerType.Missile)
        {

        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (ownerType == OwnerType.Item)
        {
            AudioSource source = GetComponent<AudioSource>();
            
            if (other.tag == "Player")
            {
                source.Play();
                Destroy(gameObject, 0.6f);
            }
        }
    }

}
