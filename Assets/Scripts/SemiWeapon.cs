using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SemiWeapon : WeaponBase
{
    [SerializeField] private Rigidbody bullet;
    [SerializeField] private float force = 50;

    protected override void Attack(float percent)
    {
        Vector3 playerForward = transform.forward;

        Rigidbody rb = Instantiate(bullet, transform.position, transform.rotation);
        rb.AddForce(Mathf.Max(percent, 0.1f) * force * playerForward, ForceMode.Impulse);
        Destroy(rb.gameObject, 4);
    }
}
