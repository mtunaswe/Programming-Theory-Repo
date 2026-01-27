using UnityEngine;

public class Factory : Building
{
    public override int constructionCost => 500;
    public override int incomeGeneration => 60; // Highest money
    public override int populationImpact => 0;
    public override float ecoScoreImpact => -15.0f; // Highest negative eco

    public override void ApplyBuildingEffect()
    {
        DataManager.Instance.AddPopulation(populationImpact);
        DataManager.Instance.AddEcoScore(ecoScoreImpact);
        DataManager.Instance.RegisterBuilding(this);
        Debug.Log($"[Construction] {BuildingName} built! Industrial production started. Eco-Score decreased.");
    }
}