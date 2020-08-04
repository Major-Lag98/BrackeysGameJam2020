using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour, IDamageable
{
    [SerializeField]
    int health = 10;

    public void Damage()
    {
        health -= 1;
        Debug.Log("Took Damage, health = " + health);
    }
}
