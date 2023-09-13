using System.Collections.Generic;
using System.Collections;
using UnityEngine.UI;
using UnityEngine;
using System;
using TMPro;

public class Main : MonoBehaviour
{
    public TextMeshProUGUI tCoinsCounter;
    public TextMeshProUGUI tCoinsCounter2;
    public TextMeshProUGUI tStats;
    public TextMeshProUGUI activatedPowerUpTextBox;

    public Text test;
    public Button OpenShop;

    //clickUpgrades
    public Upgrade clickPower;
    public TextMeshProUGUI tClickPower;
    public Button bClickPower;

    //autoClickers
    public Upgrade basicAutoClicker;
    public TextMeshProUGUI tBasicAutoClicker;
    public Button bBasicAutoClicker;

    public Upgrade reallyBadClickerBot;
    public TextMeshProUGUI tReallyBadClickerBot;
    public Button bReallyBadClickerBot;

    public Upgrade betterClickerBot;
    public TextMeshProUGUI tBetterClickerBot;
    public Button bBetterClickerBot;

    public Upgrade changeCoinBalanceScript;
    public TextMeshProUGUI tChangeCoinBalanceScript;
    public Button bChangeCoinBalanceScript;

    public Upgrade extraComputer;
    public TextMeshProUGUI tExtraComputer;
    public Button bExtraComputer;

    public Upgrade techSupport;
    public TextMeshProUGUI tTechSupport;
    public Button bTechSupport;

    //abilities

    public Upgrade luck;
    public TextMeshProUGUI tLuck;
    public Button bLuck;

    public Upgrade powerMultiplier;
    public TextMeshProUGUI tPowerMultiplier;
    public Button bPowerMultiplier;

    public Upgrade criticalChance;
    public TextMeshProUGUI tCriticalChance;
    public Button bCriticalChance;

    public double coinBalance;
    public float instantMinConstant = 0;
    public float instantMaxConstant = 1;
    public double instantLinearConstant = 5000;
    public float powerUpTimer;
    public bool isPowerUpActive = false;

    private double meshAutoClickPowerMultiplier = 1;
    private double meshClickPowerMultiplier = 1;
    private double meshCrticalHitMultiplier = 1;

    public string activePowerUp;

    public static List<Upgrade> upgrades;

    private double totalClickPower;
    private double totalAutoClickPower;

    private bool isDoubleClickPowerActivated = false;
    private bool isDoubleCoinAutomationActivated = false;

    private bool isCriticalHit = false;

    private float oneSecond = 1f;

    private double doubleClickPowerScale = 1;
    private double doubleCoinAutomationScale = 1;

    Animations animations;
    UpdateCookieMesh meshData;
    SettingsManager settings;


    void Awake()
    {
        hideGameObjects();

        upgrades = new List<Upgrade>();

        activePowerUp = "";

        powerUpTimer = 0f;

        animations = GameObject.Find("GameManager").GetComponent<Animations>();
        settings = GameObject.Find("GameManager").GetComponent<SettingsManager>();
        meshData = GameObject.Find("GameManager").GetComponent<UpdateCookieMesh>();

        //name, type, baseCost, level, costMultiplier, basePower, tierMultiple, textBox, button
        
        clickPower = new Upgrade("Click Power", "click", 20, 1, 1.5, 1, 10, tClickPower, bClickPower);
        basicAutoClicker = new Upgrade("Basic Auto Clicker", "auto", 100, 0, 1.5, 2, 10, tBasicAutoClicker, bBasicAutoClicker);
        reallyBadClickerBot = new Upgrade("Really Bad Clicker Bot", "auto", 1000, 0, 1.35, 20, 10, tReallyBadClickerBot, bReallyBadClickerBot);
        betterClickerBot = new Upgrade("Better Clicker Bot", "auto", 10000, 0, 1.30, 50, 10, tBetterClickerBot, bBetterClickerBot);
        changeCoinBalanceScript = new Upgrade("Change Coin Balance Script", "auto", 1000000, 0, 1.25, 150, 10, tChangeCoinBalanceScript, bChangeCoinBalanceScript);
        extraComputer = new Upgrade("Extra Computer", "auto", 500000, 0, 1.20, 500, 10, tExtraComputer, bExtraComputer);
        techSupport = new Upgrade("Tech Support", "auto", 100000000, 0, 1.20, 5000, 10, tTechSupport, bTechSupport);

        luck = new Upgrade("Luck", "ability", 5000, 1, 1.5, 2, tLuck, bLuck);
        powerMultiplier = new Upgrade("Power Multiplier", "multiplier", 6000, 0, 5, 2, tPowerMultiplier, bPowerMultiplier);
        criticalChance = new Upgrade("Critical Chance", "ability", 5000, 1, 1.5, 3, tCriticalChance, bCriticalChance);
        
        upgrades.Add(clickPower);
        upgrades.Add(basicAutoClicker);
        upgrades.Add(reallyBadClickerBot);
        upgrades.Add(betterClickerBot);
        upgrades.Add(changeCoinBalanceScript);
        upgrades.Add(extraComputer);
        upgrades.Add(techSupport);
        upgrades.Add(luck);
        upgrades.Add(powerMultiplier);
        upgrades.Add(criticalChance);

    }

    void hideGameObjects()
    {
        OpenShop.gameObject.SetActive(false);
        bClickPower.gameObject.SetActive(false);
        bBasicAutoClicker.gameObject.SetActive(false);
        bReallyBadClickerBot.gameObject.SetActive(false);
        bBetterClickerBot.gameObject.SetActive(false);
        bChangeCoinBalanceScript.gameObject.SetActive(false);
        bExtraComputer.gameObject.SetActive(false);
        bTechSupport.gameObject.SetActive(false);
    }

    void Update()
    {
        UpdatePower();
        UpdatePowerUpScales();
        UpdateStats();
        UpdateCoinsCounter();
        UpdateTextBox();
        UpdateVisibility();  
        LockUpgrade();      
        AutoClick();
        UnlockNextUpgrade();
        ActivatePowerUp();
        StopPowerUp();
        GetActivatedMesh();
    }

    void UpdatePowerUpScales()
    {
        if(isDoubleClickPowerActivated)
        {
            doubleClickPowerScale = 2;
        }
        else
        {
            doubleClickPowerScale = 1;
        }

        if(isDoubleCoinAutomationActivated)
        {
            doubleCoinAutomationScale = 2;
        }
        else
        {
            doubleCoinAutomationScale = 1;
        }

    }

    void GetActivatedMesh()
    {
        switch(meshData.currentStateString)
        {
            case "Cube":
                meshAutoClickPowerMultiplier = 1;
                meshClickPowerMultiplier = 1;
                meshCrticalHitMultiplier = 1;
                break;   
            case "Sphere":
                meshAutoClickPowerMultiplier = 2;
                meshClickPowerMultiplier = 1;
                meshCrticalHitMultiplier = 1;
                break;
            case "Octahedron":
                meshAutoClickPowerMultiplier = 1;
                meshClickPowerMultiplier = 3;
                meshCrticalHitMultiplier = 1;
                break;
            case "Icosahedron":
                meshAutoClickPowerMultiplier = 1;
                meshClickPowerMultiplier = 1;
                meshCrticalHitMultiplier = 5;
                break;
        }
    }
 
    public void CookieClick()
    {
        int randomNum = UnityEngine.Random.Range(0, 99);
        double criticalScale = 1;
        if(randomNum < criticalChance.power)
        {
            criticalScale = 10 * meshCrticalHitMultiplier;
            isCriticalHit = true;
            SoundManager.PlaySound("criticalHit");
        }
        else
        {
            SoundManager.PlaySound("cookieClick");
        }

        coinBalance += totalClickPower * criticalScale;
        animations.ClickCookie(isCriticalHit, totalClickPower, meshCrticalHitMultiplier);
        isCriticalHit = false;
    }

    public void SetActivePowerUp(string activePowerUp)
    {
        string createTextPowerUp = "";

        this.activePowerUp = activePowerUp;
        if(activePowerUp != "instantCoins")
        {
            powerUpTimer = UnityEngine.Random.Range(15f, 30f);
            isPowerUpActive = true;
            Debug.Log(powerUpTimer);
        }

        if(activePowerUp == "doubleClickPower")
            createTextPowerUp = "Double Click Power";
        else if(activePowerUp == "doubleCoinAutomation")
            createTextPowerUp = "Double Coin Automation";
        else if(activePowerUp == "instantCoins")
            createTextPowerUp = "Instant Coins";

        animations.CreateTextPowerUp(activatedPowerUpTextBox, createTextPowerUp, powerUpTimer);

    }

    void UpdateStats()
    {
        tStats.text = "Click Power: " + totalClickPower + " coins/click\nAuto Click Power: " + totalAutoClickPower + " coins/sec";
    }

    void UpdatePower()
    {
        double clickPowerResult = 0;
        double autoClickPowerResult = 0;
        double powerScale = 1;
        foreach(Upgrade upgrade in upgrades)
        {
            if(upgrade.type == "click")
            {
                clickPowerResult += upgrade.power;
            }
            else if(upgrade.type == "auto")
            {
                autoClickPowerResult += upgrade.power;
            }
        }
        if(powerMultiplier.power != 0)
        {
            powerScale = powerMultiplier.power;
        }
        totalClickPower = clickPowerResult * powerScale * meshClickPowerMultiplier * doubleClickPowerScale;
        totalAutoClickPower = autoClickPowerResult * powerScale * meshAutoClickPowerMultiplier * doubleCoinAutomationScale;
    }

    void UpdateCoinsCounter()
    {
        tCoinsCounter.text = "Coins: " + coinBalance;
        tCoinsCounter2.text = tCoinsCounter.text;
    }

    void UpdateVisibility()
    {
        foreach(Upgrade upgrade in upgrades)
        {
            if(upgrade.isUnlocked)
            {
                upgrade.button.gameObject.SetActive(true);
                // Debug.Log(upgrade.name + " is active");
            }
        }
    }

    void AutoClick()
    {
        oneSecond -= Time.deltaTime;
        if(oneSecond <= 0)
        {
            coinBalance += totalAutoClickPower;
            oneSecond += 1f;
        }
    }

    void UnlockNextUpgrade()
    {
        if(coinBalance >= 25)
        {
            clickPower.Unlock(true);
        }

        if(clickPower.tier >= 1)
        {
            OpenShop.gameObject.SetActive(true);
        }

        for(int i = 0; i < upgrades.Count; i++)
        {
            if(i + 1 != upgrades.Count && upgrades[i].level >= 10)
            {
                upgrades[i + 1].Unlock(true);
            }
        }
    }

    void LockUpgrade()
    {
        if(criticalChance.power >= 100)
        {
            criticalChance.button.gameObject.SetActive(false);
        }

        if(luck.power >= 100)
        {
            luck.button.gameObject.SetActive(false);
        }
    }

    void UpdateTextBox()
    {
        foreach(Upgrade upgrade in upgrades)
        {
            upgrade.UpdateTextBox(upgrade);
        }
    }

    void ActivatePowerUp()
    {
        switch(activePowerUp)
        {
            case "doubleClickPower":
                isDoubleClickPowerActivated = true;
                break;
            case "doubleCoinAutomation":
                isDoubleCoinAutomationActivated = true;
                break;
            case "instantCoins":
                InstantCoins();
                break;
        }

        activePowerUp = "";
    }

    void InstantCoins()
    {
        float minValue = (float)(totalAutoClickPower + totalClickPower) * instantMinConstant;
        float maxValue = (float)(totalAutoClickPower + totalClickPower) * instantMaxConstant;

        double instantCoins = Math.Floor((double)UnityEngine.Random.Range(minValue, maxValue)) + instantLinearConstant;

        coinBalance += instantCoins;
    }

    void StopPowerUp()
    {
        if(powerUpTimer >= 0)
            powerUpTimer -= Time.deltaTime;
        if(powerUpTimer <= 0)
        {
            isDoubleClickPowerActivated = false;
            isDoubleCoinAutomationActivated = false;
            isPowerUpActive = false;
            activePowerUp = "";
            powerUpTimer = 0;
        }
    }
}