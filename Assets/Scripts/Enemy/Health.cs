using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public event Action<float> OnHealthChanged;
    public event Action OnDeath;

    public float currenHealth;
    public float maxHealth { get; private set; }

    public float HP 
    {
        get { return currenHealth; }

        private set 
        {
            currenHealth = Mathf.Clamp(value, 0, maxHealth);
        } 
    }

    private void Awake()
    {
        maxHealth = transform.GetComponent<Enemy>().status.Health;
        currenHealth = maxHealth;
    }

    public void TakeDamage(float amount)
    {
        if (currenHealth < 0) return;

        currenHealth = Math.Max(currenHealth - amount, 0);
        OnHealthChanged?.Invoke(currenHealth);

        if(currenHealth == 0)
        {
            OnDeath?.Invoke();
        }
    }
}
