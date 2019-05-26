using UnityEngine;

public class Projectile : MonoBehaviour
{
    private float Speed = 10f;
    public int Damage { get; set; } = 0;
    public LayerMask EnemyLayer;
    public Vector3 Target { get; set; }

    bool IsTravelDone = false;

    public void InitializeFields(int damage, Vector3 target)
    {
        Damage = damage;
        Target = target + new Vector3(0, 1f, 0);
    }

    private void OnCollisionEnter(Collision collision)
    {
        // If collided with enemy, apply damage and destroy itself.
        if (((1 << collision.gameObject.layer) & EnemyLayer) != 0)
        {
            collision.gameObject.GetComponent<IDamageable>().TakeDamage(Damage);
            Destroy(gameObject);
        }
    }

    void Update()
    {
        if (!IsTravelDone)
        {
            // Missed. Dispose projectile.
            if (Vector3.Distance(transform.position, Target) > 10f)
                Destroy(gameObject);

            // Update position
            var step = Speed * Time.deltaTime;

            if (!(Target - transform.position).Equals(Vector3.zero))
            {
                var rotationTarget = Quaternion.LookRotation(Target - transform.position);
                transform.rotation = Quaternion.Slerp(transform.rotation, rotationTarget, step);
                transform.position = Vector3.MoveTowards(transform.position, Target, step);
            }
            else
            {
                GetComponent<Rigidbody>().useGravity = true;
                IsTravelDone = true;
            }
        }
    }
}