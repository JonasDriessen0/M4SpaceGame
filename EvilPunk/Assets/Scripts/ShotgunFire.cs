using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotgunFire : MonoBehaviour
{
    public Transform firePoint;
    public float firePointFlipRot;
    public GameObject bulletPrefab;
    private Transform m_transform;
    private float angle;
    private bool isFlipped = false;
    private float nextbullet;

    [SerializeField] private float nextBULLETtime = 0.2f;
    [SerializeField] private float shotgunSpreadAngle = 15f;
    [SerializeField] private int numberOfBullets = 4;

    // Start is called before the first frame update
    void Start()
    {
        m_transform = this.transform;
    }

    // Update is called once per frame
    private void LAmouse()
    {
        Vector2 center = new Vector2(Screen.width / 2, Screen.height / 2);
        angle = Mathf.Atan2(Input.mousePosition.y - center.y, Input.mousePosition.x - center.x) * Mathf.Rad2Deg;
        transform.localEulerAngles = new Vector3(0, 0, angle);
        if (angle <= -90 || angle >= 90)
        {
            transform.GetComponent<SpriteRenderer>().flipY = true;
            if (!isFlipped)
            {
                isFlipped = true;
                firePoint.transform.localPosition = new Vector3(firePoint.transform.localPosition.x, firePoint.transform.localPosition.y - firePointFlipRot, firePoint.transform.localPosition.z);
            }

        }
        else
        {
            transform.GetComponent<SpriteRenderer>().flipY = false;
            if (isFlipped)
            {
                isFlipped = false;
                firePoint.transform.localPosition = new Vector3(firePoint.transform.localPosition.x, firePoint.transform.localPosition.y + firePointFlipRot, firePoint.transform.localPosition.z);
            }
        }

        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }

        if (nextbullet > 0)
        {
            nextbullet -= Time.deltaTime;
        }
    }

    private void Update()
    {
        LAmouse();
    }

    void Shoot()
    {
        if (nextbullet > 0)
        {
            return;
        }

        // Fire four bullets with a shotgun spread
        for (int i = 0; i < numberOfBullets; i++)
        {
            float spreadAngle = Random.Range(-shotgunSpreadAngle, shotgunSpreadAngle);
            Quaternion bulletRotation = Quaternion.Euler(90, 0, angle + spreadAngle);
            Instantiate(bulletPrefab, firePoint.position, bulletRotation);
        }

        nextbullet = nextBULLETtime;
    }
}
