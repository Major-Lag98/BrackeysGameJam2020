using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour, IDamageable, IHealable
{
    public int MaxHealth = 10;

    [SerializeField]
    int currHealth = 10;

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


        currHealth = Mathf.Clamp(currHealth - amount, 0, MaxHealth);
        Debug.Log("Took Damage, health = " + currHealth);
    }

    public void Heal(int amount)
    {
        currHealth = Mathf.Clamp(currHealth + amount, 0, MaxHealth);
    }
}
