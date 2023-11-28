using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager main;
    private PointSystem pointSystem;

    public Transform startPoint;
    public Transform[] path;
    public int currency;

    private void Awake()
    {
        main = this;
        pointSystem = FindObjectOfType<PointSystem>();
        if (pointSystem == null)
        {
            GameObject pointSystemObject = new GameObject("PointSystem");
            pointSystem = pointSystemObject.AddComponent<PointSystem>();
        }
    }

    private void Start()
    {
        currency = 100;
    }

    public void IncreaseCurrency(int amount)
    {
        currency += amount;
        pointSystem.IncreasePoints(amount);
    }

    public bool SpendCurrency(int amount)
    {
        if (amount <= currency && pointSystem.SpendPoints(amount))
        {
            currency -= amount;
            return true;
        }
        else
        {
            Debug.Log("Not enough currency!");
            return false;
        }
    }
}