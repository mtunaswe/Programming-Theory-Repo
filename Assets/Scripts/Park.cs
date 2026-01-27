using Unity.Android.Gradle.Manifest;
using UnityEngine;

public class Park : Building
{
    void Awake()
    {
        constructionCost = 200;
        incomeGeneration = 0;
        populationImpact = 0;
        ecoScoreImpact = 20.0f; // Highest positive eco
    }
    public override void ApplyBuildingEffect()
    {
        DataManager.Instance.AddPopulation(populationImpact);
        DataManager.Instance.AddEcoScore(ecoScoreImpact);
        DataManager.Instance.RegisterBuilding(this);
        Debug.Log($"[Construction] {BuildingName} built! Citizens are happier. Eco-Score increased.");
    }
}