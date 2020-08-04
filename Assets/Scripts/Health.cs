using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour, IDamageable
{
    [SerializeField]
    int health = 10;

    [SerializeField]
    float invincibilityFrameTime = 5;
    float invincibilityFrameTimeCap;

    [SerializeField]
    bool canBeInvincible = false; //true for player, everything else probably false

    bool invincible = false;

    Rigidbody2D rb;

    Animator animator;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        invincibilityFrameTimeCap = invincibilityFrameTime;
    }

    void Update()
    {
        if (invincible)
        {
            invincibilityFrameTime -= Time.deltaTime;
        }
        if (invincibilityFrameTime <= 0)
        {
            invincibilityFrameTime = invincibilityFrameTimeCap;
            invincible = false;
            animator.SetBool("Invincible", false);
        }
    }

    public void Damage(int amount)
    {
        if (invincible) return;

        if (canBeInvincible)
        {
            invincible = true;
            animator.SetBool("Invincible", true);
        }
            

        health -= amount;
        Debug.Log("Took Damage, health = " + health);
    }
}
