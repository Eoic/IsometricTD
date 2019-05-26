using UnityEngine;

public class ProjectileT : MonoBehaviour
{
    public GameObject target;
    public float speed = 100f;
    public int damage = 40;

    private void Start()
    {
        //if bullet doesn't hit anything, destroy it after 3 seconds
        Invoke("DestroyBullet", 3);
    }
    void Update()
    {
        if(target != null)
            transform.LookAt(target.transform);
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy") && target != null)
        {
            IDamageable enemy = target.GetComponent<IDamageable>();
            enemy.TakeDamage(damage);
            Destroy(this.gameObject);
        }
    }

    void DestroyBullet()
    {
        Destroy(this.gameObject);
    }
}
