using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Passive Abilities/Increase Torque")]
public class IncreaseTorquePassiveAbility : PassiveAbility
{
    public FloatVariable torqueSpeed;
    public float torqueAmount;
    public override void Update()
    {
        base.Update();
        this.torqueSpeed.Value = torqueAmount;
    }
}
