using UnityEngine;
using TMPro;

public class SettingsManager : MonoBehaviour
{

    Main main;
    UpdateCookieMesh meshData;
    Animations animations;

    public TextMeshProUGUI autoSaveText;

    public TextMeshProUGUI quitText;

    public float exitTimer = 5f;

    private float privateExitTimer;

    public float autoSaveTimer = 60f;
    private float privateAutoSaveTimer;

    private bool autoLoaded = true;

    private bool notAutoSaved = true;


    // public GameObject cookie;

    void Awake()
    {
        privateExitTimer = exitTimer;
        privateAutoSaveTimer = autoSaveTimer;
        quitText.gameObject.SetActive(false);
        main = GameObject.Find("GameManager").GetComponent<Main>();
        meshData = GameObject.Find("GameManager").GetComponent<UpdateCookieMesh>();
        animations = GameObject.Find("GameManager").GetComponent<Animations>();
        LoadPlayer();
    }

    void Update() {
        if(Input.GetKey(KeyCode.Escape)) 
        {
            privateExitTimer -= Time.deltaTime;
            quitText.gameObject.SetActive(true);

            if(privateExitTimer <= 0.5)
                quitText.text = "Quitting...";
            else if(privateExitTimer <= 1.5)
                quitText.text = "Quitting..";
            else if(privateExitTimer <= 2.5)
                quitText.text = "Quitting.";

            if(privateExitTimer <= 0)
            {
                Debug.Log("Exiting");
                Application.Quit();
            }
        }
        else
        {
            quitText.gameObject.SetActive(false);
            quitText.text = "Quitting";
            privateExitTimer = exitTimer;
        }

        AutoSave();
    }

    
    void AutoSave()
    {
        autoSaveTimer -= Time.deltaTime;
        if(autoSaveTimer <= 0)
        {
            notAutoSaved = false;
            SavePlayer();
            Debug.Log("Auto Saved Player");
            animations.CreateText(autoSaveText);
            autoSaveTimer += privateAutoSaveTimer;
        }
    }

    public void SavePlayer()
    {
        SaveSystem.SavePlayer(main, meshData);
        
        if(notAutoSaved) SoundManager.PlaySound("saveLoad");

        notAutoSaved = true;

        Debug.Log("Saved Player");
    }

    public void LoadPlayer()
    {
        PlayerData data = SaveSystem.LoadPlayer();

        if(data != null)
        {
            main.coinBalance = data.coinBalance;

            main.clickPower.SetLevel(data.clickPowerLevel);

            main.basicAutoClicker.SetLevel(data.basicAutoClickerLevel);
            main.reallyBadClickerBot.SetLevel(data.reallyBadClickerBotLevel);
            main.betterClickerBot.SetLevel(data.betterClickerBotLevel);
            main.changeCoinBalanceScript.SetLevel(data.changeCoinBalanceScriptLevel);
            main.extraComputer.SetLevel(data.extraComputerLevel);
            main.techSupport.SetLevel(data.techSupportLevel);

            main.luck.SetLevel(data.luckLevel);
            main.powerMultiplier.SetLevel(data.powerMultiplierLevel);
            main.criticalChance.SetLevel(data.criticalChanceLevel);

            if(data.isSphereBought)
                meshData.SphereMesh.BuyMesh();
            if(data.isOctahedronBought)
                meshData.OctahedronMesh.BuyMesh();
            if(data.isIcosahedronBought)
                meshData.IcosahedronMesh.BuyMesh();

            if(data.isCubeActivatedMesh)
            {
                meshData.LoadCookieMesh("Cube");
            }
            if(data.isSphereActivatedMesh)
            {
                meshData.LoadCookieMesh("Sphere");
            }
            if(data.isOctahedronActivatedMesh)
            {
                meshData.LoadCookieMesh("Octahedron");
            }
            if(data.isIcosahedronActivatedMesh)
            {
                meshData.LoadCookieMesh("Icosahedron");
            }
            
            Debug.Log("Loading Player");

            if(!autoLoaded) SoundManager.PlaySound("saveLoad");
        }

        autoLoaded = false;
    }
}
