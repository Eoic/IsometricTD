using UnityEngine;

public class PlayOnTrigger : MonoBehaviour
{
    public Animator gateAnimator;
    public bool gateClosed = false;

    private void OnTriggerEnter(Collider other)
    {
        if (gateAnimator != null && !gateClosed)
        {
            gateAnimator.Play("GateClosing");
            gateClosed = true;
        }
    }
}
