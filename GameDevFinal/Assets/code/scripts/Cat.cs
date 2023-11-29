using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;

public class Cat : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform catRotationPoint;
    [SerializeField] private LayerMask enemyMask;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform firingPoint;
    [SerializeField] private GameObject upgradeUI;
    [SerializeField] private Button upgradeButton;

    [Header("Attributes")]
    [SerializeField] private float targetingRange = 5f;
    [SerializeField] private float rotationSpeed=10f;
    [SerializeField] private float bps=1f;
    [SerializeField] private int baseUpgradeCost = 100;

    private float bpsBase;
    private float targetingRangeBase;

    private Transform target;
    private float timeUntilFire;

    private int level = 1;

    private ObjectPool bulletPool;
    
    private void Start(){
        bpsBase = bps;
        bulletPool = new ObjectPool(bulletPrefab, 10); // Set the initial pool size (adjust as needed)
        
        upgradeButton.onClick.AddListener(Upgrade);

    }
    private Plot associatedPlot;

    public void SetAssociatedPlot(Plot plot)
    {
        associatedPlot = plot;
    }

    private void Update(){
        if(target == null){
            FindTarget();
            return;
        
        }

        RotateTowardsTarget();

        if(!CheckTargetIsInRange()){
            target = null;
        }else{
            timeUntilFire+= Time.deltaTime;

            if(timeUntilFire >= 1f/bps){
                Shoot();
                timeUntilFire=0f;
            }
        }
    }

    private void Shoot()
    {
        // Use the bullet pool to get a bullet
        GameObject bulletObj = bulletPool.GetPooledObject();

        // If the bullet pool is empty, create a new bullet
        if (bulletObj == null)
        {
            bulletObj = Instantiate(bulletPrefab, firingPoint.position, Quaternion.identity);
        }
        else
        {
            bulletObj.transform.position = firingPoint.position;
            bulletObj.SetActive(true);
        }

        Bullet bulletScript = bulletObj.GetComponent<Bullet>();
        bulletScript.SetTarget(target);
    }

    private void FindTarget(){
        RaycastHit2D[] hits = Physics2D.CircleCastAll(transform.position, targetingRange, (Vector2) 
        transform.position, 0f, enemyMask);

        if(hits.Length > 0){
            target = hits[0].transform;
        }
    }

    private bool CheckTargetIsInRange(){
        return Vector2.Distance(target.position, transform.position) <= targetingRange;
    }

    private void RotateTowardsTarget(){
        float angle = Mathf.Atan2(target.position.y - transform.position.y, target.position.x-
        transform.position.x) * Mathf.Rad2Deg;

        Quaternion targetRotation=Quaternion.Euler(new Vector3(0f, 0f, angle));
        catRotationPoint.rotation=Quaternion.RotateTowards(catRotationPoint.rotation, 
        targetRotation, rotationSpeed * Time.deltaTime);
    }

    public void OpenUpgradeUI(){
        //upgradeUI.SetActive(true);
    }

    public void CloseUpgradeUI(){
        upgradeUI.SetActive(false);
        UIManager.main.SetHoveringState(false);
    }

    public void Upgrade(){
        
       /* Destroy(gameObject);
        LevelManager.main.IncreaseCurrency(50);

        if (associatedPlot != null)
        {
            associatedPlot.ClearCatReference();
        }*/
    }

    private int CalculateCost(){
        return Mathf.RoundToInt(baseUpgradeCost * Mathf.Pow(level, 0.8f));
    }

    private float CalculateBPS(){
        return bpsBase * Mathf.Pow(level, 0.6f);

    }

    private float CalculateRange(){
        return targetingRangeBase * Mathf.Pow(level, 0.4f);
    }

}
