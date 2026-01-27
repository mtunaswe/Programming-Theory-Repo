using UnityEngine;
using TMPro;

public class PulseText : MonoBehaviour
{
    private TextMeshProUGUI txt;

    void Start() { txt = GetComponent<TextMeshProUGUI>(); }

    void Update()
    {
        // Use 'unscaledTime' so it keeps moving even when the game is paused!
        float scale = 1.0f + Mathf.Sin(Time.unscaledTime * 5f) * 0.1f;
        transform.localScale = new Vector3(scale, scale, 1f);
    }
}