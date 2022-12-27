using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerInfo : MonoBehaviour
{
    public TextMeshProUGUI playerHp;
    
    Health healt;
    
    void Start() {
        healt = GameObject.Find("PlayerM").GetComponent<Health>();
    }

    private void Update() {
        SetHud();
    }

    public void SetHud(){
        playerHp.text = "HP: " + healt.playerHealth;
    }


}
