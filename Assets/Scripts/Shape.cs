using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shape : MonoBehaviour
{
    public GameObjectRuntimeSet shapeSet;
    public bool IsFrozen => false; 
    public bool HasTouchedObject => true;

    public void OnEnable() {
        this.shapeSet.Add(this.gameObject);
    }

    public void OnDisable() {
        this.shapeSet.Remove(this.gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
