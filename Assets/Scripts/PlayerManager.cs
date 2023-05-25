using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour
{
    public bool isAI;
    public Player Player;
    public static PlayerManager Instance;
    public int Level = 1;

    public UnityEvent OnMinionSpawned;
    private void Awake()
    {
        Money = 100;
        Income = 100;
        NextIncome = IncomeEvery;
        Instance = this;

        if (!isAI)
        {
            Slider.minValue = 0;
            Slider.maxValue = IncomeEvery;
        }
    }


    public Building CurrentBuilding;

    private bool MenuOpened = false;
    public Menu Menu;

    private bool isPaused;


    // Get Event From Input Manager
    public void OnMove(InputAction.CallbackContext action)
    {
        if (isPaused)
        {
            return;
        }

        float axis = action.ReadValue<float>();
        Player.Direction = axis;
    }

    public void OnInteract(InputAction.CallbackContext action)
    {
        if (isPaused)
        {
            return;
        }


        if (action.ReadValue<float>() > 0)
        {
            if (MenuOpened)
            {
                Menu.Interact();
                return;
            }

            if (CurrentBuilding)
            {
                CurrentBuilding.Interact();
                return;
            }
        }
    }

    public void Update()
    {
        NextIncome -= Time.deltaTime;

        if (NextIncome < 0)
        {
            Money += Income;
            NextIncome = IncomeEvery;
        }

        if (queueSize > 0 && SpawnRequestedTime + 1f < Time.time)
        {
            Debug.Log(queueSize);
            int MinionLevel = MinionQueue.Dequeue();
            Minion minion = Instantiate(prototype, spawnPoint.transform);
            minion.transform.localRotation =
                Quaternion.Euler(0, 180, this.tag == "Player1" ? 180 : 0);
            minion.Level = MinionLevel;
            minion.Destination = destination.position;
            minion.tag = this.tag;
            minion.OnSpawn();
            OnMinionSpawned.Invoke();

            queueSize--;
            SpawnRequestedTime = Time.time;
        }

        if (!isAI)
        {
            Slider.value = IncomeEvery - NextIncome;
        }
    }

    public void OnMenu(InputAction.CallbackContext action)
    {
        if (isPaused)
        {
            return;
        }

        if (action.ReadValue<float>() > 0)
        {
            MenuOpened = !MenuOpened;

            this.Menu.GameObject().SetActive(MenuOpened);
            this.Menu.OnOpen();
        }
    }

    public void OnPause(InputAction.CallbackContext action)
    {
        if (action.ReadValue<float>() > 0)
        {
            isPaused = !isPaused;

            Time.timeScale = isPaused ? 0 : 1f;
        }
    }

    public void OnMenuMove(InputAction.CallbackContext action)
    {
        if (!MenuOpened)
        {
            return;
        }

        float axis = action.ReadValue<float>();
        Menu.navigate(axis);
    }

    private int _money = 0;

    public int Money
    {
        get { return _money; }
        set
        {
            _money = value;
            if (!isAI)
            {
                moneyText.text = value.ToString();
            }

            OnMoneyChanged.Invoke(_money);
        }
    }

    public UnityEvent<int> OnMoneyChanged;

    private int _income = 0;

    public int Income
    {
        get { return _income; }
        set
        {
            _income = value;
            if (!isAI)
            {
                incomeText.text = "+ " + value.ToString();
            }
        }
    }

    public float NextIncome = 0f;

    public float IncomeEvery = 10f;

    public Slider Slider;

    public TMPro.TextMeshProUGUI moneyText;
    public TMPro.TextMeshProUGUI incomeText;


    public Minion prototype;
    public Transform spawnPoint;
    public Transform destination;

    private Queue<int> MinionQueue = new Queue<int>();
    private int queueSize = 0;

    public bool SpawnMinionQueued(int Level)
    {
        int Cost = (int)(10f * Math.Pow(2f, Level));
        if (this.Money < Cost || queueSize >= 10)
        {
            return false;
        }

        this.Money -= Cost;
        this.Income += Cost / 5;

        MinionQueue.Enqueue(Level);
        if (queueSize == 0)
        {
            SpawnRequestedTime = Time.time;
        }
        queueSize++;

        return true;
    }

    public float SpawnRequestedTime;
}