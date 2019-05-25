using UnityEngine;

public class ShootEnemiesT : MonoBehaviour
{
    public GameObject bullet;
    public float bulletSpeed = 40f;
    public float secondsBetweenShots = 1;
    public GameObject bulletSpawn;
    private float secondCount = 0;
    public float range = 7;
    private Transform rangeCircle;

    private void Start()
    {
        rangeCircle = gameObject.transform.GetChild(0);
        rangeCircle.localScale = new Vector3(range / GetComponentInParent<Transform>().localScale.x , range / GetComponentInParent<Transform>().localScale.y);
    }
    void Update()
    {
        secondCount += Time.deltaTime;
    }

    private void OnTriggerStay(Collider collider)
    {
        Collider[] others = Physics.OverlapSphere(this.transform.position, range+1);
        Collider other = others[Random.Range(0,others.Length)];
        if (other.CompareTag("Enemy"))
        {
            //check if enough time passed between shots
            if (secondCount >= secondsBetweenShots)
            {
                secondCount = 0;
                //create bullet
                GameObject bul = Instantiate(bullet, bulletSpawn.transform.position, bulletSpawn.transform.rotation);
                ProjectileT bulletObj = bul.GetComponent<ProjectileT>();
                bulletObj.speed = bulletSpeed;
                //Debug.Log("BULLET SHOT");
                bulletObj.target = other.gameObject;
            }

        }
    }
}
