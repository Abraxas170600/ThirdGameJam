using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Player : Entity
{
    [Header("Movement")]
    private float moveInput;

    [Header("Dependences")]
    [SerializeField] private Transform playerTransform;
    [SerializeField] private Transform visualTransform;
    [SerializeField] private PlayerDamage playerDamage;
    [SerializeField] private Animator planetAnim;
    [SerializeField] private Animator moonAnim;
    protected override void Start()
    {
        base.Start();
        playerDamage.DamageEvent += Attack;
    }
    protected override void Defeat()
    {
        Time.timeScale = 0;
        AudioManager.Instance.PlayMusic(EnumSounds.Sound_GameOver);
        planetAnim.Play("LosePlanet");
        moonAnim.Play("MoonGameOver");
    }

    protected override void Movement()
    {
        if (!isDeath)
        {
            playerTransform.RotateAround(Vector3.zero, Vector3.forward, GetInput());
            Flip();
        }
    }
    protected override void TakeDamage(float damageAmount)
    {
        base.TakeDamage(damageAmount);
        planetAnim.Play("DamagePlanet");
        moonAnim.Play("MoonDamage");
    }
    public void CompleteActions()
    {
        planetAnim.Play("LastWavePlanet");
        moonAnim.Play("MoonPassHorda");
    }
    private void Flip()
    {
        if (moveInput < 0)
        {
            visualTransform.localRotation = Quaternion.Euler(0, 0, 0);
        }
        else if (moveInput > 0)
        {
            visualTransform.localRotation = Quaternion.Euler(0, 180, 0);
        }
    }
    private float GetInput()
    {
        moveInput = Input.GetAxisRaw("Horizontal");
        return moveInput * Time.deltaTime * -speed;
    }
    private void OnDisable()
    {
        playerDamage.DamageEvent -= Attack;
    }
}
