using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// For loading different scenes and quiting the game
/// Written for buttons
/// </summary>
public class MenuController : MonoBehaviour
{
    /// <summary>
    /// When the button is pressed, load the Game scene.
    /// </summary>
    public void OnStartClicked()
    {
        Debug.Log("Detected");
        SceneManager.LoadScene("Game");
        // Application.LoadLevel("Game");
    }

    /// <summary>
    /// When the button is clicked, load the Menu scene.
    /// </summary>
    public void OnBackClicked()
    {
        SceneManager.LoadScene("Menu");
    }

    /// <summary>
    /// When the button is pressed, quit the game.
    /// </summary>
    public void Quit()
    {
        Application.Quit();
    }

    public void EnterLevel2()
    {
        SceneManager.LoadScene("Level 2");
    }

    public void EnterLevel3()
    {
        SceneManager.LoadScene("Level 3");
    }

    public void GamePass()
    {
        SceneManager.LoadScene("Congratulations");
    }

    public void EnterMenu()
    {
        SceneManager.LoadScene("Menu");
    }

}
