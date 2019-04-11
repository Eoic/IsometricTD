
using UnityEngine;

public class BridgeController : MonoBehaviour
{
    private Animator animator;
    private bool isRaised = false;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy") && !isRaised)
        {
            animator.SetTrigger("Raise");
            isRaised = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Enemy") && isRaised)
        {
            animator.SetTrigger("Lower");
            isRaised = false;
        }
    }
}
