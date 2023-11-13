using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SemiWeapon : WeaponBase
{
    [SerializeField] private Rigidbody bullet;
    [SerializeField] private float force = 50;

    protected override void Attack(float percent)
    {
        Rigidbody rb = Instantiate(bullet, transform.position, transform.rotation);
        rb.AddForce(Mathf.Max(percent, 0.1f) * force * transform.forward, ForceMode.Impulse);
        Destroy(rb.gameObject, 4);

        GetComponent<AudioSource>().Play();
    }
}
