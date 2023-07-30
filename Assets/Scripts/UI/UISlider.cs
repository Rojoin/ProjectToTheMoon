using UnityEngine;
using UnityEngine.UI;

public class UISlider : MonoBehaviour
{
    [SerializeField]  private string sliderKey;
    private Slider slide;
    private void Awake()
    {
        slide = GetComponent<Slider>();
        if (PlayerPrefs.HasKey(sliderKey))
        {
            slide.value = PlayerPrefs.GetFloat(sliderKey);
        }
    }
}