using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EnemyInfo : MonoBehaviour
{
    [SerializeField] public Enemy hpEnemy;
    public TextMeshProUGUI enemyHp;
    // Start is called before the first frame update
    void Start()
    {
        hpEnemy = GetComponent<Enemy>();
    }

    // Update is called once per frame
    void Update()
    {
        SetHp();
    }

    void SetHp(){
        enemyHp.text = "HP ENEMY: " + hpEnemy.healtPoint;
    }
}
