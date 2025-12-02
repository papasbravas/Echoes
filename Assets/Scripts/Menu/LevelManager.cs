using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    void Update()
    {
        var kb = Keyboard.current;

        if (kb == null) return;

        if (kb.digit1Key.wasPressedThisFrame)
        {
            SceneManager.LoadScene(0);
        }
        if (kb.digit2Key.wasPressedThisFrame)
        {
            SceneManager.LoadScene(3);
        }
        if (kb.digit3Key.wasPressedThisFrame)
        {
            SceneManager.LoadScene(4);
        }
    }
}
