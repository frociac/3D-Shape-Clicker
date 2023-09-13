using UnityEngine;

[System.Serializable]
public class PlayerData
{

    public double coinBalance;

    public double clickPowerLevel;
    public double basicAutoClickerLevel;
    public double reallyBadClickerBotLevel;
    public double betterClickerBotLevel;
    public double changeCoinBalanceScriptLevel;
    public double extraComputerLevel;
    public double techSupportLevel;

    public double luckLevel;
    public double powerMultiplierLevel;
    public double criticalChanceLevel;

    public bool isSphereBought;
    public bool isOctahedronBought;
    public bool isIcosahedronBought;

    public bool isCubeActivatedMesh;
    public bool isSphereActivatedMesh;
    public bool isOctahedronActivatedMesh;
    public bool isIcosahedronActivatedMesh;

// SettingsManager playerSettings
    public PlayerData(Main player, UpdateCookieMesh meshData)
    {
        coinBalance = player.coinBalance;

        clickPowerLevel = player.clickPower.level;

        basicAutoClickerLevel = player.basicAutoClicker.level;
        reallyBadClickerBotLevel = player.reallyBadClickerBot.level;
        betterClickerBotLevel = player.betterClickerBot.level;
        changeCoinBalanceScriptLevel = player.changeCoinBalanceScript.level;
        extraComputerLevel = player.extraComputer.level;
        techSupportLevel = player.techSupport.level;

        luckLevel = player.luck.level;
        powerMultiplierLevel = player.powerMultiplier.level;
        criticalChanceLevel = player.criticalChance.level;
        
        isSphereBought = meshData.SphereMesh.isBought;
        isOctahedronBought = meshData.OctahedronMesh.isBought;
        isIcosahedronBought = meshData.IcosahedronMesh.isBought;

        isCubeActivatedMesh = meshData.CubeMesh.isActivated;
        isSphereActivatedMesh = meshData.SphereMesh.isActivated;
        isOctahedronActivatedMesh = meshData.OctahedronMesh.isActivated;
        isIcosahedronActivatedMesh = meshData.IcosahedronMesh.isActivated;


    }

}
