using System;
using UnityEngine;
using UnityEngine.Events;

public class Character : MonoBehaviour
{
    [Header("基本属性")]
    public float maxHealth;
    public float currentHealth;

    [Header("受伤无敌")]
    public float invulnerableDuration;
    public float invulnerableCounter;
    public bool invulnerable;
    public UnityEvent<Transform> OnTakeDamage;
    public UnityEvent OnDie;
    
    private void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(Attack attack)
    {
        if (invulnerable)
        {
            return;
        }

        if (currentHealth > attack.damage)
        {
            currentHealth -= attack.damage; 
            TriggerInvulnerable();
            OnTakeDamage?.Invoke(attack.transform);
        }
        else
        {
            currentHealth = 0;
            OnDie?.Invoke();
        }
    }

    private void TriggerInvulnerable()
    {
        invulnerableCounter = invulnerableDuration;
        invulnerable = true;
    }

    private void Update()
    {
        if (invulnerable)
        {
            invulnerableCounter -= Time.deltaTime;
            if (invulnerableCounter <= 0)
            {
                invulnerable = false;
            }
        }
    }
}
