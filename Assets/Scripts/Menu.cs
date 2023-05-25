using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    public List<QueueItem> queue;

    public List<MinionMenuItem> items = new List<MinionMenuItem>();
    public MinionMenuItem MinionMenuItemPrototype;
    public RectTransform MinionListTransform;

    public ScrollRect scroll;
    private int active = 0;
    public PlayerManager playerManager;

    public void Interact()
    {
        if (this.items[active].Interact())
        {
            QueueItem item = this.queue.First(item => !item.occupied);
            item.Set(this.items[active].Level);
        }
    }

    private void Start()
    {
        playerManager.OnMinionSpawned.AddListener(this.regenerateQueue);
        playerManager.OnMoneyChanged.AddListener((money) => { this.items.ForEach((item) => item.RefreshGUI()); });
        OnOpen();
        this.items[0].Active = true;
        this.items[0].RefreshGUI();

    }

    private void regenerateQueue()
    {

        int i = 0;
        QueueItem item = this.queue.ElementAt(0);
        do
        {
            i++;
            item.CopyFrom(this.queue.ElementAt(i));
            item = this.queue.ElementAt(i);

        } while (item.occupied);
    }

    public void OnOpen()
    {
        if (playerManager.Level > this.items.Count)
        {
            for (int i = this.items.Count; i < playerManager.Level; i++)
            {
                MinionMenuItem item = Instantiate(MinionMenuItemPrototype, MinionListTransform.transform);
                item.Level = i +1;
                item.Cost = (int)(10f * Math.Pow(2f, i));
                item.playerManager = this.playerManager;
                item.RefreshGUI();
                items.Add(item);
            }
        }
    }

    public void navigate(float axis)
    {
        if (axis == 0)
        {
            return;
        }

        if (active - axis < 0 || active - axis >= this.items.Count)
        {
            return;
        }

        this.items[active].Active = false;
        active -= (int)axis;
        this.items[active].Active = true;
        this.items.ForEach(item => item.RefreshGUI());
        scroll.verticalNormalizedPosition = 1f - (active * 0.2f);
    }
}