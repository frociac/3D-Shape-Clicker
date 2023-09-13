using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class CookieMesh
{
    public Mesh mesh;

    public Vector3 scale;

    public bool isActivated;
    public bool isBought;
    
    public double cost;

    public string name;

    public TextMeshProUGUI textBox;

    public Button button;

    public CookieMesh(string name, double cost, Mesh mesh, Vector3 scale, Button button, TextMeshProUGUI textBox)
    {
        this.name = name;
        this.cost = cost;
        this.mesh = mesh;
        this.scale = scale;
        this.textBox = textBox;
        this.button = button;

        isActivated = false;
        isBought = false;
    }

    public void SetActivated(bool state)
    {
        isActivated = state;
    }

    public void BuyMesh()
    {
        isBought = true;
    }

    public void UpdateTextBox(CookieMesh mesh)
    {
        textBox.text = mesh.ToString();
    }
    public override string ToString ()
    {
        string info = "";

        if(!isBought)
        {
            info = name + "\nCost: " + cost;
        }
        else if(isBought && !isActivated)
        {
            info = name + "\nPress to Activate";
        }
        else if(isBought && isActivated)
        {
            info = name + "\nActivated";
        }

        
        if(name == "Cube")
            info += "\n No Benefits";
        else if(name == "Sphere")
            info += "\n x2 Automation Power";
        else if(name == "Octahedron")
            info += "\n x3 Click Power";
        else if(name == "Icosahedron")
            info += "\n x5 Critical Hit Power";

        return info;
    }

}
