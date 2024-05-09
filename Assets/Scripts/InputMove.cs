using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(GroundedCheck))]
public class InputMove : MonoBehaviour
{
    [SerializeField]
    private float Speed = 4f;
    [SerializeField]
    private float JumpSpeed = 10f;
    [SerializeField]
    private float JumpInterval = 0.2f;

    private Rigidbody _rigidbody;

    private GroundedCheck _groundedCheck;
    private bool _jumpIsAllowed = true;

    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _groundedCheck = GetComponent<GroundedCheck>();
    }

    void Update()
    {
        if(Input.GetKeyUp(KeyCode.Space))
        {
            _jumpIsAllowed = true;
        }

        if (!_groundedCheck.Grounded)
        {
            return;
        }
        
        Vector3 newVelocity =
            new Vector3(Input.GetAxis("Horizontal") * Speed, _rigidbody.velocity.y);
        _rigidbody.velocity = newVelocity;
        
        /*
        Vector3 deltaVelocity = 
            (Input.GetAxis("Horizontal") * Speed - _rigidbody.velocity.x) * Vector3.right;
        _rigidbody.AddForce(deltaVelocity, ForceMode.VelocityChange);
        */
        /*
        Vector3 deltaPosition = Input.GetAxis("Horizontal") * Speed * 
            Vector3.right * Time.deltaTime;
        _rigidbody.position += deltaPosition;
        */
        /*
        _rigidbody.MovePosition(transform.position +
            Input.GetAxis("Horizontal") * Speed * Vector3.right * Time.deltaTime);*/
        //_rigidbody.AddForce(Input.GetAxis("Horizontal") * Speed * Vector3.right, ForceMode.Acceleration);

        if(Input.GetKey(KeyCode.Space) && _jumpIsAllowed)
        {
            _jumpIsAllowed = false;
            //Invoke(nameof(AllowJump), JumpInterval);
            //_rigidbody.AddForce(JumpSpeed * Vector3.up, ForceMode.VelocityChange);
            newVelocity =
                new Vector3(_rigidbody.velocity.x, JumpSpeed);
            _rigidbody.velocity = newVelocity;
        }
    }

    private void AllowJump()
    {
        _jumpIsAllowed = true;
    }
}
