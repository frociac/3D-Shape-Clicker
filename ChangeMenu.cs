using UnityEngine;

public class ChangeMenu : MonoBehaviour
{
    public enum MenuStates {CookieMenu, ShopMenu, SettingsMenu};
    public MenuStates currentState;

    public GameObject cookieMenu;
    public GameObject shopMenu;
    public GameObject settingsMenu;

    void Awake()
    {
        Debug.Log("Awake");
        currentState = MenuStates.CookieMenu;
    }

    void Update()
    {
        switch (currentState)
        {
            case MenuStates.CookieMenu:
                cookieMenu.SetActive(true);
                shopMenu.SetActive(false);
                settingsMenu.SetActive(false);
                break;
            case MenuStates.ShopMenu:
                cookieMenu.SetActive(false);
                shopMenu.SetActive(true);
                settingsMenu.SetActive(false);
                break;
            case MenuStates.SettingsMenu:
                cookieMenu.SetActive(false);
                shopMenu.SetActive(false);
                settingsMenu.SetActive(true);
                break;
        }
    }

    public void EnterShop()
    {
        // Debug.Log("Change State ");
        currentState = MenuStates.ShopMenu;
        SoundManager.PlaySound("changeMenu");
    }

    public void EnterMenu()
    {
        // Debug.Log("Change State ");
        currentState = MenuStates.CookieMenu;
        SoundManager.PlaySound("changeMenu");
    }

    public void EnterSettings()
    {
        // Debug.Log("Change State");
        currentState = MenuStates.SettingsMenu;
        SoundManager.PlaySound("changeMenu");
    }

}
