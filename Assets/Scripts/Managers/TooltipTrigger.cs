using UnityEngine;
using UnityEngine.EventSystems;

public class TooltipTrigger : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [Header("Building Info")]
    public string buildingName;
    [TextArea] public string description;

    [Header("Stats (Enter 0 to hide)")]
    public int cost;
    public int population;
    public int income;
    public int eco;

    public void OnPointerEnter(PointerEventData eventData)
    {
        // 1. Cost
        string costStr = (cost > 0) ? $"<color=#FFD700>Cost: ${cost} <sprite name=\"coin\" tint=1></color>" : "";

        // 2. Population (New Separate Logic)
        string popStr = "";
        if (population != 0)
        {
            // Green for positive, Red for negative (if you have death mechanics)
            string color = (population > 0) ? "#90EE90" : "#FF6347";
            string sign = (population > 0) ? "+" : "";
            popStr = $"<color={color}>Pop: {sign}{population} <sprite name=\"person\" tint=1></color>";
        }

        // 3. Income (New Separate Logic)
        string incomeStr = "";
        if (income != 0)
        {
            string color = (income > 0) ? "#90EE90" : "#FF6347"; // Green for income, Red for maintenance cost
            string sign = (income > 0) ? "+" : "";
            incomeStr = $"<color={color}>Income: {sign}${income} <sprite name=\"cash\" tint=1></color>";
        }
        
        // 4. Eco
        string ecoStr = "";
        if (eco != 0)
        {
            string color = (eco > 0) ? "#90EE90" : "#FF6347"; 
            string sign = (eco > 0) ? "+" : "";
            ecoStr = $"<color={color}>Eco: {sign}{eco} <sprite name=\"leaf\" tint=1></color>";
        }

        // Send to System
        TooltipSystem.Instance.Show(buildingName, costStr, popStr, incomeStr, ecoStr, description);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        TooltipSystem.Instance.Hide();
    }
}