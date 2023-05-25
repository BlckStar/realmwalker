using System;
using System.Collections;
using System.Linq;
using TMPro;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HQ : Building
{
    public Transform spawnpoint;
    public TextMeshProUGUI upgradeText;
    public ArrowTower tower;
    public int Health = 200;
    public Slider healthbar;
    public PlayerManager player;

    public int Cost => (int)(200 * math.pow(2, player.Level));

    private void Start()
    {
        tower = this.GetComponent<ArrowTower>();
        player = FindObjectsOfType<PlayerManager>().First(manager => manager.tag == this.tag);
        this.healthbar.maxValue = Health;
        this.healthbar.value = Health;
        base.Start();
    }

    public override void Interact()
    {
        int cost = Cost;
        if (this.PlayerManager.Money < cost)
        {
            return;
        }

        this.Health = cost;
        this.healthbar.maxValue = cost;
        this.healthbar.value = cost;
        PlayerManager.Money -= cost;
        PlayerManager.Income += cost / 10;
        PlayerManager.Level++;
        tower.Level = PlayerManager.Level;
        TriggerEnter();
    }

    public override void TriggerEnter()
    {
        int cost = (int)(200 * math.pow(2, PlayerManager.Level));
        this.upgradeText.text = "E to Upgrade (" + cost + " Gold) + " + (int)(cost / 10) + " Income";
    }


    public override void TriggerExit()
    {
    }

    public void Hit(int dmg)
    {
        this.Health -= dmg;
        this.healthbar.value = this.Health;

        if (this.Health <= 0)
        {
            LevelManager.Instance.EndGame(this.tag);
        }
    }
}