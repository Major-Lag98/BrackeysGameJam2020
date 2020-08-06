﻿using System.Collections;
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

    private float _timerCounter = 0;


    // Start is called before the first frame update
    void Start()
    {
        _timerCounter = FireDelay - InitialDelay; // Our ReadyDelay minus InitialDelay will be our starting time

        // Get the trigger area and attach a callback to it
        transform.GetChild(0).GetComponent<TriggerArea>().OnTriggerEnter += (collision) =>
        {
            // If our player entered the area and we're ready to fire and only if it's a regular fire type trap
            if (collision.gameObject.layer == LayerMask.NameToLayer("Player") && TrapFireType == TrapFireTypeEnum.Regular && ReadyToFire())
            {
                // Fire the projectile and reset the timer
                FireProjectile();
                _timerCounter = 0f;
            }
        };
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
        comp.DamageToPlayer = DamageToPlayerFromProjectile;
    }

    private bool ReadyToFire() 
        => _timerCounter >= FireDelay;

}
