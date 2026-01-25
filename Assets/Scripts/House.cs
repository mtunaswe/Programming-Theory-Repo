using UnityEngine;

public class House : Building
{
    void Awake()
    {
        constructionCost = 100;
        incomeGeneration = 10;
        populationImpact = 15; // Highest population
        ecoScoreImpact = -5.0f;
    }
    public override void ApplyBuildingEffect()
    {
        DataManager.Instance.RegisterBuilding(this);
        Debug.Log($"[Construction] {BuildingName} built! Housing capacity increased.");
    }
}