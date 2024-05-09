
using UnityEngine;

public class RagdollToggle : MonoBehaviour
{
    [SerializeField]
    private Collider myCollider;
    [SerializeField]
    private MonoBehaviour[] myScripts;
    [SerializeField]
    private Rigidbody myRigidbody;
    [SerializeField]
    private GameObject RagdollRoot;
    [SerializeField]
    private Animator Animator;
    [SerializeField]
    private bool isRagdoll;

    public bool IsRagdoll => isRagdoll;

    private Rigidbody[] rigidbodies;
    private Collider[] raddollsColliders;

    void Start()
    {
        rigidbodies =RagdollRoot.GetComponentsInChildren<Rigidbody>();
        raddollsColliders = RagdollRoot.GetComponentsInChildren<Collider>();
        ToggleRagdoll(isRagdoll) ;
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.G))
        {
            ToggleRagdoll(true);
        }
    }

    private void ToggleRagdoll(bool isActive)
    {
        isRagdoll = isActive;
        myCollider.enabled = !isActive;
        Vector3 velocity = myRigidbody.velocity;
        myRigidbody.isKinematic = isActive;
        Animator.enabled = !isActive;
        for (int i = 0; i < myScripts.Length; i++)
        {
            myScripts[i].enabled = !isActive;
        }
        for (int i = 0; i < rigidbodies.Length; i++)
        {
            rigidbodies[i].isKinematic = !isActive;
        }
        if(isActive)
        {
            for (int i = 0; i < rigidbodies.Length; i++)
            {
                rigidbodies[i].velocity = velocity;
            }
        }
        for (int i = 0; i < raddollsColliders.Length; i++)
        {
            raddollsColliders[i].enabled = isActive;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "enableRagdoll")
        {
            ToggleRagdoll(true);
        }
    }
}
