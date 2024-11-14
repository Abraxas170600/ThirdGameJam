using System.Collections;
using System.Collections.Generic;
using UltEvents;
using UnityEngine;

public class PlayerDamage : MonoBehaviour
{
    private UltEvent<Enemy> damageEvent;
    public UltEvent<Enemy> DamageEvent { get => damageEvent; set => damageEvent = value; }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Enemy enemy = collision.GetComponent<Enemy>();
        if (enemy != null)
        {
            DamageEvent.Invoke(enemy);
        }
    }
}
