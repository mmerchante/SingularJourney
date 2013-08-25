using UnityEngine;
using System.Collections;

public class SelectableObjectEffects : MonoBehaviour {

    public static Color SELECTION_COLOR = new Color(.5f, .7f, 1f, .5f);

    private float hoverIntensity = 0f;
    private float hoverVelocity = 0f;

    private bool hover = false;

    private Material[] objectMaterials = new Material[2];

    private bool click = false;
    
    void Start () 
    {
        Shader selectionShader = Shader.Find("vrjam/BRDF (Selectable)");

        Material selectionMaterial = new Material(selectionShader);
        selectionMaterial.SetColor("_Color", SELECTION_COLOR);
        

        Material originalMaterial = this.renderer.material; // duplicate material too

        if(originalMaterial.HasProperty("_BRDFTex"))
            selectionMaterial.SetTexture("_BRDFTex", originalMaterial.GetTexture("_BRDFTex"));

        this.objectMaterials[0] = selectionMaterial;
        this.objectMaterials[1] = originalMaterial;

        this.renderer.materials = objectMaterials;
	}
	
	void Update () 
    {
        hoverIntensity = Mathf.SmoothDamp(hoverIntensity, hover ? 1.5f : .25f, ref hoverVelocity, .25f);

        Material selMat = objectMaterials[0];
        selMat.SetFloat("_Displacement", hoverIntensity * .5f);
	}

    public void OnSelect()
    {
        this.renderer.materials = new Material[1] {objectMaterials[0]} ;
    }

    public void OnUnselect()
    {
        this.renderer.materials = objectMaterials;
    }

    public void OnHover()
    {
        hover = true;
    }

    public void OnUnhover()
    {
        hover = false;
    }

    public void OnMouseEnter()
    {
        OnHover();
    }

    public void OnMouseExit()
    {
        OnUnhover();
    }

    public void OnMouseDown()
    {
        click = !click;

        if (click)
            OnSelect();
        else
            OnUnselect();
    }
}
