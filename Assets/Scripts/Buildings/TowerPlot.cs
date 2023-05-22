using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TowerPlot : Building
{
    public Transform spawnPos;
    public GameObject ArrowTowerPrototype;
    private bool isBuilt = false;
    public int cost = 20;
    public TextMeshProUGUI text;
    public ArrowTower tower;
    
    
    public override void Interact()
    {
        if (!isBuilt)
        {
            if (IncomeManager.Instance.Souls > cost)
            {
                this.Build();
            }
            return;
        }

        if (IncomeManager.Instance.Souls > this.tower.UpgradeCost)
        {
            this.tower.Upgrade();
            TriggerEnter();
        }
    }

    private void Build()
    {
        IncomeManager.Instance.Souls -= cost;
        GameObject newTower = Instantiate(this.ArrowTowerPrototype, this.transform, false);
        newTower.transform.position = this.spawnPos.position;
        this.tower = newTower.GetComponent<ArrowTower>();
        this.isBuilt = true;
        TriggerEnter();
    }
    
    
    
    public override void TriggerEnter()
    {
        if (!isBuilt)
        {
            return;
        }

        this.text.text = "E to Upgrade (" + this.tower.UpgradeCost +" Gold)";

    }
    
    
    public override void TriggerExit()
    {

    }
}