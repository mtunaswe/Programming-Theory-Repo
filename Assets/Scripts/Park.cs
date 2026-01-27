using Unity.Android.Gradle.Manifest;
using UnityEngine;

public class Park : Building
{
    public override int constructionCost => 200;
    public override int incomeGeneration => 0;
    public override int populationImpact => 0;
    public override float ecoScoreImpact => 20.0f; // Highest positive eco

    public override void ApplyBuildingEffect()
    {
        DataManager.Instance.AddPopulation(populationImpact);
        DataManager.Instance.AddEcoScore(ecoScoreImpact);
        DataManager.Instance.RegisterBuilding(this);
        Debug.Log($"[Construction] {BuildingName} built! Citizens are happier. Eco-Score increased.");
    }
}