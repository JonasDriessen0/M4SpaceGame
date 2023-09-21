using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunRotate : MonoBehaviour
{
    public Transform firePoint;
    public float firePointFlipRot;

    private Transform m_transform;
    public float angle;
    private bool isFlipped = false;

    void Start()
    {
        m_transform = this.transform;
    }

    public void LAmouse()
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
    }

    private void Update()
    {
        LAmouse();
    }
}
