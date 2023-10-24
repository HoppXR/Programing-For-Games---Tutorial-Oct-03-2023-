using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private float curSpeed;
    private float curDamage;
    public float baseSpeed;
    public float contactDamage;

    private Vector3 curDirection;

    private Rigidbody owner;

    public float lifetime = 0;

    public void Initialize(float chargePercent, Rigidbody owner)
    {
        this.owner = owner;
        curDirection = transform.right;
        curSpeed = baseSpeed * chargePercent;
        curDamage = contactDamage * chargePercent;
        GetComponent<Rigidbody>().AddForce(transform.forward * curSpeed,ForceMode.Impulse);
    }
}
