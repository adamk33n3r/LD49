using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeightSliderUI : MonoBehaviour
{
    public FloatReference minHeight;
    public FloatReference maxHeight;
    public Vector3Variable targetCameraPosition;

    private Slider slider;

    public void Start()
    {
        this.slider = GetComponent<Slider>();
    }

    public void Update()
    {
        float normalized = Mathf.InverseLerp(this.minHeight, this.maxHeight, this.targetCameraPosition.Value.y);
        this.slider.SetValueWithoutNotify(normalized);
    }

    public void SetCameraTarget(float val)
    {
        this.targetCameraPosition.Value.y = Mathf.Lerp(this.minHeight, this.maxHeight, val);
    }
}
