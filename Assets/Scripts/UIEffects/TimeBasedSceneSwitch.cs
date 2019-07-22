using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TimeBasedSceneSwitch : MonoBehaviour
{
    public string sceneName;
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
