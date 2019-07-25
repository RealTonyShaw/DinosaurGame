using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpdateAchievementList : MonoBehaviour
{
    public GameObject collectedStars;
    public GameObject crashedStones;
    public GameObject rotationTimes;

    public void UpdateAchievements()
    {
        collectedStars.GetComponent<Text>().text = GameDB.collectedStars.ToString();
        crashedStones.GetComponent<Text>().text = GameDB.crashedStones.ToString();
        rotationTimes.GetComponent<Text>().text = GameDB.rotationTimes.ToString();
    }
}
