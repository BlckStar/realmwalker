using System;
using System.Collections;
using JetBrains.Annotations;
using TMPro;
using System.Linq;
using Unity.Mathematics;
using UnityEngine;

public class ArrowTower : MonoBehaviour
{
    private int range = 5;
    private int _level = 1;
    private float speed = 0.5f;
    private float NextAttack = 0;
    public GameObject ArrowPrefab;

    public PlayerManager playerManager;

    public int Level
    {
        get { return _level; }
        set
        {
            _level = value;
            this.text.text = "LVL " + _level;
        }
    }

    [CanBeNull] public Minion lockedMinion;

    public void Update()
    {
        if (lockedMinion == null)
        {
            Collider[] colliders = Physics
                .OverlapSphere(transform.position, range, LayerMask.GetMask("Minion"))
                .Where(collider => collider.tag != this.tag).ToArray();

            if (colliders.Length > 0)
            {
               lockedMinion = colliders.First().GetComponent<Minion>();
            }

            return;
        }

        // find distance between minion and tower
        float distance = Vector3.Distance(this.transform.position, lockedMinion.transform.position);
        // if distance is less than range
        if (distance > this.range)
        {
            lockedMinion = null;
        }
        else
        {
            if (NextAttack < Time.time)
            {
                NextAttack = Time.time + speed;
                lockedMinion.Hit((int)(10f * Math.Pow(2f, this.Level)));
            }
        }
    }

    public void OnDrawGizmosSelected()
    {
        // Draw a yellow sphere at the transform's position
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, this.range);
    }

    public TextMeshProUGUI text;

    public int UpgradeCost
    {
        get { return (int)(10 * math.pow(2, this._level)); }
    }

    public void Start()
    {
        this.text.text = "LVL " + Level;
    }

    public void Upgrade()
    {
        if (playerManager.Money >= this.UpgradeCost)
        {
            this.range += 1;
            playerManager.Money -= this.UpgradeCost;
            this.Level++;
            this.speed *= 0.9f;
        }
    }
}