using UnityEngine;

public class House : Building
{
    public override int constructionCost => 100;
    public override int incomeGeneration => 10;
    public override int populationImpact => 15; // Highest population
    public override float ecoScoreImpact => -5.0f;

    public override void ApplyBuildingEffect()
    {
        DataManager.Instance.AddPopulation(populationImpact);
        DataManager.Instance.AddEcoScore(ecoScoreImpact);
        DataManager.Instance.RegisterBuilding(this);
        Debug.Log($"[Construction] {BuildingName} built! Housing capacity increased.");
    }
}