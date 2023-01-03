using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] public int healtPoint = 10000;
    [SerializeField] public float speedEn;

    void Update() {
    if (healtPoint <= 0){
        healtPoint = 0;
        Destroy(gameObject);
    }
    }
    public void KenaDamage(int damage){
        if(gameObject != null){
            healtPoint -= damage;
        }
    }
}
