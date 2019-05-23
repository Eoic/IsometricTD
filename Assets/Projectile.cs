using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float Speed = 1f;
    public int Damage { get; set; } = 0;
    public LayerMask EnemyLayer { get; set; }
    public Vector3 Target { get; set; }

    private bool fieldsSet = false;

    public void InitializeFields(int damage, Vector3 target)
    {
        Damage = damage;
        Target = new Vector3(1.5f, 0.5f, 0f);
        fieldsSet = true;
    }

    private void OnCollisionEnter(Collision collision)
    {
        // If collided with enemy, apply damage and destroy itself.
        if (((1 << collision.gameObject.layer) & EnemyLayer) != 0)
        {
            Debug.Log("Enemy hit.");
            collision.gameObject.GetComponent<EnemyController>().TakeDamage(Damage);
            Destroy(gameObject);
        }
    }

    void Update()
    {
        if (fieldsSet)
        {
            // Missed. Dispose projectile.
            if (Vector3.Distance(transform.position, Target) > 700f)
            {
                Debug.Log(Vector3.Distance(transform.position, Target));
                Destroy(gameObject);
            }

            Debug.Log(Target);
            // Update position
            var step = Speed * Time.deltaTime;
            var rotationTarget = Quaternion.LookRotation(Target - transform.position);
            transform.position = Vector3.MoveTowards(transform.position, Target, step);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotationTarget, step);
        }
    }
}