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
        print("My weapon attacked: " + percent);
        Ray camRay = InputManager.GetCameraRay();

        delay = new WaitForSeconds(burstDelay);
        StartCoroutine(Burst(percent, camRay));
    }

    private IEnumerator Burst(float percent, Ray camRay)
    {
        for (int i = 0; i < burstCount; i++)
        {
            Rigidbody rb = Instantiate(bullet, camRay.origin, transform.rotation);
            rb.AddForce(Mathf.Max(percent, 0.1f) * force * camRay.direction, ForceMode.Impulse);
            Destroy(rb.gameObject, 5);

            yield return delay;
        }
    }
}
