using UnityEngine;

public class House : Building
{
    // POLYMORPHISM: Override base class properties with House-specific values
    public override int constructionCost => 100;
    public override int incomeGeneration => 10;
    public override int populationImpact => 15; // Highest population
    public override float ecoScoreImpact => -5.0f;

    // POLYMORPHISM: Override base class method with House-specific logic
    public override void ApplyBuildingEffect()
    {
        DataManager.Instance.AddPopulation(populationImpact);
        DataManager.Instance.AddEcoScore(ecoScoreImpact);
        DataManager.Instance.RegisterBuilding(this);
        Debug.Log($"[Construction] {BuildingName} built! Housing capacity increased.");
    }
}