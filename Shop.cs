using System.Collections.Generic;
using UnityEngine.EventSystems;
using System.Collections;
using UnityEngine.UI;
using UnityEngine;

public class Shop : MonoBehaviour
{

    Animations animations;

    void Awake()
    {
        animations = GameObject.Find("GameManager").GetComponent<Animations>();
    }

    public void OnUpgradeClick()
    {
        Button clickedButton = EventSystem.current.currentSelectedGameObject.GetComponent<Button>();
        Upgrade upgrade = GetUpgrade(clickedButton);
        if(upgrade != null)
            BuyUpgrade(upgrade);
    }


    Upgrade GetUpgrade(Button clickedButton)
    {
        Upgrade currentUpgrade = null;
        foreach (Upgrade upgrade in Main.upgrades)
        {
            if(upgrade.button == clickedButton)
            {
                currentUpgrade = upgrade;
                break;
            }
        }
        return currentUpgrade;
    }

    void BuyUpgrade(Upgrade upgrade)
    {
        animations.BounceShape();
        if(GameObject.Find("GameManager").GetComponent<Main>().coinBalance >= upgrade.cost)
        {
            SoundManager.PlaySound("upgrade");
            GameObject.Find("GameManager").GetComponent<Main>().coinBalance -= upgrade.cost;
            upgrade.IncreaseLevel();
            // Debug.Log(upgrade);
        }
        else
        {
            SoundManager.PlaySound("notEnoughCoins");
            // Debug.Log("not enough coins");
        }
    }


}
