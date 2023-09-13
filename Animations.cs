using UnityEngine.EventSystems;
using UnityEngine;
using System;
using TMPro;

public class Animations : MonoBehaviour
{

    public Transform cookie;
    public ParticleSystem cookieParticles;

    public Transform cube;
    public Transform sphere;
    public Transform octahedron;
    public Transform icosahedron;

    public GameObject canvas;

    public Camera cam;

    public TextMeshProUGUI popUp;

    private Transform bouncedShape;
    
    private TextMeshProUGUI textClone;

    private Rigidbody2D rb;

    [Range(0f, 1f)] public float rotationSpeed = 0.5f;

    [Range(0, 1f)] public float bounceScale = 0.9f;

    [Range(0f, 1f)] public float bounceTimer = 0.1f;

    [Range(0f, 5f)] public float fadeTimer = 3f;

    [Range(0f, 5f)] public float preFabTimer = 5f;

    [Range(0f, 30f)] public float yForce = 200f;

    public float createTextTimer = 5f;

    public float createActivatedTextTimer = 3f;

    public float yOffset = 10f;

    public float setGravityScale = 2f;

    private float fieldTimer;

    private float fieldShapeTimer;

    private float fieldFadeTimer;


    bool isBounced = false;

    bool isShapeBounced = false;

    bool fadingText = false;

    public Vector3 currentScale;
    private Vector3 currentShapeScale;
    


    void Awake()
    {
        fieldTimer = bounceTimer;
        fieldShapeTimer = bounceTimer;
        fieldFadeTimer = fadeTimer;

        currentScale = cookie.transform.localScale;
    }

    public void ClickCookie(bool isCriticalHit, double totalClickPower, double meshCritScale)
    {
        BounceCookie();
        SpawnParticles();
        SpawnNumberCounter(isCriticalHit, totalClickPower, meshCritScale);
    }

    public void SpawnParticles()
    {
        Instantiate(cookieParticles, canvas.transform);
    }

    public void SetScale(Vector3 scale)
    {
        currentScale = scale;
    }

    void SpawnNumberCounter(bool isCriticalHit, double totalClickPower, double meshCritScale)
    {
        Vector2 force = new Vector2(0, yForce);
        Vector3 spawnPosition = cam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, -600));
        spawnPosition.x *= -1;
        spawnPosition.y *= -1;
        spawnPosition.y += yOffset;
        // Debug.Log(spawnPosition);
        TextMeshProUGUI clone = Instantiate(popUp, spawnPosition, Quaternion.identity, canvas.transform);
        clone.transform.localPosition = spawnPosition;
        rb = clone.GetComponent<Rigidbody2D>();
        clone.GetComponent<Rigidbody2D>().gravityScale = setGravityScale;
        if(isCriticalHit)
        {
            clone.color = new Color32(255, 255, 0, 255);
            totalClickPower *= 10 * meshCritScale;
        }
        clone.text = totalClickPower.ToString();
        rb.AddForce(force, ForceMode2D.Impulse);
        Destroy(clone.gameObject, preFabTimer);
    }


    void BounceCookie()
    {
        cookie.transform.localScale = currentScale * bounceScale;
        isBounced = true;
    }

    void RotateCookie()
    {
        cookie.transform.Rotate(rotationSpeed, rotationSpeed, rotationSpeed, Space.Self);  
        cube.transform.Rotate(rotationSpeed, rotationSpeed, rotationSpeed, Space.Self);  
        sphere.transform.Rotate(rotationSpeed, rotationSpeed, rotationSpeed, Space.Self);  
        octahedron.transform.Rotate(rotationSpeed, rotationSpeed, rotationSpeed, Space.Self);
        icosahedron.transform.Rotate(rotationSpeed, rotationSpeed, rotationSpeed, Space.Self);  
    }

    public void BounceShape()
    {
        bouncedShape = EventSystem.current.currentSelectedGameObject.GetComponent<Transform>();
        if(!isShapeBounced)
            currentShapeScale = bouncedShape.transform.localScale;
        bouncedShape.transform.localScale = currentShapeScale * bounceScale;
        isShapeBounced = true;
        fieldShapeTimer = bounceTimer;
    }

    bool UpdateBounce(Transform obj, float timer, Vector3 scale)
    {
        bool shouldContinue = true;
        if(timer <= 0)
        {
            obj.transform.localScale = scale;
            shouldContinue = false;
        }
        return shouldContinue;
    }

    public void CreateText(TextMeshProUGUI textBox)
    {
        TextMeshProUGUI createTextClone = Instantiate(textBox, canvas.transform);
        Destroy(createTextClone.gameObject, createTextTimer);
    }

    public void CreateTextPowerUp(TextMeshProUGUI textBox, string text, float powerUpTimer)
    {
        textClone = Instantiate(textBox, canvas.transform);
        textClone.color = new Color32(255, 251, 0, 255);
        textClone.text = text + " Activated ";
        if(text != "Instant Coins")
            textClone.text += "For "  + Math.Floor(powerUpTimer) + " Seconds";
        fadingText = true;
    }
    //clone.color = new Color32(255, 255, 0, 255);

    void Update()
    {        
        if(isBounced)
        {
            fieldTimer -= Time.deltaTime;
            if(!UpdateBounce(cookie, fieldTimer, currentScale))
            {
                fieldTimer = bounceTimer;
                isBounced = false;
            }
        }

        if(isShapeBounced)
        {
            fieldShapeTimer -= Time.deltaTime;
            if(!UpdateBounce(bouncedShape, fieldShapeTimer, currentShapeScale))
            {
                fieldShapeTimer = bounceTimer;
                isShapeBounced = false;
            }
        }

        if(fadingText)
        {
            fieldFadeTimer -= Time.deltaTime;
            if(fieldFadeTimer <= fadeTimer - 0.4 * 5)
            {
                textClone.color = new Color32(255, 251, 0, 5);
            }
            else if(fieldFadeTimer <= fadeTimer - 0.4 * 4)
            {
                textClone.color = new Color32(255, 251, 0, 55);
            }
            else if(fieldFadeTimer <= fadeTimer - 0.4 * 3)
            {
                textClone.color = new Color32(255, 251, 0, 105);
            }
            else if(fieldFadeTimer <= fadeTimer - 0.4 * 2)
            {
                textClone.color = new Color32(255, 251, 0, 155);
            }
            else if(fieldFadeTimer <= fadeTimer - 0.4)
            {
                textClone.color = new Color32(255, 251, 0, 205);
            }

            if(fieldFadeTimer <= 0)
            {
                fieldFadeTimer = fadeTimer;
                Destroy(textClone.gameObject);
                fadingText = false;
            }
        }

        RotateCookie();
    }


}

