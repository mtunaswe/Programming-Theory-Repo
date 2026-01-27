using UnityEngine;

public class Villa : Building
{
    // POLYMORPHISM: Override base class properties with Villa-specific values
    public override int constructionCost => 300;
    public override int incomeGeneration => 30;
    public override int populationImpact => 5;
    public override float ecoScoreImpact => -2.0f;

    // POLYMORPHISM: Override base class method with Villa-specific logic
    public override void ApplyBuildingEffect()
    {
        DataManager.Instance.AddPopulation(populationImpact);
        DataManager.Instance.AddEcoScore(ecoScoreImpact);
        DataManager.Instance.RegisterBuilding(this);
        Debug.Log($"[Construction] {BuildingName} built! Luxury residence established. Tax revenue increased.");
    }
}