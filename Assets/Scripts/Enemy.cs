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
    private void OnEnable()
    {
        respawnEvent.Invoke();
    }
    protected override void Defeat()
    {
        VFXManager.Instance.PlayVFX(EnumEffect.CFXR_Explo1, transform);
        VFXManager.Instance.PlayVFX(EnumEffect.CFXR_Boom, transform);
        AudioManager.Instance.PlaySFX(EnumSounds.Sfx_Crash);
        DefeatEvent.Invoke();
    }
    protected override void Attack(Entity entity)
    {
        base.Attack(entity);
        DefeatEvent.Invoke();
        gameObject.SetActive(false);
        AudioManager.Instance.PlaySFX(EnumSounds.Sfx_Crash);
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
            VFXManager.Instance.PlayVFX(EnumEffect.CFXR_Explo1, transform);
            Attack(player);
        }
    }
    private Player GetPlayer() => FindObjectOfType<Player>();

}
