using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlankIndicatorUI : MonoBehaviour
{
    public FloatReference plankRotation;

    public void Start()
    {
    }

    public void Update()
    {
        this.transform.rotation = Quaternion.Euler(0, 0, -this.plankRotation);
    }
}
