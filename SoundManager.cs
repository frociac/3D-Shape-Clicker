using UnityEngine.UI;
using UnityEngine;

public class SoundManager : MonoBehaviour
{

    public static AudioClip upgrade;
    public static AudioClip notEnoughCoins;
    public static AudioClip cookieClick;
    public static AudioClip criticalHit;
    public static AudioClip changeMenu;
    public static AudioClip powerUp;
    public static AudioClip saveLoad;
	
    public Slider slider;

    static AudioSource audioSrc;

    private float sliderValue = 1f;

    void Start()
    {
        upgrade = Resources.Load<AudioClip>("upgrade");
        notEnoughCoins = Resources.Load<AudioClip>("notEnoughCoins");
        cookieClick = Resources.Load<AudioClip>("cookieClick");
        criticalHit = Resources.Load<AudioClip>("criticalHit");
        changeMenu = Resources.Load<AudioClip>("changeMenu");
        powerUp = Resources.Load<AudioClip>("powerUp");
        saveLoad = Resources.Load<AudioClip>("saveLoad");

        audioSrc = GetComponent<AudioSource>();
    }

    public void OnSliderValueChange()
    {
        sliderValue = slider.value;
    }

    void Update()
    {
        audioSrc.volume = sliderValue;
    }

    public static void PlaySound(string clip)
    {
        switch(clip)
        {
            case "upgrade":
                audioSrc.PlayOneShot(upgrade);
                break;
            case "notEnoughCoins":
                audioSrc.PlayOneShot(notEnoughCoins);
                break;
            case "cookieClick":
                audioSrc.PlayOneShot(cookieClick);
                break;
            case "criticalHit":
                audioSrc.PlayOneShot(criticalHit);
                break;
            case "changeMenu":
                audioSrc.PlayOneShot(changeMenu);
                break;
            case "powerUp":
                audioSrc.PlayOneShot(powerUp);
                break;
            case "saveLoad":
                audioSrc.PlayOneShot(saveLoad);
                break;
            default:
                Debug.Log("bruh");
                break;
        }
    }
}
