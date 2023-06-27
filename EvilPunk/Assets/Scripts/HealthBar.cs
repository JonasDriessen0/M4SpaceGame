using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    public Image image;

    private PlayerScript playerScript;

    private void Awake()
    {
        playerScript = GameObject.Find("PlayerGroup").GetComponent<PlayerScript>();
    }

    private void Update()
    {
        image.fillAmount = playerScript.currentHP / playerScript.maxHP;
        Debug.Log(playerScript.currentHP / playerScript.maxHP);
    }
}
