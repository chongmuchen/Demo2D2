using System;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStatBar : MonoBehaviour
{
    public Character currentCharacter;
    public Image healthImage;
    public Image healthDeleteImage;
    public Image powerImage;
    private bool isRecovering;

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

        if (isRecovering)
        {
            var percentage = currentCharacter.currentPower / currentCharacter.maxPower;
            powerImage.fillAmount = percentage;
            if (percentage >= 1)
            {
                isRecovering = false;
            }
        }
    }

    public void OnHealthChange(float value)
    {
        healthImage.fillAmount = value;
    }

    public void OnPowerChange(Character character)
    {
        isRecovering = true;
        currentCharacter = character;
    }
}