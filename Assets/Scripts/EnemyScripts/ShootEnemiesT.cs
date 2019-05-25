using UnityEngine;

public class ShootEnemiesT : MonoBehaviour
{
    public GameObject bullet;
    public float bulletSpeed = 40f;
    public float secondsBetweenShots = 1;
    public GameObject bulletSpawn;
    private float secondCount = 0;
    public float range = 1;


    private void Start()
    {
        var rangeCircle = gameObject.transform.GetChild(0);
        rangeCircle.localScale = new Vector3(range,range);
    }
    void Update()
    {
        secondCount += Time.deltaTime;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            //check if enough time passed between shots
            if (secondCount >= secondsBetweenShots)
            {
                //TODO: use orc prefab instead of model
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
