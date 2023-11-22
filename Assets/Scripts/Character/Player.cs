using System;
using System.Collections;
using System.Collections.Generic;
using ScriptableObjects;
using TMPro;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    Rigidbody rb;

    [Header("Weapons")]
    public ScriptableObjects.BulletSO bulletSO;
    private float selectedWeapon;
    private string currentWeapon;
    private string currentBullet;
    
    [SerializeField] private WeaponBase semiWeapon;
    [SerializeField] private WeaponBase burstWeapon;
    [SerializeField] private WeaponBase shotgunWeapon;

    [SerializeField] private float recoil;

    [SerializeField] private int maxAmmo;
    private float currentAmmo;
    private float consumedAmmo;
    [SerializeField] private float reserveAmmo;

    [Header("Movement")]
    [SerializeField] private float speed;
    [SerializeField] private float jump;

    [Header("Camera")]
    [SerializeField, Range(1,20)] private float mouseSensX;
    [SerializeField, Range(1,20)] private float mouseSensY;

    [SerializeField, Range(-90,0)] private float minViewAngle;
    [SerializeField, Range(0,90)] private float maxViewAngle;

    [SerializeField] private Transform followTarget;


    [Header("Player UI")]
    [SerializeField] private TextMeshProUGUI ammoCounter;
    [SerializeField] private TextMeshProUGUI reserveCounter;
    [SerializeField] private TextMeshProUGUI weaponInfo;
    [SerializeField] private TextMeshProUGUI bulletInfo;

    private Vector2 currentAngle;

    bool isCrouched;
    bool isGrounded;
    bool doubleJump;

    private float distanceToGround;

    private Vector3 _moveDirection;

    // Start is called before the first frame update
    void Start()
    {
        Reload();
        
        rb = GetComponent<Rigidbody>();

        InputManager.Init(this);
        InputManager.SetGameControls();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += transform.rotation * (speed * Time.deltaTime * _moveDirection);
        isGrounded = Physics.Raycast(transform.position, -Vector3.up, GetComponent<Collider>().bounds.extents.y);

        ChangeWeaponInfo();
        ChangeBulletInfo();
        ChangeBullet();
    }

    public void SetMovementDirection(Vector3 currentDirection)
    {
        _moveDirection = currentDirection;
    }

    public void PlayerJump(Vector3 currentDirection)
    {
        if (Input.GetKeyDown("space"))
        {
            if (isGrounded)
            {
                doubleJump = true;
                rb.velocity = new Vector3(rb.velocity.x, jump, rb.velocity.z);
            }
            else if (doubleJump)
            {
                doubleJump = false;
                rb.velocity = new Vector3(rb.velocity.x, jump, rb.velocity.z);
            }
        }
    }

    public void StealthMode(Vector3 currentDirection)
    {
        if (Input.GetKeyDown("left ctrl"))
        {
            if (!isCrouched)
            {
                isCrouched = true;
                speed /= 2;
                Debug.Log("Stealth Mode On");
            }
            else
            {
                isCrouched = false;
                speed *= 2;
                Debug.Log("Stealth Mode Off");
            }
        }
    }

    public void SetLookRotation(Vector2 readValue)
    {
        currentAngle.x += readValue.x * Time.deltaTime * mouseSensX;
        currentAngle.y += readValue.y * Time.deltaTime * mouseSensY;

        currentAngle.y = Mathf.Clamp(currentAngle.y, minViewAngle, maxViewAngle);

        transform.rotation = Quaternion.AngleAxis(currentAngle.x, Vector3.up);
        followTarget.localRotation = Quaternion.AngleAxis(currentAngle.y, Vector3.right);
    }

    public void ChangeBullet()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            bulletSO.SetBulletType(ScriptableObjects.EProjectileType.Normal);
            currentBullet = "Bullet: Normal";
        }
        else if (Input.GetKeyDown(KeyCode.X))
        {
            bulletSO.SetBulletType(ScriptableObjects.EProjectileType.Water);
            currentBullet = "Bullet: Water";
        }
        else if (Input.GetKeyDown(KeyCode.C))
        {
            bulletSO.SetBulletType(ScriptableObjects.EProjectileType.Fire);
            currentBullet = "Bullet: Fire";
        }
        else if (Input.GetKeyDown(KeyCode.V))
        {
            bulletSO.SetBulletType(ScriptableObjects.EProjectileType.Light);
            currentBullet = "Bullet: Light";
        }
        else if (Input.GetKeyDown(KeyCode.B))
        {
            bulletSO.SetBulletType(ScriptableObjects.EProjectileType.Dark);
            currentBullet = "Bullet: Dark";
        }
    }

    public void Shoot()
    {
        if (currentAmmo > 0)
        {
            Vector3 recoilVector = -transform.forward * recoil;

            if(selectedWeapon == 1)
            {
                rb.AddForce(recoilVector, ForceMode.Impulse);

                semiWeapon.StartShooting();
                --currentAmmo;
                ++consumedAmmo;
                ammoCounter.text = currentAmmo.ToString();
            }
            else if(selectedWeapon == 2)
            {
                rb.AddForce(recoilVector, ForceMode.Impulse);

                burstWeapon.StartShooting();
                --currentAmmo;
                ++consumedAmmo;
                ammoCounter.text = currentAmmo.ToString();
            }
            else if(selectedWeapon == 3)
            {
                rb.AddForce(recoilVector, ForceMode.Impulse);

                shotgunWeapon.StartShooting();
                --currentAmmo;
                ++consumedAmmo;
                ammoCounter.text = currentAmmo.ToString();
            }
        }
    }   

    public void Reload()
    {
        if(consumedAmmo >= reserveAmmo)
        {
            currentAmmo += reserveAmmo;
            reserveAmmo = 0;
            consumedAmmo = 0;

            ammoCounter.text = currentAmmo.ToString();
            reserveCounter.text = reserveAmmo.ToString();
        }
        else if(reserveAmmo >= consumedAmmo)
        {
            currentAmmo = maxAmmo;
            reserveAmmo -= consumedAmmo;
            consumedAmmo = 0;

            ammoCounter.text = currentAmmo.ToString();
            reserveCounter.text = reserveAmmo.ToString();
        }
    }

    public void AddAmmo(int ammoAmount)
    {
        reserveAmmo += ammoAmount;
        
        reserveCounter.text = reserveAmmo.ToString();
    }

    public void ChangeWeaponInfo()
    {
        weaponInfo.text = currentWeapon;
    }

    public void ChangeBulletInfo()
    {
        bulletInfo.text = currentBullet;
    }

    public void SemiWeapon()
    {
        selectedWeapon = 1;
        currentWeapon = "Semi Auto Weapon";
    }

    public void BurstWeapon()
    {
        selectedWeapon = 2;
        currentWeapon = "Burst Fire Weapon";
    }

    public void ShotgunWeapon()
    {
        selectedWeapon = 3;
        currentWeapon = "Shotgun Weapon";
    }
}
