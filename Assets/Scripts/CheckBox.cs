using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckBox : MonoBehaviour
{
    [Header("Obstacle test")]
    [SerializeField]
    private Vector3 TriggerLocalPosition;
    [SerializeField]
    private Vector3 TriggerSize;
    [SerializeField]
    private LayerMask LayerMask;


    private Collider[] _obstacleCollidersBuffer = new Collider[1];

    public Collider CheckOverlap()
    {
        int obstacleCount = Physics.OverlapBoxNonAlloc(
        transform.TransformPoint(TriggerLocalPosition),
        TriggerSize * 0.5f,
        _obstacleCollidersBuffer,
        transform.rotation,
        LayerMask);

        if(obstacleCount == 0)
        {
            return null;
        }
        else
        {
            return _obstacleCollidersBuffer[0];
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.black;
        Gizmos.matrix = Matrix4x4.TRS(transform.position, transform.rotation, Vector3.one);
        Gizmos.DrawWireCube(TriggerLocalPosition, TriggerSize);
    }
}
