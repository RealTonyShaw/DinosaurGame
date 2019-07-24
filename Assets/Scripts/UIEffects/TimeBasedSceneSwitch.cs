using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Switch to another scene at certain time
/// </summary>
public class TimeBasedSceneSwitch : MonoBehaviour
{
    /// <summary>
    /// To which scene
    /// </summary>
    public string sceneName;

    /// <summary>
    /// At which time
    /// </summary>
    public float switchTime;

    // Update is called once per frame
    void Update()
    {
        if (Time.time > switchTime)
        {
            SceneManager.LoadScene(sceneName);
        }
    }
}
