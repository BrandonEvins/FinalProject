using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plot : MonoBehaviour
{

    [Header("References")]
    [SerializeField] private SpriteRenderer sr;
    [SerializeField] private Color hoverColor;

    private GameObject towerObj;
    public Cat cat;
    private Color startColor;
    private AudioSource broke;
    public AudioClip brokeSound;

    public void Awake(){
        GameObject brokeAudioSourceObject = new GameObject("BrokeAudioSource");
        broke = brokeAudioSourceObject.AddComponent<AudioSource>();
        broke.clip = brokeSound;
    }
    private void Start(){
        startColor = sr.color;
    }

    private void OnMouseEnter(){
        sr.color = hoverColor;
    }

    private void OnMouseExit(){
        sr.color = startColor;
    }

    private void OnMouseDown(){
        if(UIManager.main.IsHoveringUI()) return;

        if(towerObj != null){
            cat.OpenUpgradeUI();
            return;
        }

        Tower towerToBuild = BuildManager.main.GetSelectedTower();

        if(towerToBuild.cost > LevelManager.main.currency){
            broke.Play();
            Debug.Log("You Broke");
            return;
        }

        LevelManager.main.SpendCurrency(towerToBuild.cost);
        
        towerObj = Instantiate(towerToBuild.prefab, transform.position, Quaternion.identity);
        cat = towerObj.GetComponent<Cat>();
    }
}
