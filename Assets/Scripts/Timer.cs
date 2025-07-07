using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
    [SerializeField] private TMP_Text TimerText;
    [SerializeField] private GameObject EndMenu;
    [SerializeField] private GameObject GameCanva;
    [SerializeField] private float BaseTime = 60;

    void Update()
    {
        BaseTime -= Time.deltaTime;
        TimerText.text = BaseTime.ToString("F0");

        if (BaseTime <= 0)
        {
            GameCanva.SetActive(false);
            EndMenu.SetActive(true);
        }
    }
}
