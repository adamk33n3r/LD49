using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VariableTextDisplayUI : MonoBehaviour
{
    public enum VariableType {
        Integer,
        Float,
    }
    public TMPro.TMP_Text text;
    [TextArea]
    public string preText;
    public VariableType variableType;
    public IntegerReference intRef;
    public FloatReference floatRef;

    public void Update()
    {
        this.text.text = this.preText;
        switch (this.variableType) {
            case VariableType.Integer:
                this.text.text += this.intRef.Value;
                break;
            case VariableType.Float:
                this.text.text += this.floatRef.Value.ToString("n2");
                break;
        }
    }
}
