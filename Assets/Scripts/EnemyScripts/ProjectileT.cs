using UnityEngine;

public class ProjectileT : MonoBehaviour
{
    public GameObject target;
    public float speed = 100f;
    public int damage = 40;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(target.transform);
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            IDamageable enemy = target.GetComponent<IDamageable>();
            enemy.TakeDamage(damage);
            Destroy(this.gameObject);
        }
    }
}
