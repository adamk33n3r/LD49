using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlankColor : MonoBehaviour
{
    public FloatReference plankRotation;
    public FloatReference plankAngleLimit;
    public ColorVariable stableColor;
    public ColorVariable unstableColor;

    private Image imageComponent;
    private SpriteRenderer spriteRendererComponent;

    public void Start()
    {
        this.imageComponent = GetComponent<Image>();
        this.spriteRendererComponent = GetComponent<SpriteRenderer>();
    }

    public void Update()
    {
        float absRotation = Mathf.Abs(this.plankRotation);
        Color newColor = Color.Lerp(this.stableColor.Value, this.unstableColor.Value, absRotation / this.plankAngleLimit);
        if (this.imageComponent != null) {
            this.imageComponent.color = newColor;
        }
        if (this.spriteRendererComponent != null) {
            this.spriteRendererComponent.color = newColor;
        }
    }
}
