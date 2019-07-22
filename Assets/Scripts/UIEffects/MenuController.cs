using UnityEngine;
using UnityEngine.SceneManagement;

/*
 * Small behaviour to handle menu button callbacks.
 */
public class MenuController : MonoBehaviour
{
    /*
     * When the start button is pressed, load the Game scene.
     */
    public void OnStartClicked()
    {
        Debug.Log("Detected");
        SceneManager.LoadScene("Game");
        // Application.LoadLevel("Game");
    }

    /*
     * When the back button is clicked, load the Menu scene.
     */
    public void OnBackClicked()
    {
        SceneManager.LoadScene("Menu");
        //Application.LoadLevel("Menu");
    }
}
