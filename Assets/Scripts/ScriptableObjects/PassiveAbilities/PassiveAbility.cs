using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassiveAbility : ScriptableObject
{
    public new string name;
    public int cost;
    public int uses;
    public Sprite sprite;

    public virtual void Start()
    {
        
    }

    public virtual void Update()
    {
        
    }
}
