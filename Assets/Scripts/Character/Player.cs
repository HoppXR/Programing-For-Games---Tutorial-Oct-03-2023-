using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    //[SerializeField] private WeaponBase myWeapon;
    private bool weaponShootToggle;
    
    Rigidbody rb;

    [SerializeField] private float speed;
    [SerializeField] private float jump;

    private Vector2 currentRotation;

    [Header("Camera")]
    [SerializeField, Range(1,20)] private float mouseSensX;
    [SerializeField, Range(1,20)] private float mouseSensY;

    [SerializeField, Range(-90,0)] private float minViewAngle;
    [SerializeField, Range(0,90)] private float maxViewAngle;

    [SerializeField] private Transform followTarget;

    [Header("Shooting")]
    [SerializeField] private Rigidbody bulletPrefab;
    [SerializeField] private float projectileForce;

    [Header("Player UI")]
    //[SerializeField] private Image healthBar;
    [SerializeField] private TextMeshProUGUI ammoCounter;

    //[SerializeField] private float maxHealth;
    [SerializeField] private int maxAmmo;
    private float currentAmmo;
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
            Rigidbody currentProjectile = Instantiate(bulletPrefab, transform.position, Quaternion.identity);

            currentProjectile.AddForce(followTarget.forward * projectileForce, ForceMode.Impulse);

            --currentAmmo;

            ammoCounter.text = currentAmmo.ToString();

            Destroy(currentProjectile.gameObject, 4);
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
        currentAmmo = maxAmmo;
        ammoCounter.text = currentAmmo.ToString();
    }
}
