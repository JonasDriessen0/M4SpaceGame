using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunRotateExtra : MonoBehaviour
{
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
        transform.localEulerAngles = new Vector3(0, 0, angle);
        if (angle <= -90 || angle >= 90)
        {
            transform.GetComponent<SpriteRenderer>().flipY = true;
            if (!isFlipped)
            {
                isFlipped = true;
                //firePoint.transform.localPosition = new Vector3(firePoint.transform.localPosition.x, firePoint.transform.localPosition.y - firePointFlipRot, firePoint.transform.localPosition.z);
            }

        }
        else
        {
            transform.GetComponent<SpriteRenderer>().flipY = false;
            if (isFlipped)
            {
                isFlipped = false;
                //firePoint.transform.localPosition = new Vector3(firePoint.transform.localPosition.x, firePoint.transform.localPosition.y + firePointFlipRot, firePoint.transform.localPosition.z);
            }
        }
    }

    private void Update()
    {
        LAmouse();
    }
}
