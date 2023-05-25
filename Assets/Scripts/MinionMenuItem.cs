using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MinionMenuItem : MonoBehaviour
{
    public bool _active = false;
    private Image bgImage;
    public int Cost;
    public int Level;

    public TextMeshProUGUI textMoney;
    public TextMeshProUGUI textLvl;
    public TextMeshProUGUI textIncome;

    public PlayerManager playerManager;
    public bool Interact()
    {
        return this.playerManager.SpawnMinionQueued(this.Level);
    }

    public void RefreshGUI()
    {
        this.Cost =  (int)(10f * Math.Pow(2f, Level));
        this.textMoney.text = this.Cost + "G";
        this.textLvl.text = "LVL " + this.Level;
        this.textIncome.text = "Income +" + this.Cost/5;
        Active = Active;
        if (this.playerManager.Money < Cost)
        {
            this.textMoney.color = Color.grey;
        }
        else
        {
            this.textMoney.color = Color.white;
        }
    }

    public bool Active
    {
        get { return _active; }
        set
        {
            _active = value;

            Color color = bgImage.color;
            if (_active)
            {
                color.a = 80f;
            }
            else
            {
                color.a = 0f;
            }

            bgImage.color = color;
        }
    }

    public void Awake()
    {
        this.bgImage = this.GetComponent<Image>();
    }
}