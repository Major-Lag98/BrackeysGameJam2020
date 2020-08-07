using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Boss : MonoBehaviour
{
    //[SerializeField]
    //GameObject projectilePrefab;

    //[SerializeField]
    //Transform targetPosition;

    //[SerializeField]
    //Transform projectileSpawnPosition;

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
                    bossMoveLeftTime -= Time.deltaTime;

                    if (bossMoveLeftTime <= 0)
                    {
                        bossMovingLeft = !bossMovingLeft;
                        bossMoveLeftTime = bossMoveLeftTimeMax;
                    }
                    
                    if (bossMovingLeft)
                    {
                        rb.velocity = Vector2.left;
                    }
                    else
                    {
                        rb.velocity = Vector2.right;
                    }
                        

                    if (idleTime <= 0)
                    {
                        idleTime = idleTimeMax;
                        ChangToRandomAttackState();
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
        for (int i = 0; i < 10; i++)
        {
            Debug.Log("Launching ball #" + (i+1) + " at player");
            yield return new WaitForSeconds(.5f);
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
        for (int i = 0; i < 3; i++)
        {
            Debug.Log("Launch SemiCircle Attack " + (i + 1));
            yield return new WaitForSeconds(1);
        }
        attackState = AttackState.Idle;
        attacking = false;
    }
}
