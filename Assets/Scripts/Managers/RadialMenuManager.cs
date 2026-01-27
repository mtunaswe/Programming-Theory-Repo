using UnityEngine;
public class RadialMenuManager : MonoBehaviour
{
    public static RadialMenuManager Instance;
    private ConstructionPlot activePlot;

    [Header("UI Button GameObjects")]
    public GameObject villaButton;
    public GameObject houseButton;
    public GameObject factoryButton;
    public GameObject parkButton;

    void Awake()
    {
        if (Instance == null) Instance = this;
        gameObject.SetActive(false);
    }

    void Update()
    {
        // Only check for the Escape key if the menu is actually open
        if (gameObject.activeSelf && Input.GetKeyDown(KeyCode.Escape))
        {
            // ABSTRACTION: The user just wants to "Stop", 
            // the code handles the cleanup internally.
            Close();
        }
    }

    public void Open(ConstructionPlot plot)
    {
        activePlot = plot;
        gameObject.SetActive(true);
        transform.position = Input.mousePosition;

        // Reset: Hide all buttons first
        HideAllButtons();

        // ABSTRACTION: Loop through the plot's specific options and turn on buttons
        foreach (GameObject buildingModel in plot.buildableOptions)
        {
            if (buildingModel.CompareTag("Villa")) villaButton.SetActive(true);
            if (buildingModel.CompareTag("House")) houseButton.SetActive(true);
            if (buildingModel.CompareTag("Factory")) factoryButton.SetActive(true);
            if (buildingModel.CompareTag("Park")) parkButton.SetActive(true);
        }
    }

    private void HideAllButtons()
    {
        villaButton.SetActive(false);
        houseButton.SetActive(false);
        factoryButton.SetActive(false);
        parkButton.SetActive(false);
    }   

    // Assign these functions specifically to each button's OnClick event
    public void SelectVilla() => ExecuteSelection("Villa");
    public void SelectHouse() => ExecuteSelection("House");
    public void SelectFactory() => ExecuteSelection("Factory");
    public void SelectPark() => ExecuteSelection("Park");

    private void ExecuteSelection(string tagToFind)
    {
        if (activePlot == null) return;

        // Search the active plot for a building with the matching tag
        for (int i = 0; i < activePlot.buildableOptions.Length; i++)
        {
            if (activePlot.buildableOptions[i].CompareTag(tagToFind))
            {
                activePlot.BuildAtIndex(i);
                Close();
                return;
            }
        }
        Debug.LogWarning($"No building with tag {tagToFind} exists in this plot!");
    }

    public void Close()
    {
        activePlot = null;
        gameObject.SetActive(false);
        Debug.Log("Construction cancelled by player.");
    }
}