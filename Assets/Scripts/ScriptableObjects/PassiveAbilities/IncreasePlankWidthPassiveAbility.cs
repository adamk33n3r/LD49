using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Passive Abilities/Increase Plank Width")]
public class IncreasePlankWidthPassiveAbility : PassiveAbility
{
    public FloatVariable plankWidth;
    public FloatVariable cameraSize;
    public float newPlankWidth;
    public float newCameraSize;
    public override void Update()
    {
        base.Update();
        this.plankWidth.Value = newPlankWidth;
        this.cameraSize.Value = newCameraSize;
    }
}
