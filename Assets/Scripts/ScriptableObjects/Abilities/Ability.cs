using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Ability : ScriptableObject
{
    public enum AbilityState {
        Ready,
        Active,
        Cooldown,
    }
    public new string name;
    public int cost;
    public int uses;
    public Sprite sprite;
    public float cooldownTime;
    private float remainingCooldownTime;
    public float activeTime;
    private float remainingActiveTime;
    public float RemainingCooldownTime => this.remainingCooldownTime;
    public float RemainingActiveTime => this.remainingActiveTime;

    protected AbilityState state = AbilityState.Ready;

    public void OnValidate()
    {
        // Reset these. Feels bad that they are in here.
        this.Reset();
    }

    public virtual void Activate(GameObject placeholder)
    {
        if (this.state == AbilityState.Ready) {
            this.state = AbilityState.Active;
            this.remainingActiveTime = this.activeTime;
        }
    }

    public virtual bool CanActivate(GameObject placeholder)
    {
        return this.state == AbilityState.Ready;
    }

    public virtual void Start()
    {

    }

    public virtual void Update()
    {
        switch (this.state) {
            case AbilityState.Active:
                if (this.remainingActiveTime > 0) {
                    this.remainingActiveTime -= Time.deltaTime;
                } else {
                    this.state = AbilityState.Cooldown;
                    this.remainingCooldownTime = this.cooldownTime;
                }
                break;
            case AbilityState.Cooldown:
                if (this.remainingCooldownTime > 0) {
                    this.remainingCooldownTime -= Time.deltaTime;
                } else {
                    this.state = AbilityState.Ready;
                }
                break;
        }
    }

    public virtual void Reset()
    {
        this.remainingActiveTime = 0;
        this.remainingCooldownTime = 0;
        this.state = AbilityState.Ready;
    }
}
