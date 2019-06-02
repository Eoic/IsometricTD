using UnityEngine;
using System.Collections.Generic;

public class ResourceDetector : MonoBehaviour
{
    public LayerMask resourceLayerMask;
    public GameObject resourceDepletionMarker;

    public float collectionInterval;
    public int bitePerCollection;

    private Stack<GameObject> resourceEntities;

    void CollectResources()
    {
        if (resourceEntities.Count == 0)
        {
            CancelInvoke("CollectResources");
            return;
        }

        Resource resourceRef = null;

        if (resourceEntities != null && resourceEntities.Peek() != null)
            resourceRef = resourceEntities.Peek().GetComponentInParent<Resource>();

        if (resourceRef != null)
        {
            if (resourceRef.IsDepleted)
            {
                Instantiate(resourceDepletionMarker, resourceEntities.Peek().transform.position, resourceDepletionMarker.transform.rotation);
                Destroy(resourceEntities.Peek());
                resourceEntities.Pop();
            }
            else
            {
                int collected = resourceRef.ConsumeResource(bitePerCollection);
                ResourceManager.Instance.AddWood(collected);
            } 
        }
    }

    public void StartCollecting()
    {
        // Scan for resources in range and invoke collection method
        TransformToStack(Physics.OverlapSphere(transform.position, 27, resourceLayerMask));
        InvokeRepeating("CollectResources", 2f, collectionInterval);
    }

    void TransformToStack(Collider[] colliders)
    {
        resourceEntities = new Stack<GameObject>();

        foreach (var collider in colliders)
            resourceEntities.Push(collider.gameObject);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, 27);
    }
}