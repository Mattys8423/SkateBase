using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using static UnityEngine.Rendering.DebugUI;

public class ButtonFonction : MonoBehaviour
{
    public void StartScene(int value)
    {
        StartCoroutine(LauchAfterDelay(value));
    }

    private IEnumerator LauchAfterDelay(int value)
    {
        yield return new WaitForSeconds(.2f);
        SceneManager.LoadScene(value);
    }
}
