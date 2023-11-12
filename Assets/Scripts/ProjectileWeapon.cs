using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileWeapon : WeaponBase
{
    [SerializeField] private Rigidbody myBullet;
    [SerializeField] private Rigidbody myBullet2;
    [SerializeField] private float force = 50;

    protected override void Attack(float percent)
    {
        Rigidbody rb = Instantiate(percent>0.5f?myBullet2:myBullet, transform.position, transform.rotation);
        rb.AddForce(Mathf.Max(percent, 0.1f) * force * transform.position, ForceMode.Impulse);
        Destroy(rb.gameObject, 4);
    }
}
