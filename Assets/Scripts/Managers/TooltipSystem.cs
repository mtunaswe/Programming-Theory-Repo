using UnityEngine;
using TMPro;

public class TooltipSystem : MonoBehaviour
{
    public static TooltipSystem Instance;

    [Header("Main Components")]
    public GameObject tooltipPanel;
    // We removed the public tooltipRect variable

    [Header("Text Fields")]
    public TextMeshProUGUI headerText;
    public TextMeshProUGUI costText;
    public TextMeshProUGUI populationText;
    public TextMeshProUGUI incomeText;
    public TextMeshProUGUI ecoText;
    public TextMeshProUGUI descriptionText;

    // We store the RectTransform privately now
    private RectTransform panelRect;

    private void Awake()
    {
        Instance = this;
        
        // AUTOMATICALLY grab the RectTransform from the panel you assigned
        panelRect = tooltipPanel.GetComponent<RectTransform>();
        
        tooltipPanel.SetActive(false);
    }

    void Update()
    {
        if (tooltipPanel.activeSelf)
        {
            Vector2 mousePos = Input.mousePosition;
            
            // PIVOT LOGIC: Flipping the tooltip so it stays on screen
            // If mouse is on the right, pivot switches to the right (growing left)
            // If mouse is on the left, pivot switches to the left (growing right)
            float pivotX = (mousePos.x / Screen.width);
            float pivotY = (mousePos.y / Screen.height);
            
            panelRect.pivot = new Vector2(pivotX, pivotY);

            // Move the panel to the mouse
            tooltipPanel.transform.position = mousePos;
        }
    }

    public void Show(string header, string cost, string pop, string income, string eco, string desc)
    {
        headerText.text = header;
        
        // Auto-Collapse Logic
        SetText(costText, cost);
        SetText(populationText, pop);
        SetText(incomeText, income);
        SetText(ecoText, eco);
        SetText(descriptionText, desc);

        tooltipPanel.SetActive(true);
    }

    // Helper to hide empty text lines
    void SetText(TextMeshProUGUI textObj, string content)
    {
        if (string.IsNullOrEmpty(content))
        {
            textObj.gameObject.SetActive(false);
        }
        else
        {
            textObj.gameObject.SetActive(true);
            textObj.text = content;
        }
    }

    public void Hide()
    {
        tooltipPanel.SetActive(false);
    }
}