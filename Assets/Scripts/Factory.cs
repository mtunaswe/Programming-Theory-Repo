using UnityEngine;

public class Factory : Building
{
    void Awake()
    {
        constructionCost = 500;
        incomeGeneration = 60; // Highest money
        populationImpact = 0;
        ecoScoreImpact = -15.0f; // Highest negative eco
    }
    
    public override void ApplyBuildingEffect()
    {
        DataManager.Instance.AddPopulation(populationImpact);
        DataManager.Instance.AddEcoScore(ecoScoreImpact);
        DataManager.Instance.RegisterBuilding(this);
        Debug.Log($"[Construction] {BuildingName} built! Industrial production started. Eco-Score decreased.");
    }
}