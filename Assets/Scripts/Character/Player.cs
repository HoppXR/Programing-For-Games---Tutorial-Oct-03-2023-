using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    Rigidbody rb;

    [Header("Weapons")]
    private float selectedWeapon;
    private string currentWeapon;
    
    [SerializeField] private WeaponBase semiWeapon;
    [SerializeField] private WeaponBase burstWeapon;
    [SerializeField] private WeaponBase shotgunWeapon;
    [SerializeField] private WeaponBase laserWeapon;

    [SerializeField] private int maxAmmo;
    private float currentAmmo;
    private float consumedAmmo;
    [SerializeField] private float reserveAmmo;

    [Header("Movement")]
    [SerializeField] private float speed;
    [SerializeField] private float jump;

    private Vector2 currentRotation;

    [Header("Camera")]
    [SerializeField, Range(1,20)] private float mouseSensX;
    [SerializeField, Range(1,20)] private float mouseSensY;

    [SerializeField, Range(-90,0)] private float minViewAngle;
    [SerializeField, Range(0,90)] private float maxViewAngle;

    [SerializeField] private Transform followTarget;

    //[Header("Shooting")]
    //[SerializeField] private Rigidbody bulletPrefab;
    //[SerializeField] private float projectileForce;

    [Header("Player UI")]
    //[SerializeField] private Image healthBar;
    [SerializeField] private TextMeshProUGUI ammoCounter;
    [SerializeField] private TextMeshProUGUI reserveCounter;
    [SerializeField] private TextMeshProUGUI weaponInfo;

    //[SerializeField] private float maxHealth;
    
    //private float _health;

    /*
    private float Health
    {
        get => _health;
        set
        {
            _health = value;
            healthBar.fillAmount = _health / maxHealth;
        }
    }
    */

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

        //Health = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += transform.rotation * (speed * Time.deltaTime * _moveDirection);
        isGrounded = Physics.Raycast(transform.position, -Vector3.up, GetComponent<Collider>().bounds.extents.y);

        ChangeWeapon();

        //Health -= Time.deltaTime * 2;
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

    public void Shoot()
    {
        if (currentAmmo > 0)
        {
            --currentAmmo;
            ++consumedAmmo;

            if(selectedWeapon == 1)
            {
                semiWeapon.StartShooting();
                ammoCounter.text = currentAmmo.ToString();
            }
            else if(selectedWeapon == 2)
            {
                burstWeapon.StartShooting();
                ammoCounter.text = currentAmmo.ToString();
            }
            else if(selectedWeapon == 3)
            {
                shotgunWeapon.StartShooting();
                ammoCounter.text = currentAmmo.ToString();
            }
            else if(selectedWeapon == 4)
            {
                laserWeapon.StartShooting();
                ammoCounter.text = currentAmmo.ToString();
            }
            
            /*
            Rigidbody currentProjectile = Instantiate(bulletPrefab, transform.position, Quaternion.identity);

            currentProjectile.AddForce(followTarget.forward * projectileForce, ForceMode.Impulse);

            --currentAmmo;

            ammoCounter.text = currentAmmo.ToString();

            Destroy(currentProjectile.gameObject, 4);
            */
        }

        /*
        print("I shot: " + InputManager.GetCameraRay());
        weaponShootToggle = !weaponShootToggle;
        if(weaponShootToggle) myWeapon.StartShooting();
        else myWeapon.StopShooting();
        */
    }   

    public void Reload()
    {
        if(reserveAmmo > 0)
        {
            currentAmmo = maxAmmo;
            reserveAmmo -= consumedAmmo;
            ammoCounter.text = currentAmmo.ToString();
            reserveCounter.text = reserveAmmo.ToString();
        }
    }

    public void ChangeWeapon()
    {
        weaponInfo.text = currentWeapon;

        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            selectedWeapon = 1;
            currentWeapon = "Semi Auto Weapon";
        }
        else if(Input.GetKeyDown(KeyCode.Alpha2))
        {
            selectedWeapon = 2;
            currentWeapon = "Burst Fire Weapon";
        }
        else if(Input.GetKeyDown(KeyCode.Alpha3))
        {
            selectedWeapon = 3;
            currentWeapon = "Shotgun Weapon";
        }
        else if(Input.GetKeyDown(KeyCode.Alpha4))
        {
            selectedWeapon = 4;
            currentWeapon = "Laser Weapon";
        }
    }
}
