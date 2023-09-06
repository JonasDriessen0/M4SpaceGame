using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FullAutoShoot : MonoBehaviour
{
    public Transform firePoint;
    public float firePointFlipRot;
    public GameObject bulletPrefab;
    public AudioSource shootAudioSource;

    private Transform m_transform;
    private float angle;
    private bool isFlipped = false;
    private float nextBullet;
    private bool isShooting = false;

    [SerializeField] private float bulletDelayTime = 0.2f;
    [SerializeField] private float shootSpeed = 5f;

    void Start()
    {
        m_transform = this.transform;
    }

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
            StartShooting();
        }
        else if (Input.GetButtonUp("Fire1"))
        {
            StopShooting();
        }

        if (isShooting && nextBullet <= 0)
        {
            Shoot();
            nextBullet = bulletDelayTime / shootSpeed;
        }

        if (nextBullet > 0)
        {
            nextBullet -= Time.deltaTime;
        }
    }

    private void Update()
    {
        LAmouse();
    }

    void StartShooting()
    {
        isShooting = true;
        shootAudioSource.Play();
    }

    void StopShooting()
    {
        isShooting = false;
        shootAudioSource.Stop();
    }

    void Shoot()
    {
        Instantiate(bulletPrefab, firePoint.position, Quaternion.Euler(90, 0, angle));
    }
}
