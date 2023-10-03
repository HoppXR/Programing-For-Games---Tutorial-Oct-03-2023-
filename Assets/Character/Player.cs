using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float speed;
    private Vector3 _moveDirection;

    Rigidbody rb;
    [SerializeField] private float jumpSpeed;

    // Start is called before the first frame update
    void Start()
    {
        InputManager.Init(this);
        InputManager.SetGameControls();
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += speed * Time.deltaTime * _moveDirection;
    }

    public void SetMovementDirection(Vector3 currentDirection)
    {
        _moveDirection = currentDirection;
    }

    public void PlayerJump(Vector3 currentDirection)
    {
        rb.velocity = new Vector3(rb.velocity.x,jumpSpeed,rb.velocity.z);
    }
}
