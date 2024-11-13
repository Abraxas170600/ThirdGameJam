using System.Collections;
using System.Collections.Generic;
using UltEvents;
using UnityEngine;

public class Enemy : Entity
{
    [Header("Dependences")]
    protected Player player;

    [Header("Events")]
    [SerializeField] protected UltEvent respawnEvent;
    private UltEvent defeatEvent;
    public UltEvent DefeatEvent { get => defeatEvent; set => defeatEvent = value; }
    protected override void Start()
    {
        base.Start();
        player = GetPlayer();
    }
    protected override void Defeat()
    {
        DefeatEvent.Invoke();
    }
    protected override void Attack(Entity entity)
    {
        base.Attack(entity);
        DefeatEvent.Invoke();
        gameObject.SetActive(false);
    }
    protected override void Movement()
    {
        if (player != null)
        {
            Vector2 direction = (player.transform.position - transform.position).normalized;
            transform.Translate(direction * speed * Time.deltaTime);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Player player = collision.GetComponent<Player>();
        if (player != null)
        {
            //DamageEvent.Invoke(enemy);
            Attack(player);
        }
    }
    private Player GetPlayer() => FindObjectOfType<Player>();

}
