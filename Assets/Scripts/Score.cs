using TMPro;
using UnityEngine;

public class Score : MonoBehaviour
{
    [SerializeField] private Player script;
    [SerializeField] private TMP_Text scoretext;
    [SerializeField] private TMP_Text scoretextEnd;
    private float score = 0;

    private void Update()
    {
        if (script.GetPlayer())
        {
            score += 5 * Time.deltaTime;
        }

        scoretext.text = score.ToString("F0");
        scoretextEnd.text = score.ToString("F0");
    }

    public void AddScore(int value) { score += value; }
}
