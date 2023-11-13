using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotgunWeapon : WeaponBase
{
    [SerializeField] private Rigidbody bullet;
    [SerializeField] private float force = 50;
    [SerializeField] int bulletCount = 9;
    [SerializeField] float spreadAngle = 45;

    protected override void Attack(float percent)
    {
        for(int i = 0 ; i < bulletCount ; i++)
        {
            float randomAngle = Random.Range(-spreadAngle, spreadAngle);
            Quaternion spreadRotation = Quaternion.Euler(0, randomAngle, 0);
            Vector3 randomOffset = new Vector3(Random.Range(-1.5f, 1.5f), Random.Range(-1.5f, 1.5f), 0);

            randomOffset = transform.rotation * randomOffset;
            
            Rigidbody rb = Instantiate(bullet, transform.position + randomOffset, transform.rotation * spreadRotation); 
            rb.AddForce(Mathf.Max(percent, 0.1f) * force * transform.forward, ForceMode.Impulse);
            Destroy(rb.gameObject, 4);

            GetComponent<AudioSource>().Play();
        }
    }
}
