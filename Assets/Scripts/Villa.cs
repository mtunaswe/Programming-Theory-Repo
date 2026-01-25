using UnityEngine;

public class Villa : Building
{
    public override void ApplyBuildingEffect()
    {
        Debug.Log($"[Construction] {BuildingName} built! Luxury residence established. Tax revenue increased.");
    }
}