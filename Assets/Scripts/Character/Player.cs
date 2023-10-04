using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    Rigidbody rb;

    [SerializeField] private float speed;
    [SerializeField] private float jump;

    bool isCrouched;
    bool isGrounded;
    bool doubleJump;

    private float distanceToGround;

    private Vector3 _moveDirection;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();

        InputManager.Init(this);
        InputManager.SetGameControls();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += speed * Time.deltaTime * _moveDirection;
        isGrounded = Physics.Raycast(transform.position, -Vector3.up, GetComponent<Collider>().bounds.extents.y);
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
}
