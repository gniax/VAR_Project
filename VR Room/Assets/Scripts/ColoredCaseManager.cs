using UnityEngine;

public class ColoredCaseManager : MonoBehaviour
{
    private Material _localMaterial;

    void Awake()
    {
        // Clone the material
        _localMaterial = new Material(GetComponent<Renderer>().material);
        GetComponent<Renderer>().material = _localMaterial;

        // Ensure emission is enabled so we can see the color more vividly.
        _localMaterial.EnableKeyword("_EMISSION");
    }

    public void ChangeColorToRandom()
    {
        Color newColor = new Color(Random.value, Random.value, Random.value, 1.0f);

        // Apply the new color to both the main color and the emission color
        _localMaterial.color = newColor;
        _localMaterial.SetColor("_EmissionColor", newColor * Mathf.LinearToGammaSpace(2.0f));
    }
}
