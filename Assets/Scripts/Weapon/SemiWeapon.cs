using System.Collections;
using System.Collections.Generic;
using ScriptableObjects;
using Unity.VisualScripting;
using UnityEngine;

public class SemiWeapon : WeaponBase
{
    //[SerializeField] private Rigidbody bullet;

    [SerializeField] private BulletSO stats;

    [SerializeField] private Rigidbody Normal;
    [SerializeField] private Rigidbody Water;
    [SerializeField] private Rigidbody Fire;
    [SerializeField] private Rigidbody Light;
    [SerializeField] private Rigidbody Dark;

    [SerializeField] private float force = 50;

    protected override void Attack(float percent)
    {
        if (stats.BulletType == EProjectileType.Normal)
        {
            Rigidbody nl = Instantiate(Normal, transform.position, transform.rotation);
            nl.AddForce(Mathf.Max(percent, 0.1f) * force * transform.forward, ForceMode.Impulse);
            Destroy(nl.gameObject, 4);
        }
        else if (stats.BulletType == EProjectileType.Water)
        {
            Rigidbody wr = Instantiate(Water, transform.position, transform.rotation);
            wr.AddForce(Mathf.Max(percent, 0.1f) * force/3 * transform.forward, ForceMode.Impulse);
            Destroy(wr.gameObject, 4);
        }
        else if (stats.BulletType == EProjectileType.Fire)
        {
            Rigidbody fr = Instantiate(Fire, transform.position, transform.rotation);
            fr.AddForce(Mathf.Max(percent, 0.1f) * force*2 * transform.forward, ForceMode.Impulse);
            Destroy(fr.gameObject, 4);
        }
        else if (stats.BulletType == EProjectileType.Light)
        {
            Rigidbody lt = Instantiate(Light, transform.position, transform.rotation);
            lt.AddForce(Mathf.Max(percent, 0.1f) * force*3 * transform.forward, ForceMode.Impulse);
            Destroy(lt.gameObject, 4);
        }
        else if (stats.BulletType == EProjectileType.Dark)
        {
            Rigidbody dk = Instantiate(Dark, transform.position, transform.rotation);
            dk.AddForce(Mathf.Max(percent, 0.1f) * force/2 * transform.forward, ForceMode.Impulse);
            Destroy(dk.gameObject, 4);
        }

        //Rigidbody rb = Instantiate(Bullet, transform.position, transform.rotation);
        //rb.AddForce(Mathf.Max(percent, 0.1f) * force * transform.forward, ForceMode.Impulse);
        //Destroy(rb.gameObject, 4);

        GetComponent<AudioSource>().Play();
    }
}
