using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Passive Abilities/Auto Balance")]
public class AutoBalancePassiveAbility : PassiveAbility
{
    public float torqueAmount;
    private RotatePlank plankObj;
    private Rigidbody2D plankRigidbody2D;

    public override void Start()
    {
        this.plankObj = FindObjectOfType<RotatePlank>();
        this.plankRigidbody2D = this.plankObj.GetComponent<Rigidbody2D>();
    }

    public override void Update()
    {
        base.Update();
        if (this.plankObj == null) {
            Start();
        }

        float curRot = this.plankObj.transform.rotation.eulerAngles.z;
        curRot = (curRot > 180) ? curRot - 360 : curRot;

        float amount = Mathf.SmoothStep(0, this.torqueAmount, Mathf.Abs(curRot) / 30);

        this.plankRigidbody2D.AddTorque(curRot < 0 ? amount : -amount);
    }
}
