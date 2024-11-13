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
    private PlayerDamage playerDamage;
    protected override void Start()
    {
        base.Start();
        playerDamage = gameObject.GetComponentInChildren<PlayerDamage>();
        playerDamage.DamageEvent += Attack;
    }
    protected override void Defeat()
    {
        throw new System.NotImplementedException();
    }

    protected override void Movement()
    {
        playerTransform.RotateAround(Vector3.zero, Vector3.forward, GetInput());
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
