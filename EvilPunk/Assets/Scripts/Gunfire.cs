using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gunfire : MonoBehaviour
{
    public Transform firePoint;
    private float nextbullet;
    public GameObject bulletPrefab;
    public AudioSource shootAudioSource;
    public GunRotate gunrotate;
    [SerializeField] private float nextBULLETtime = 0.2f;

    private void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }

        if (nextbullet > 0)
        {
            nextbullet -= Time.deltaTime;
        }

        void Shoot()
        {
            if (nextbullet > 0)
            {
                return;
            }

            Instantiate(bulletPrefab, firePoint.position, Quaternion.Euler(90, 0, gunrotate.angle));
            nextbullet = nextBULLETtime;

            // Play the audio clip
            shootAudioSource.Play();
        }
    }
}
