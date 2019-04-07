using UnityEngine;

public class PlayOnTrigger : MonoBehaviour
{
    public Animator gateAnimator;
    public bool gateClosed = false;

    private void OnTriggerEnter(Collider other)
    {
        if (gateAnimator != null && !gateClosed)
        {
            Debug.Log("Triggered0");
            gateAnimator.Play("GateClosing");
            gateClosed = true;
        }
    }
}
