using UnityEngine;

public class Villa : Building
{
    void Awake()
    {
        constructionCost = 300;
        incomeGeneration = 30;
        populationImpact = 5;
        ecoScoreImpact = -2.0f;
    }
    public override void ApplyBuildingEffect()
    {
        DataManager.Instance.RegisterBuilding(this);
        Debug.Log($"[Construction] {BuildingName} built! Luxury residence established. Tax revenue increased.");
    }
}