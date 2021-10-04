using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColorLerpUI : MonoBehaviour
{
    public Image target;
    public ColorReference fromColor;
    public ColorReference toColor;
    public bool useInt = false;
    public FloatReference floatValue;
    public IntegerReference intValue;
    public FloatReference max;
    public FloatReference min;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        this.target.color = Color.Lerp(this.fromColor, this.toColor, GetValue());
    }

    private float GetValue()
    {
        return Mathf.InverseLerp(this.min, this.max, this.useInt ? this.intValue.Value : this.floatValue.Value);
    }

}
