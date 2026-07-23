using System;
using UnityEngine;
using UnityEngine.Events;

public class Character : MonoBehaviour
{
    [Header("基本属性")] public float maxHealth;
    public float currentHealth;
    public float maxPower;
    public float currentPower;
    public float powerRecoverSpeed;

    [Header("受伤无敌")] public float invulnerableDuration;
    public float invulnerableCounter;
    public bool invulnerable;
    [Header("事件")] public UnityEvent<Transform> OnTakeDamage;
    public UnityEvent OnDie;
    public UnityEvent<Character> OnHealthChange;

    private void Start()
    {
        currentHealth = maxHealth;
        currentPower = maxPower;
        OnHealthChange?.Invoke(this);
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

        OnHealthChange?.Invoke(this);
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
        
        currentPower += powerRecoverSpeed * Time.deltaTime;
        if (currentPower >= maxPower)
        {
            currentPower = maxPower;
        }
    }

    public void OnSlide(float cost)
    {
        currentPower -= cost;
        if (currentPower < 0)
        {
            currentPower = 0;
        }
        OnHealthChange?.Invoke(this);
    }
}