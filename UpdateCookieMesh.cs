using System.Collections.Generic;
using UnityEngine.EventSystems;
using System.Collections;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class UpdateCookieMesh : MonoBehaviour
{
    public enum CookieStates {Cube, Sphere, Octahedron, Icosahedron};

    public CookieStates currentState;

    public Mesh cube;
    public Vector3 cubeScale;
    public TextMeshProUGUI cubeTextBox;
    public Button cubeButton;


    public Mesh sphere;
    public Vector3 sphereScale;
    public TextMeshProUGUI sphereTextBox;
    public Button sphereButton;

    public Mesh octahedron;
    public Vector3 octahedronScale;
    public TextMeshProUGUI octahedronTextBox;
    public Button octahedronButton;

    public Mesh icosahedron;
    public Vector3 icosahedronScale;
    public TextMeshProUGUI icosahedronTextBox;
    public Button icosahedronButton;

    public CookieMesh CubeMesh;
    public CookieMesh SphereMesh;
    public CookieMesh OctahedronMesh;
    public CookieMesh IcosahedronMesh;

    public GameObject cookie;

    public List<CookieMesh> meshes;

    MeshFilter meshFilter;
    MeshCollider meshCollider;

    Main main;
    Animations animations;

    public string currentStateString;

    void Awake()
    {
        Debug.Log("Mesh Awake");
        currentState = CookieStates.Cube;
        meshes = new List<CookieMesh>();

        meshFilter = cookie.GetComponent<MeshFilter>();
        meshCollider = cookie.GetComponent<MeshCollider>();

        meshFilter.sharedMesh = sphere;
        meshCollider.sharedMesh = sphere;

        main = GameObject.Find("GameManager").GetComponent<Main>();
        animations = GameObject.Find("GameManager").GetComponent<Animations>();
    }

    void Start()
    {
        CubeMesh = new CookieMesh("Cube", 0, cube, cubeScale, cubeButton, cubeTextBox);
        CubeMesh.BuyMesh();
        CubeMesh.SetActivated(true);

        SphereMesh = new CookieMesh("Sphere", 50000, sphere, sphereScale, sphereButton, sphereTextBox);
        OctahedronMesh = new CookieMesh("Octahedron", 100000, octahedron, octahedronScale, octahedronButton, octahedronTextBox);
        IcosahedronMesh = new CookieMesh("Icosahedron", 500000, icosahedron, icosahedronScale, icosahedronButton, icosahedronTextBox);

        meshes.Add(CubeMesh);
        meshes.Add(SphereMesh);
        meshes.Add(OctahedronMesh);
        meshes.Add(IcosahedronMesh);
    }

    void Update()
    {
        switch(currentState)
        {
            case CookieStates.Cube:
                CubeMesh.SetActivated(true);
                SphereMesh.SetActivated(false);
                OctahedronMesh.SetActivated(false);
                IcosahedronMesh.SetActivated(false);
                break;
            case CookieStates.Sphere:
                CubeMesh.SetActivated(false);
                SphereMesh.SetActivated(true);
                OctahedronMesh.SetActivated(false);
                IcosahedronMesh.SetActivated(false);
                break;
            case CookieStates.Octahedron:
                CubeMesh.SetActivated(false);
                SphereMesh.SetActivated(false);
                OctahedronMesh.SetActivated(true);
                IcosahedronMesh.SetActivated(false);
                break;
            case CookieStates.Icosahedron:
                CubeMesh.SetActivated(false);
                SphereMesh.SetActivated(false);
                OctahedronMesh.SetActivated(false);
                IcosahedronMesh.SetActivated(true);
                break;
            default:
                Debug.Log("bruh moment");
                break;
        }

        currentStateString = currentState.ToString();

        ChangeMesh();
        UpdateTextBox();
    }


    void ChangeMesh()
    {
        // Debug.Log("reched changedMesh");
        foreach(CookieMesh mesh in meshes)
        {
            if(mesh.isActivated)
            {
                SetUpMesh(mesh.mesh);
            }
        }
    }

    void SetUpMesh(Mesh mesh)
    {
        meshFilter.sharedMesh = mesh;
        meshCollider.sharedMesh = mesh;
    }

    public void OnBuyCookie()
    {
        Button clickedButton = EventSystem.current.currentSelectedGameObject.GetComponent<Button>();
        CookieMesh cookieMesh = GetCookieMesh(clickedButton);
        if(cookieMesh != null)
            CookieMeshAction(cookieMesh);
    }

    public void LoadCookieMesh(string name)
    {
        switch(name)
        {
            case "Cube":
                currentState = CookieStates.Cube;
                cookie.transform.localScale = cubeScale;
                animations.currentScale = cubeScale;
                break;
            case "Octahedron":
                currentState = CookieStates.Octahedron;
                cookie.transform.localScale = octahedronScale;
                animations.currentScale = octahedronScale;
                break;
            case "Sphere":
                currentState = CookieStates.Sphere;
                cookie.transform.localScale = sphereScale;
                animations.currentScale = sphereScale;
                break;
            case "Icosahedron":
                currentState = CookieStates.Icosahedron;
                cookie.transform.localScale = icosahedronScale;
                animations.currentScale = icosahedronScale;
                break;
        }
    }

    CookieMesh GetCookieMesh(Button clickedButton)
    {
        CookieMesh currentCookieMesh = null;
        foreach (CookieMesh mesh in meshes)
        {
            if(mesh.button == clickedButton)
            {
                currentCookieMesh = mesh;
                break;
            }
        }
        return currentCookieMesh;
    }

    void CookieMeshAction(CookieMesh cookieMesh)
    {
        if(cookieMesh.isBought)
        {
            LoadCookieMesh(cookieMesh.name);
        }
        else
        {
            CookieMeshBuy(cookieMesh);
        }
    }

    void CookieMeshBuy(CookieMesh cookieMesh)
    {
        animations.BounceShape();
        if(main.coinBalance >= cookieMesh.cost)
        {
            SoundManager.PlaySound("upgrade");
            main.coinBalance -= cookieMesh.cost;
            cookieMesh.BuyMesh();
            // Debug.Log(upgrade);
        }
        else
        {
            SoundManager.PlaySound("notEnoughCoins");
            Debug.Log("not enough coins");
        }
    }

    public void OnCube()
    {
        if(CubeMesh.isBought)
        {
            currentState = CookieStates.Cube;
            cookie.transform.localScale = cubeScale;

            SoundManager.PlaySound("upgrade");

            animations.SetScale(cubeScale);
        }
    }

    public void OnSphere()
    {
        if(SphereMesh.isBought)
        {
            currentState = CookieStates.Sphere;
            cookie.transform.localScale = sphereScale;
           
            SoundManager.PlaySound("upgrade");
            
            animations.SetScale(sphereScale);
        }
    }

    public void OnOctahedron()
    {
        if(OctahedronMesh.isBought)
        {
            currentState = CookieStates.Octahedron;
            cookie.transform.localScale = octahedronScale;
            
            SoundManager.PlaySound("upgrade");
            
            animations.SetScale(octahedronScale);
        }
    }
    
    public void OnIcosahedron()
    {
        if(IcosahedronMesh.isBought)
        {
            currentState = CookieStates.Icosahedron;
            cookie.transform.localScale = icosahedronScale;
            
            SoundManager.PlaySound("upgrade");
            
            animations.SetScale(icosahedronScale);
        }
    }

    void UpdateTextBox()
    {
        foreach(CookieMesh mesh in meshes)
        {
            mesh.UpdateTextBox(mesh); 
        }
    }
}
