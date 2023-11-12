using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BurstWeapon : WeaponBase
{
    [SerializeField] private Rigidbody bullet;
    [SerializeField] private float force = 50;
    [SerializeField] private float burstCount = 3;
    [SerializeField] private float burstDelay = 0.5f;
    private WaitForSeconds delay;

    protected override void Attack(float percent)
    {
        delay = new WaitForSeconds(burstDelay);
        StartCoroutine(Burst(percent));
    }

    private IEnumerator Burst(float percent)
    {
        for (int i = 0; i < burstCount; i++)
        {
            Vector3 playerForward = transform.forward;

            Rigidbody rb = Instantiate(bullet, transform.position, transform.rotation);
            rb.AddForce(Mathf.Max(percent, 0.1f) * force * playerForward, ForceMode.Impulse);
            Destroy(rb.gameObject, 4);

            yield return delay;
        }
    }
}
