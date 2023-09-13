using UnityEngine;

public class ChangePanel : MonoBehaviour
{
    public enum PanelStates {AutoPanel, AbilitiesPanel, CookiePanel};
    public PanelStates currentState;

    public GameObject mainPanel;

    public GameObject AutoPanel;
    public GameObject AbilitiesPanel;
    public GameObject CookiePanel;

    void Awake()
    {
        Debug.Log("Panel Awake");
        currentState = PanelStates.AutoPanel;
        mainPanel.SetActive(true);
    }

    void Update()
    {
        switch (currentState)
        {
            case PanelStates.AutoPanel:
                AutoPanel.SetActive(true);
                AbilitiesPanel.SetActive(false);
                CookiePanel.SetActive(false);
                break;
            case PanelStates.AbilitiesPanel:
                AbilitiesPanel.SetActive(true);
                AutoPanel.SetActive(false);
                CookiePanel.SetActive(false);
                break;
            case PanelStates.CookiePanel:
                CookiePanel.SetActive(true);
                AbilitiesPanel.SetActive(false);
                AutoPanel.SetActive(false);
                break;
        }
    }

    public void EnterAutoPanel()
    {
        // Debug.Log("Change State ");
        currentState = PanelStates.AutoPanel;
        SoundManager.PlaySound("changeMenu");
    }

    public void EnterAbilitiesPanel()
    {
        // Debug.Log("Change State ");
        currentState = PanelStates.AbilitiesPanel;
        SoundManager.PlaySound("changeMenu");
    }

    public void EnterCookiePanel()
    {
        // Debug.Log("Change State ");
        currentState = PanelStates.CookiePanel;
        SoundManager.PlaySound("changeMenu");
    }
}
