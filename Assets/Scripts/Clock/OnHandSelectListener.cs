using UnityEngine;

public class OnHandSelectListener : MonoBehaviour
{
    private Color standardColor;
    private Color selectedColor = new(0.92f, 0.6f, 0.25f, 1);
    private MeshRenderer meshRenderer;
   
    void Start()
    {
        meshRenderer = GetComponentInChildren<MeshRenderer>();
        standardColor = meshRenderer.material.color;
    }

    public void Select()
    {
        meshRenderer.material.color = selectedColor;
    }

    public void Deselect()
    {
        meshRenderer.material.color = standardColor;
    }
}
