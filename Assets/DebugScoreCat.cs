using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DebugScoreCat : MonoBehaviour
{

    public Text scoreText;
    public Text multiplierText;
    public int score;
    public int consecutiveCats = 0;

    private void OnTriggerEnter(Collider other)
    {
        CatInstance cat = other.gameObject.GetComponentInParent<CatInstance>();
        AddPointsFromCat(cat);
    }

    public void AddPointsFromCat(CatInstance cat)
    {
        if (cat.IsCatComplete())
        {
            score += cat.CatPointValue;
            consecutiveCats++;
            scoreText.text = score.ToString();
            multiplierText.text = consecutiveCats.ToString();
        }
        else
        {
            consecutiveCats = 0;
            multiplierText.text = consecutiveCats.ToString();
        }

    }

}
