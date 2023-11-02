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
        print("My weapon attacked: " + percent);
        Ray camRay = InputManager.GetCameraRay();
        Rigidbody rb = Instantiate(percent>0.5f?myBullet2:myBullet, camRay.origin, transform.rotation);
        rb.AddForce(Mathf.Max(percent, 0.1f) * force * camRay.direction, ForceMode.Impulse);
        Destroy(rb.gameObject, 5);
    }
}
