using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class InputMove : MonoBehaviour
{
    [SerializeField]
    private float Speed = 4f;

    private Rigidbody _rigidbody;

    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    void Update()
    {
        /*
        Vector3 newVelocity =
            new Vector3(Input.GetAxis("Horizontal") * Speed, _rigidbody.velocity.y);
        Debug.LogWarning(newVelocity);
        _rigidbody.velocity = newVelocity;
        Debug.Log(_rigidbody.velocity);
        */
        /*
        Vector3 deltaVelocity = 
            (Input.GetAxis("Horizontal") * Speed - _rigidbody.velocity.x) * Vector3.right;
        _rigidbody.AddForce(deltaVelocity, ForceMode.VelocityChange);
        */
        Vector3 deltaPosition = Input.GetAxis("Horizontal") * Speed * 
            Vector3.right * Time.deltaTime;
        _rigidbody.position += deltaPosition;
        /*
        _rigidbody.MovePosition(transform.position +
            Input.GetAxis("Horizontal") * Speed * Vector3.right * Time.deltaTime);*/
    }
}
