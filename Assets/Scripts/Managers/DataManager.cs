using UnityEngine;
using System.Collections.Generic;
using TMPro;

public class DataManager : MonoBehaviour
{
    public static DataManager Instance;

    [Header("Current Stats")]
    [SerializeField] private int money = 1000;
    [SerializeField] private int population = 0;
    [SerializeField] private float ecoScore = 50f;

    [Header("Target Goals")]
    public int targetPopulation = 100;
    public float targetEcoScore = 80f;

    [Header("UI Panels")]
    public GameObject winPanel;       
    public GameObject gameOverPanel;  
    public TextMeshProUGUI GoldText;
    public TextMeshProUGUI PopulationText;
    public TextMeshProUGUI EcoScoreText;
    public TextMeshProUGUI PlayerNameText;

    private List<Building> activeBuildings = new List<Building>();
    private int totalPlots;
    private int occupiedPlots = 0;

    void Awake()
    {
        Instance = this;

        // MODERN UNITY: Using FindObjectsByType for better performance
        // This ensures we know exactly how many plots are in the city
        ConstructionPlot[] allPlots = Object.FindObjectsByType<ConstructionPlot>(FindObjectsSortMode.None);
        totalPlots = allPlots.Length;
        
        if(winPanel) winPanel.SetActive(false);
        if(gameOverPanel) gameOverPanel.SetActive(false);

        UpdateUI(); // INITIAL UI UPDATE to see the starting stats

        string playerName = PlayerPrefs.GetString("PlayerName", "Architect");
        Debug.Log($"Welcome, {playerName}!");
        if (PlayerNameText) PlayerNameText.text = $"{playerName}'s City";

        InvokeRepeating("UpdateCityCycle", 5.0f, 5.0f);
    }

    // ABSTRACTION: Simplified method to register a new building
    public void RegisterBuilding(Building newBuilding)
    {
        activeBuildings.Add(newBuilding);
        occupiedPlots++; // TRACKING: Increments every time a building is successfully placed
                
        UpdateUI();
        CheckGameState(); 
    }

    // ONE-TIME IMPACTS: Apply immediately upon construction
    // ABSTRACTION: Simplified methods to modify core stats
    public void AddPopulation(int amount)
    {
        population += amount;
    }

    // ABSTRACTION: Simplified methods to modify core stats
    public void AddEcoScore(float amount)
    {
        ecoScore += amount;
    }
    void UpdateCityCycle()
    {
        int totalNewMoney = 0;
        foreach (Building b in activeBuildings)
        {
            totalNewMoney += b.incomeGeneration;
        }
        money += totalNewMoney;
        UpdateUI();
    }

    void CheckGameState()
    {
        // ABSTRACTION: Win Condition Logic
        if (population >= targetPopulation && ecoScore >= targetEcoScore)
        {
            TriggerEndState(winPanel);
            return;
        }

        if (occupiedPlots >= totalPlots)
        {
            TriggerEndState(gameOverPanel);
        }
    }

    private void TriggerEndState(GameObject panel)
    {
        if (panel != null)
        {
            panel.SetActive(true);
            Time.timeScale = 0; // ABSTRACTION: Pauses the entire game simulation
        }
    }

    public bool CanAfford(int cost) => money >= cost;
    public void SpendMoney(int amount) { money -= amount; UpdateUI(); }

    private void UpdateUI()
    {
        if (GoldText) GoldText.text = $"{money}";
        if (PopulationText) PopulationText.text = $"{population}/{targetPopulation}";
        if (EcoScoreText) EcoScoreText.text = $"{ecoScore}/{targetEcoScore}";
    }
}