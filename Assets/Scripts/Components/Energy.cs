using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Energy : MonoBehaviour
{

    public float MaxEnergy = 100;
    public float PassiveEnergyRegen = 0.1f;

    private float currentEnergy = 100;

    void Update()
    {
        currentEnergy = Mathf.Clamp(currentEnergy + PassiveEnergyRegen, 0, MaxEnergy);
    }

    /// <summary>
    /// Drains energy from the current energy pull. If there is enough current energy for the 'amount'
    /// passed in, the energy will be drained and True will be returned. Returns false otherwise
    /// </summary>
    /// <param name="amount">The amount of energy to drain</param>
    /// <returns>True if energy was drained by the amount. False otherwise</returns>
    public bool UseEnergy(float amount)
    {
        if(currentEnergy >= amount)
        {
            currentEnergy = Mathf.Clamp(currentEnergy - amount, 0, MaxEnergy);
            return true;
        }

        return false;
    }

    /// <summary>
    /// Adds energy to the current energy
    /// </summary>
    /// <param name="amount">The amount of energy to add. </param>
    public void AddEnergy(float amount)
        => currentEnergy = Mathf.Clamp(currentEnergy + amount, 0, MaxEnergy);
}
