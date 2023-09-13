using System.Threading;
using UnityEngine.UI;
using UnityEngine;
using System;
using TMPro;

public class Upgrade
{
    public string name;
    public string type;

    public double baseCost;
    public double level;
    public double costMultiplier;
    public double basePower;
    public double tierMultiple;

    public TextMeshProUGUI textBox;
    public Button button;

    public double cost;
    public double power;
    public double tierMultiplier;
    public double tier;
    public int order;
    public bool isUnlocked = false;

    private static int instanceCount;

    public Upgrade(string name, string type, double baseCost, double level, double costMultiplier, double basePower, double tierMultiple, TextMeshProUGUI textBox, Button button)
    {
        Interlocked.Increment(ref instanceCount);

        this.name = name;
        this.type = type;

        this.baseCost = baseCost;
        this.level = level;
        this.costMultiplier = costMultiplier;
        this.basePower = basePower;
        this.tierMultiple = tierMultiple;

        this.textBox = textBox;
        this.button = button;

        cost = baseCost;
        power = basePower;
        tierMultiplier = 2;
        tier = 0;
        order = instanceCount;


        UpdateUpgrade();
    }

    public Upgrade(string name, string type, double baseCost, double level, double costMultiplier, double basePower, TextMeshProUGUI textBox, Button button)
    {
        Interlocked.Increment(ref instanceCount);

        this.name = name;
        this.type = type;

        this.baseCost = baseCost;
        this.level = level;
        this.costMultiplier = costMultiplier;
        this.basePower = basePower;
        this.textBox = textBox;
        this.button = button;

        cost = baseCost;
        power = basePower;

        order = instanceCount;


        UpdateUpgrade();
    }

    public void IncreaseLevel()
    {
        level++;
        UpdateUpgrade();
    }

    public void UpdateTextBox(Upgrade upgrade)
    {
        textBox.text = upgrade.ToString();
    }

    public void Unlock(bool unlocked)
    {
        isUnlocked = unlocked;
    }

    public void SetLevel(double level)
    {
        this.level = level;
        UpdateUpgrade();
    }

    private void UpdateUpgrade()
    {
        cost = Math.Ceiling(baseCost * Math.Pow(costMultiplier, level));
        if(type == "click" || type == "auto")
        {
            tier = Math.Floor(level / tierMultiple);
            power = basePower * level * Math.Pow(tierMultiplier, tier);
        }
        else
        {
            power = level * basePower;
        }
    }

    private double getPowerIncrease()
    {
        double tempLevel = level + 1;
        double tempTier = Math.Floor(tempLevel / tierMultiple);
        double nextPower = basePower * tempLevel * Math.Pow(tierMultiplier, tempTier);
        double powerIncrease = nextPower - power;
        return powerIncrease;
    }


    public override string ToString()
    {
        string info = "";
        if(type == "click" || type == "auto")
        {
            info = name + "\nCost: " + cost + " coins\nCurrent Level: " + level + "\nPower+ " + getPowerIncrease();
        }
        else if(type == "ability")
        {
            info = name + "\nCost: " + cost + " coins\nCurrent: " + power + " %\n% " + basePower;
        }
        else if(type == "multiplier")
        {
            info = name + "\nCost: " + cost + " coins\nCurrent Multiplier: " + power + "\n+" + basePower;
        }
        return info;
    }

}
