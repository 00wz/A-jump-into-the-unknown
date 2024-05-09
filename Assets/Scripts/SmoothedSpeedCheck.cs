using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmoothedSpeedCheck : MonoBehaviour
{
    [SerializeField]
    private float SpeedChangeRate = 10.0f;

    public Vector3 InstantaneousSpeed { get; private set; }
    public Vector3 SmoothedSpeed { get; private set; }
    private Vector3 _lastPosition;

    private void Start()
    {
        _lastPosition = transform.position;
    }

    void Update()
    {
        var _currentPosition = transform.position;
        InstantaneousSpeed = (_currentPosition - _lastPosition) / Time.deltaTime;
        _lastPosition = _currentPosition;
        SmoothedSpeed = Vector3.Lerp(SmoothedSpeed, InstantaneousSpeed,
                Time.deltaTime * SpeedChangeRate);
    }
}
