using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityController : MonoBehaviour
{
    public Inventory playerInventory;
    public IntegerReference selectedAbility;

    public float CooldownTime => this.cooldownTime;
    public float ActiveTime => this.activeTime;

    private float cooldownTime;
    private float activeTime;

    enum AbilityState {
        Ready,
        Active,
        Cooldown,
    }

    private AbilityState state = AbilityState.Ready;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Ability ability = this.playerInventory.abilities[this.selectedAbility];
        switch (this.state) {
            case AbilityState.Ready:
                if (Input.GetButtonDown("Fire1")) {
                    // ability.Activate(this.gameObject);
                    this.state = AbilityState.Active;
                    this.activeTime = ability.activeTime;
                }
                break;
            case AbilityState.Active:
                if (this.activeTime > 0) {
                    this.activeTime -= Time.deltaTime;
                } else {
                    this.state = AbilityState.Cooldown;
                    this.cooldownTime = ability.cooldownTime;
                }
                break;
            case AbilityState.Cooldown:
                if (this.cooldownTime > 0) {
                    this.cooldownTime -= Time.deltaTime;
                } else {
                    this.state = AbilityState.Ready;
                }
                break;
        }
    }
}
