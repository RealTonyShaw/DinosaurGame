using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// For loading different scenes and quiting the game
/// Written for buttons
/// </summary>
public class MenuController : MonoBehaviour
{
    public Slider volumeSlider;

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

    /// <summary>
    /// Enter Level 2
    /// </summary>
    public void EnterLevel2()
    {
        SceneManager.LoadScene("Level 2");
    }

    /// <summary>
    /// Enter Level 3
    /// </summary>
    public void EnterLevel3()
    {
        SceneManager.LoadScene("Level 3");
    }

    /// <summary>
    /// Enter Congratulation Scene
    /// </summary>
    public void GamePass()
    {
        SceneManager.LoadSceneAsync("Congratulations");
    }

    /// <summary>
    /// Go back to Menu
    /// </summary>
    public void EnterMenu()
    {
        SceneManager.LoadScene("Menu");
    }

    /// <summary>
    /// Set volume.
    /// </summary>
    public void SetVolume()
    {
        GameDB.volume = volumeSlider.value;
    }

}
