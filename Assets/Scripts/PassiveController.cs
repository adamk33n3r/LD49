using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassiveController : MonoBehaviour
{
    public PassiveInventory passiveInventory;
    public BoolReference isGamePaused;

    void Start()
    {
        foreach (PassiveAbility passive in this.passiveInventory.passives) {
            passive.Start();
        }
    }

    void Update()
    {
        if (this.isGamePaused) {
            return;
        }
        foreach (PassiveAbility passive in this.passiveInventory.passives) {
            passive.Update();
        }
    }
}
