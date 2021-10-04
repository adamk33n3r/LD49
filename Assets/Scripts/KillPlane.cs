using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class KillPlane : MonoBehaviour
{
    public ScriptableEvents.Events.GameObjectScriptableEvent KillPlaneEnteredEvent;
    public void OnTriggerEnter2D(Collider2D other)
    {
        KillPlaneEnteredEvent.Raise(other.gameObject);
    }
}
