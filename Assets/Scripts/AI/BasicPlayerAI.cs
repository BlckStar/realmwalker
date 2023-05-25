using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using Random = Unity.Mathematics.Random;

[RequireComponent(typeof(PlayerManager))]
public class BasicPlayerAI : MonoBehaviour
{
    public Vector3 offsetPosition;
    private Queue<Task> _tasks = new Queue<Task>();
    private PlayerManager _playerManager;
    private Player player;
    private bool noPlotsLeft = false;
    private Task currentTask;
    private Vector3 destination;
    private TowerPlot currentPlot;
    private HQ currentHQ;

    // Start is called before the first frame update
    void Start()
    {
        this._playerManager = GetComponent<PlayerManager>();
        player = _playerManager.Player;
        currentHQ = FindObjectsByType<HQ>(FindObjectsSortMode.None).Where(HQ => HQ.tag == this.tag).First();
        this._tasks.Enqueue(Task.Build);
        Random random = new Random((uint)System.DateTime.Now.Millisecond);

        for (int i = 0; i < 2; i++)
        {
            int action = random.NextInt(1, 3);
            this._tasks.Enqueue((Task)action);
        }

        WorkQueue();
    }

    private void WorkQueue()
    {
        if (!this._tasks.TryDequeue(out this.currentTask))
        {
            Random random = new Random((uint)System.DateTime.Now.Millisecond);

            for (int i = 0; i < 10; i++)
            {
                // statistically, 
                // 0 = HQ
                // 1 = build
                // 2 = upgrade
                // 3-9 = spawn minion
                int action = random.NextInt(0, 20);
                    action = (int)Mathf.Min(action, 3);
                this._tasks.Enqueue((Task)action);
            }

            this.currentTask = this._tasks.Dequeue();
        }

        switch (currentTask)
        {
            case Task.HQ:
                this.TryHQUpgdrade();
                break;
            case Task.Build:
                if (this.noPlotsLeft)
                {
                    WorkQueue();
                    return;
                }
                this.FindPlot();
                break;
            case Task.Upgrade:
                this.FindPlot();
                break;
            case Task.SpawnMinion:
                this.SpawnMinion();
                break;
        }
    }

    private void TryHQUpgdrade()
    {
        if (currentHQ.Cost <= _playerManager.Money)
        {
            player._direction = 1;
        }
    }

    private void FindPlot()
    {
        IEnumerable<TowerPlot> plots = FindObjectsByType<TowerPlot>(FindObjectsSortMode.None)
            .Where(plot => plot.tag == this.tag);

        if (currentTask == Task.Build)
        {
            plots = plots.Where(plot => !plot.isBuilt);
        }
        else
        {
            plots = plots.Where(plot => plot.isBuilt);
        }

        TowerPlot[] towerPlots = plots.ToArray();

        if (currentTask == Task.Build)
        {
            if (towerPlots.Length == 1)
            {
                this.noPlotsLeft = true;
            }
        }

        Array.Sort(towerPlots,(a, b) => (int)(b.transform.position.x - a.transform.position.x));
        this.currentPlot = towerPlots[0];
        if (currentPlot.Cost > _playerManager.Money)
        {
            WorkQueue();
            return;
        }

        if (currentPlot.transform.position.x < player.transform.position.x)
        {
            player.Direction = -1;
        }
        else
        {
            player.Direction = 1;
        }
    }


    private void Update()
    {
        if (currentTask == Task.HQ && player.transform.position.x - currentHQ.transform.position.x < 0.1f)
        {
            currentHQ.Interact();
            WorkQueue();
            return;
        }
        
        if (currentPlot != null)
        {
            // Find distance of x coordinates
            if (player.transform)
            
                
            if (math.abs(player.transform.position.x - currentPlot.transform.position.x) < 0.1f)
            { 
                currentPlot.Interact();
                currentPlot = null;
                player.Direction = 0;
                WorkQueue();
            }
        }

      
    }


    public void SpawnMinion()
    {
        int level = _playerManager.Level;
        bool done = true;
        do
        {
            done = this._playerManager.SpawnMinionQueued(level);
            level--;
        } while (!done && level > 0);

        WorkQueue();
    }
}