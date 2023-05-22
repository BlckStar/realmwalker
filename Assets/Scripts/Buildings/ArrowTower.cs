using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ArrowTower : MonoBehaviour
{
    public int Level = 1;
    public TextMeshProUGUI text;
    public int UpgradeCost
    {
        get
        {
            return this.Level * 20;
        }
    }

    public void Start()
    {
        this.text.text = "LVL " + Level;
    }

    public void Upgrade()
    {
        if (IncomeManager.Instance.Souls > this.UpgradeCost)
        {
            this.Level++;
            IncomeManager.Instance.Souls -= this.UpgradeCost;
        }
        
        this.text.text = "LVL " + Level;
    }
}