using UnityEngine;
using UnityEngine.SceneManagement; // Required to change scenes
using TMPro; 

public class MenuHandler : MonoBehaviour
{
    [Header("UI References")]
    public TMP_InputField nameInputField; 

    [Header("Scene Settings")]
    public string sceneToLoad = "Main"; 

    void Start()
    {
        // We listen for the specific 'onSubmit' event.
        // This fires automatically when the user hits 'Enter' on keyboard
        // or 'Done' on mobile while the field is focused.
        nameInputField.onSubmit.AddListener(OnNameEntered);
    }

    // The listener passes the text automatically, even if we don't use it yet
    void OnNameEntered(string playerName)
    {
        // Simple check to ensure they didn't leave it blank
        if (string.IsNullOrEmpty(playerName)) return;

        // Save the name to use in the Main Scene later
        PlayerPrefs.SetString("PlayerName", playerName);
        
        LoadGameScene();
    }

    public void LoadGameScene()
    {
        SceneManager.LoadScene(sceneToLoad);
    }
}