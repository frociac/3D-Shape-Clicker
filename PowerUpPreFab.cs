using UnityEngine;

public class PowerUpPreFab : MonoBehaviour
{

    Main main;

    GeneratePowerup generate;

    void Awake()
    {
        main = GameObject.Find("GameManager").GetComponent<Main>();
        generate = GameObject.Find("GameManager").GetComponent<GeneratePowerup>();
    }
    public void OnClick()
    {
        int randomNum = UnityEngine.Random.Range(1, 4);

        string activePowerUp = "";
        
        switch(randomNum)
        {
            case 1:
                activePowerUp = "doubleClickPower";
                break;
            case 2:
                activePowerUp = "doubleCoinAutomation";
                break;
            case 3:
                activePowerUp = "instantCoins";
                break;
        }

        Debug.Log(activePowerUp);

        main.SetActivePowerUp(activePowerUp);

        SoundManager.PlaySound("powerUp");

        Destroy(gameObject);
    }
}
