using UnityEngine;

public class GeneratePowerup : MonoBehaviour
{
    public GameObject powerUp;

    public GameObject canvas;

    public RectTransform canvasDim;

    public GameObject randomPowerUp;

    public int preFabMax = 1;

    public float waitTime = 30f;

    public float destroyTimer = 15f;

    public float widthSpawnOffset = 100f;

    public float heightSpawnOffset = 50f;

    private float failTimer;

    private bool canSpawn = true;

    Main main;

    void Awake()
    {
        main = GameObject.Find("GameManager").GetComponent<Main>();
        // Debug.Log(canvasDim.rect.width + " " + canvasDim.rect.height);

        failTimer = waitTime;
    }

    void Update()
    {
        if(!canSpawn)
            failTimer -= Time.deltaTime;

        if(failTimer <= 0)
        {
            canSpawn = true;
            failTimer = waitTime;
        }
    }

    void FixedUpdate()
    {
        RandomlyGeneratePowerUp();
    }

    public void RandomlyGeneratePowerUp()
    {
        int randomNum = UnityEngine.Random.Range(0, 99);

        // Debug.Log(GameObject.FindGameObjectsWithTag("powerUp").Length);

        if(GameObject.FindGameObjectsWithTag("powerUp").Length >= preFabMax)
        {
            canSpawn = false;
        }

        if(main.isPowerUpActive)
        {
            canSpawn = false;
        }

        if(canSpawn && randomNum < main.luck.power)
        {

            float positionX = Random.Range(-widthSpawnOffset, widthSpawnOffset);
            float positionY = Random.Range(-heightSpawnOffset, heightSpawnOffset);
            // Debug.Log("PosX " + positionX + "\nPosY " + positionY);
            Vector3 position = new Vector3(positionX, positionY, -500);
            randomPowerUp = GameObject.Instantiate(powerUp, position, Quaternion.identity, canvas.transform);
            randomPowerUp.tag = "powerUp";
            randomPowerUp.transform.localPosition = position;
            Destroy(randomPowerUp, destroyTimer);
            canSpawn = false;
        }
        else
        {
            canSpawn = false;
        }

    }

    public void DestroyPreFab()
    {
        Destroy(randomPowerUp);
    }
}
