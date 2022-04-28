using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UI_DeathCount : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI deathCount;
    void OnEnable(){
       UpdateDeathCount(); 
    }
    void Start(){
        UpdateDeathCount();
    }

    public void UpdateDeathCount(){
        deathCount.text = GameManager.Instance.GetDeathCount().ToString();
    }
}
