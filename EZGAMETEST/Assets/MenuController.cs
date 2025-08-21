using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MenuController : MonoBehaviour
{
    public Slider difficultySlider;
    public TMP_Text difficultyText;
    public Button playButton;

    private void Awake()
    {
        difficultySlider.onValueChanged.AddListener(OnSliderChanged);
        playButton.onClick.AddListener(OnPlayButtonClicked);
    }
    
    private void Start()
    {
        OnSliderChanged(difficultySlider.value);
    }

    private void OnSliderChanged(float value)
    {
        difficultyText.text = $"Difficulty: {value:F1}";
    }

    private void OnPlayButtonClicked()
    {
        GameManager.StartGame(difficultySlider.value);
    }
}
