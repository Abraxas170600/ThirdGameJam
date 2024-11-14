using System.Collections;
using System.Collections.Generic;
using UltEvents;
using UnityEngine;

public abstract class Entity : MonoBehaviour
{
    [Header("Health")]
    [SerializeField] private float currentHealth;
    [SerializeField] private float maxHealth;
    [SerializeField] private UltEvent<float> healthEvent;
    [SerializeField] private UltEvent deathEvent;
    public float RemainingHealthPercentage
    {
        get
        {
            return currentHealth / maxHealth;
        }
    }

    [Header("Attributes")]
    [SerializeField] protected float speed;
    [SerializeField] protected float damage;
    protected bool isDeath;

    [Header("Dependences")]
    protected Rigidbody entityRb;
    protected virtual void Start()
    {
        entityRb = GetComponent<Rigidbody>();
    }
    protected virtual void Update()
    {
        Movement();
    }
    protected abstract void Movement();
    protected abstract void Defeat();
    protected virtual void Attack(Entity entity)
    {
        entity.TakeDamage(damage);
    }

    protected virtual void TakeDamage(float damageAmount)
    {
        currentHealth -= damageAmount;
        healthEvent.Invoke(RemainingHealthPercentage);

        if (currentHealth <= 0)
        {
            currentHealth = 0;
            isDeath = true;
            deathEvent.Invoke();
            Defeat();
        }
    }

    public void AddHealth(float amountToAdd)
    {
        if (currentHealth == maxHealth)
        {
            return;
        }

        currentHealth += amountToAdd;

        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }

        healthEvent.Invoke(RemainingHealthPercentage);
    }
    public void FullHealth()
    {
        currentHealth = maxHealth;
    }
}
