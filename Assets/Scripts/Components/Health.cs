using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Health : MonoBehaviour, IDamageable, IHealable
{
    [SerializeField]
    int maxHealth = 10;

    [HideInInspector]
    public int currHealth;

    [SerializeField]
    float invincibilityFrameTime = 5;
    float invincibilityFrameTimeCap;

    [SerializeField]
    bool canBeInvincible = false; //true for player, everything else probably false

    bool invincible = false;

    //Rigidbody2D rb;

    Animator animator;

    [SerializeField]
    ResourceBar healthBar;

    //[SerializeField]
    //Boss theBoss = null;

    //[SerializeField]
    //SpriteRenderer bossRenderer = null;


    [SerializeField]
    BossDefeatedManager boss;


    void Start()
    {
        //rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        currHealth = maxHealth;
        healthBar = transform.Find("Healthbar")?.GetComponent<ResourceBar>();
        SetHealthBar();

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
            if (animator != null)
            {
                animator.SetBool("Invincible", false);
            }
            
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

        currHealth = Mathf.Clamp(currHealth - amount, 0, maxHealth);
        SetHealthBar();

        if (currHealth <= 0)
        {
            Die();
        }
    }

    public void Heal(int amount)
    {
        currHealth = Mathf.Clamp(currHealth + amount, 0, maxHealth);
        SetHealthBar();
    }

    void SetHealthBar() //should be called whenever you change health value
    {
        if (healthBar == null) return; //only if we have a healthbar to set

        healthBar.SetSize(((float)currHealth / (float)maxHealth)); //set the size of the healthbar to the percent of health remaining

        
    }

    void Die()
    {
        if (gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            StartCoroutine("DelayResetPlayer");
        }
        else if (gameObject.name == "Boss")
        {

            
            boss.defeated = true;
            Destroy(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private IEnumerator DelayResetPlayer()
    {
        var renderer = GetComponent<SpriteRenderer>();
        var player = GetComponent<Player>();

        renderer.enabled = false; // Disable our renderer
        player.enabled = false; // Disable our player input
        foreach (Transform child in transform) // Disable all children
            child.gameObject.SetActive(false);

        yield return new WaitForSeconds(1.5f);

        MovePlayerToCheckpoint(CheckpointController.Instance.GetCurrentCheckpoint().transform);
        Heal(1000000);

        renderer.enabled = true; // Enable our renderer
        player.enabled = true; // Enable our player input
        foreach (Transform child in transform) // Enable all children
            child.gameObject.SetActive(true);
    }


    public void MovePlayerToCheckpoint(Transform checkpoint)
    {
        gameObject.transform.position = checkpoint.position;
    }


}
