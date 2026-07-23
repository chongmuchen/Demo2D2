using System;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStatBar : MonoBehaviour
{
    public Image healthImage;
    public Image healthDeleteImage;
    public Image powerImage;

    public void Update()
    {
        if (healthImage.fillAmount > healthDeleteImage.fillAmount)
        {
            healthDeleteImage.fillAmount -= Time.deltaTime;
        }
        else
        {
            healthDeleteImage.fillAmount = healthImage.fillAmount;
        }
    }

    public void OnHealthChange(float value)
    {
        healthImage.fillAmount = value;
    }
}