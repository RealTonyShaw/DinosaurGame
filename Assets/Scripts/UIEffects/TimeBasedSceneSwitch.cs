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
    float startTime;
    private void Start()
    {
        startTime = Time.time;
    }
    // Update is called once per frame
    void Update()
    {
        if (Time.time - startTime > switchTime)
        {
            SceneManager.LoadSceneAsync(sceneName);
        }
    }
}
