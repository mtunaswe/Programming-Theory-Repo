using UnityEngine;
using UnityEngine.EventSystems;

public class PlotGlow : MonoBehaviour
{
    [Header("References")]
    public MeshRenderer[] greenFieldRenderers;
    private ConstructionPlot plotScript;

    [Header("Glow Settings")]
    public Color glowColor = new Color(0.467f, 0.627f, 0.188f, 1.0f);
    public float pulseSpeed = 4f;
    [Range(0, 1)] public float minIntensity = 0.2f; // Minimum brightness
    [Range(0, 1)] public float maxIntensity = 0.8f; // Maximum brightness

    private Color originalColor;
    private bool isHovering = false;

    void Start()
    {
        plotScript = GetComponent<ConstructionPlot>();
        if (greenFieldRenderers.Length > 0)
            originalColor = greenFieldRenderers[0].material.color;
    }

    void Update()
    {
        if (isHovering && (plotScript == null || !plotScript.IsOccupied))
        {
            // Use Sine to oscillate between 0 and 1
            float glowValue = Mathf.Lerp(minIntensity, maxIntensity, (Mathf.Sin(Time.time * pulseSpeed) + 1.0f) / 2.0f);
            
            Color finalGlow = Color.Lerp(originalColor, glowColor, glowValue);
            ApplyColor(finalGlow);
        }
    }

    private void ApplyColor(Color col)
    {
        foreach (var renderer in greenFieldRenderers)
        {
            if (renderer != null) renderer.material.color = col;
        }
    }

    void OnMouseEnter()
    {
        if (EventSystem.current.IsPointerOverGameObject()) return;
        isHovering = true;
    }

    void OnMouseExit()
    {
        isHovering = false;
        ApplyColor(originalColor); // Reset instantly or Lerp back if preferred
    }
}