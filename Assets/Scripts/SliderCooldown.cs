using UnityEngine;
using UnityEngine.UI;

public class SliderCooldown : MonoBehaviour
{
    [SerializeField] private Slider Cooldown;

    private void Update()
    {
        Cooldown.value -= Time.deltaTime;
    }

    public void SetSliderToZero()
    {
        Cooldown.value = 0f;
    }

    public void SetSliderToMax()
    {
        Cooldown.value = 2f;
    }
}
