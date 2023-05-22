using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class IncomeManager : MonoBehaviour
{
    private int _souls = 0;
    public int Souls
    {
        get
        {
            return _souls;
        }
        set
        {
            _souls = value;
            moneyText.text = value.ToString();
        }
    }

    private int _income = 0;
    public int Income
    {
        get
        {
            return _income;
        }
        set
        {
            _income = value;
            incomeText.text = "+ " + value.ToString();
        }
    }
    public float NextIncome = 0f;

    public float IncomeEvery = 10f;

    public Slider Slider;
    public static IncomeManager Instance;

    public TMPro.TextMeshProUGUI moneyText;
    public TMPro.TextMeshProUGUI incomeText;

    public void Start()
    {
        Souls = 100;
        Income = 100;
        NextIncome = IncomeEvery;
        Slider.minValue = 0;
        Slider.maxValue = IncomeEvery;
        Instance = this;

    }

    public void Update()
    {
        NextIncome -= Time.deltaTime;

        if (NextIncome < 0)
        {
            Souls += Income;
            NextIncome = IncomeEvery;
        }

        Slider.value = IncomeEvery - NextIncome;
    }
    
    

}
