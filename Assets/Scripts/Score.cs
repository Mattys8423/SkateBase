using TMPro;
using UnityEngine;

public class Score : MonoBehaviour
{
    [SerializeField] private Player script;
    [SerializeField] private GameObject player;
    [SerializeField] private TMP_Text scoretext;
    [SerializeField] private TMP_Text scoretextEnd;
    private float score = 0;

    private void Update()
    {
        if (script.GetPlayer())
        {
            score += 20 * Time.deltaTime;
        }

        if (script.GetObstacle())
        {
            score += 5 * Time.deltaTime;
        }


        Vector3 flatVel = new Vector3(player.GetComponent<Rigidbody>().linearVelocity.x, 0, player.GetComponent<Rigidbody>().linearVelocity.z);
        if (flatVel.magnitude >= 7) 
        {
            score += 5 * Time.deltaTime;
        }

        scoretext.text = score.ToString("F0");
        scoretextEnd.text = score.ToString("F0");
    }

    public void AddScore(int value) { score += value; }
    public void RemoveScore(int value) {score -= value; }
}
