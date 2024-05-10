using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpringScaler : MonoBehaviour
{
    [SerializeField]
    private GameObject Base;
    [SerializeField]
    private GameObject Platform;
    [SerializeField]
    private GameObject SpriteRoot;

    private float _scaleRatio;

    private void Awake()
    {
        if (SpriteRoot.transform.localScale != Vector3.one)
            Debug.LogWarning("SpringScaler: SpiteRoot not an isolated scale. GameObject:" +
                name);
        _scaleRatio = 1f / 
            (Platform.transform.position - Base.transform.position).magnitude;
    }

    void Update()
    {
        float ratio = 
            (Platform.transform.position - Base.transform.position).magnitude * _scaleRatio;
        SpriteRoot.transform.localScale =
            new Vector3(transform.localScale.x, ratio, transform.localScale.z);
    }
}
