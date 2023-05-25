using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunRotate : MonoBehaviour
{
    public Transform firePoint;
    public float firePointFlipRot;
    public GameObject bulletPrefab;
    private Transform m_transform;
    private float angle;
    private bool isFlipped = false;
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
        Debug.Log($"{angle} | {Input.mousePosition.x} | {Input.mousePosition.y}");
        transform.localEulerAngles = new Vector3(0, 0, angle);
        if (angle <= -90 || angle >= 90)
        {
            transform.GetComponent<SpriteRenderer>().flipY = true;
            if(!isFlipped)
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
    }

    private void Update()
    {
        LAmouse();
    }

    void Shoot()
    {
        Instantiate(bulletPrefab, firePoint.position, Quaternion.Euler(90, 0, angle));
    }
}
