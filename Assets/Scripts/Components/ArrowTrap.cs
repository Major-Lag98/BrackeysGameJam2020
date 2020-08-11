using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowTrap : MonoBehaviour
{
    public enum TrapFireTypeEnum { Regular, OneShot, Continous}

    public GameObject ProjectilePrefab;
    public float ProjectileSpeedAtSpawn = 1f;
    public TrapFireTypeEnum TrapFireType = TrapFireTypeEnum.Regular;
    public float FireDelay = 1f;
    public float InitialDelay = 0f;
    public int DamageToPlayerFromProjectile = 1;
    public bool TrapActive = true;

    private float _timerCounter = 0;

    private bool _shot = false;

    // Start is called before the first frame update
    void Start()
    {
        _timerCounter = FireDelay - InitialDelay; // Our ReadyDelay minus InitialDelay will be our starting time

        // Get the trigger area and attach a callback to it
        transform.GetChild(0).GetComponent<TriggerArea>().AddOntriggerEnterEvent(OnEnterTriggerArea);
    }

    private void OnEnterTriggerArea(Collider2D collision, TriggerArea area)
    {
        var acceptabletype = TrapFireType == TrapFireTypeEnum.Regular || TrapFireType == TrapFireTypeEnum.OneShot;
        // If our player entered the area and we're ready to fire and only if it's a regular fire type trap
        if (acceptabletype && ReadyToFire())
        {
            // Fire the projectile and reset the timer
            FireProjectile();
            _timerCounter = 0f;
        }
    }

    private void Update()
    {
        // If our trap type is regular or continous, try to count up
        if((TrapFireType == TrapFireTypeEnum.Continous || TrapFireType == TrapFireTypeEnum.Regular) && _timerCounter <= FireDelay)
        {
            _timerCounter += Time.deltaTime;

            // If our trap is specifically continous and we're ready to fire, then fire!
            if (ReadyToFire() && TrapFireType == TrapFireTypeEnum.Continous)
            {
                FireProjectile();
                _timerCounter -= FireDelay;
            }
        }
    }

    private void FireProjectile()
    {
        var projectile = ProjectileFactory.CreateProjectile(ProjectilePrefab, transform.rotation.eulerAngles.z, transform.position);
        var comp = projectile.GetComponent<Projectile>();
        comp.MovementSpeed = ProjectileSpeedAtSpawn;
        comp.ProjectileDamage = DamageToPlayerFromProjectile;

        projectile.transform.parent = transform.parent;

        _shot = true;
    }

    /// <summary>
    /// Returns true if ready to fire, false otherwise
    /// </summary>
    /// <returns></returns>
    private bool ReadyToFire()
        // We are ready to shoot immediately if we're the type OneShot
        => TrapActive && _timerCounter >= FireDelay || (TrapFireType == TrapFireTypeEnum.OneShot && !_shot);

    public void SetTrapActive(bool trapActive)
        => TrapActive = trapActive;

}
