using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

abstract public class Building : MonoBehaviour
{
    public GameObject TriggerEffect;

    private void OnTriggerEnter(Collider other)
    {
        if (this.PlayerManager.isAI)
        {
            return;
        }

        if (other.tag != this.tag)
        {
            return;
        }

        this.TriggerEffect.SetActive(true);

        this.PlayerManager.CurrentBuilding = this;
        this.TriggerEnter();
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag != this.tag)
        {
            return;
        }

        this.TriggerEffect.SetActive(false);

        this.PlayerManager.CurrentBuilding = null;
        this.TriggerExit();
    }

    public abstract void Interact();

    public abstract void TriggerEnter();

    public abstract void TriggerExit();

    protected PlayerManager PlayerManager;

    public void Start()
    {
        this.PlayerManager = FindObjectsOfType<PlayerManager>().Where(manager => manager.tag == this.tag).First();
    }
}