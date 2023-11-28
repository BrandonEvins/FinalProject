using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointSystem : MonoBehaviour
{
    public static PointSystem instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    private int totalPoints;

    public void IncreasePoints(int amount)
    {
        totalPoints += amount;
    }

    public int GetTotalPoints()
    {
        return totalPoints;
    }
}
