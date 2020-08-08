using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Boss : MonoBehaviour
{
    [SerializeField]
    GameObject projectilePrefab;

    [SerializeField]
    Transform targetPosition;

    [SerializeField]
    Transform projectileSpawn;

    [SerializeField]
    float idleTimeMax = 10;//default is 10, you can change in editor
    float idleTime;

    [SerializeField]
    bool active = false; //set a trigger so when player enters a room it ai starts to do stuff... instead of just always doing it.

    bool attacking = false;


    [SerializeField]
    float bossMoveLeftTimeMax = 10;//default is 10, you can change in editor
    float bossMoveLeftTime;

    bool bossMovingLeft = true; //flag to make him move back and forth

    Rigidbody2D rb;

    [SerializeField]
    BoxCollider2D Trigger;


    [SerializeField]
    int semiCircleAttackAmountOfProjectiles = 10;
    [SerializeField]
    int semiCircleBurstCount = 3;

    [SerializeField]
    int secondsOfContinousFire = 10;

    [SerializeField]
    Animator animator;

    

    

    enum AttackState
    {
        Idle,
        SemiCircle,
        ContinuousFire
    }

    AttackState attackState = AttackState.Idle;



    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        active = false;
        idleTime = idleTimeMax;
        bossMoveLeftTime = bossMoveLeftTimeMax;
        
    }

    private void OnTriggerEnter2D(Collider2D collision) //trigger to start boss
    {
        if (active) return; //do nothing if boss is already fighting
        Trigger.enabled = false;
        active = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (active)
        {
            switch (attackState)
            {
                case AttackState.Idle:
                    idleTime -= Time.deltaTime;
                    animator.StopPlayback();
                    if (idleTime <= 0)
                    {
                        idleTime = idleTimeMax;
                        rb.velocity = Vector2.zero;
                        ChangToRandomAttackState();
                        animator.StartPlayback();
                    }
                    break;

                case AttackState.SemiCircle:
                    if (attacking) break;

                    Debug.Log("Boss is attacking with semicircle");
                    StartCoroutine("SemiCircleAttack");
                    attacking = true;
                    break;

                case AttackState.ContinuousFire:
                    if (attacking) break;

                    Debug.Log("Boss is attacking with ContinousFire");
                    StartCoroutine("ContinuousFireAttack");
                    attacking = true;
                    break;

            }
        }
        
    }

    void ChangToRandomAttackState()
    {
        int i = Random.Range(0, 2);

        if (i == 0) { attackState = AttackState.SemiCircle; }

        if (i == 1) { attackState = AttackState.ContinuousFire; }
    }



    /// <summary>
    /// Fires a continuous line of projectiles for X amount of time then returns to idle state
    /// </summary>
    /// <returns></returns>
    IEnumerator ContinuousFireAttack()
    {
        float seconds = secondsOfContinousFire * 10; // multiply by 10 because we are technicaly dividing by 10 with our yield so we cancle it out
        for (int i = 0; i < seconds; i++)
        {

            Debug.Log("Launching ball #" + (i+1) + " at player");

            Vector2 vectorToTarget = targetPosition.position - projectileSpawn.position;
            float angleToTarget = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg;

            ProjectileFactory.CreateProjectile(projectilePrefab, angleToTarget, projectileSpawn.position);

            yield return new WaitForSeconds(.1f);
        }
        attackState = AttackState.Idle;
        attacking = false;
    }

    /// <summary>
    /// Launch a semicircle attack of projectiles N amount of times with X amount of time between rounds then return to idle state
    /// </summary>
    /// <returns></returns>
    IEnumerator SemiCircleAttack()
    {

         //pi is half a circle so divide it by how many balls we want to spawn

        for (int i = 0; i < semiCircleBurstCount; i++)
        {
            float angleIncrement = Mathf.PI / (semiCircleAttackAmountOfProjectiles + i); //pi is half a circle so divide it by how many balls we want to spawn... +i so the player cant just stay in one spot to dodge the attack

            Debug.Log("Launch SemiCircle Attack " + (i + 1));
            for (float j = 0; j < (semiCircleAttackAmountOfProjectiles + i); j++)
            {
                
                Vector2 direction = new Vector2(Mathf.Cos(angleIncrement * j), Mathf.Sin(angleIncrement * j));
                float angleToTarget = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                ProjectileFactory.CreateProjectile(projectilePrefab, angleToTarget + 90, projectileSpawn.position); //+90 becuause we want the start of spawning of the projectiles to be at 90 degrees
            }
            
            yield return new WaitForSeconds(1);
        }
        attackState = AttackState.Idle;
        attacking = false;
    }
}
