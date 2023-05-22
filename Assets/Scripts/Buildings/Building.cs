
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class Building : MonoBehaviour
{
    public GameObject TriggerEffect;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag != this.tag)
        {
            return;
        }

        this.TriggerEffect.SetActive(true);

        InputManager.Instance.CurrentBuilding = this;
        this.TriggerEnter();
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag != this.tag)
        {
            return;
        }

        this.TriggerEffect.SetActive(false);
        
        InputManager.Instance.CurrentBuilding = null;
        this.TriggerExit();
    }

    public abstract void Interact();
    
    public abstract void TriggerEnter();
    
    public abstract void TriggerExit();
}