using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    private float waktuBisaAttack;
    [SerializeField] public float mulaiWaktuAttack;
    [SerializeField] Transform posisiAttack;
    [SerializeField] GameObject prefPedang;
    public int damage = 1000;
    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {
        if (waktuBisaAttack <= 0){
            if(Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.L)){
                Instantiate(prefPedang, posisiAttack.position, Quaternion.identity);
                waktuBisaAttack = mulaiWaktuAttack;
            }
        }
        else 
        {
            waktuBisaAttack -=Time.deltaTime;
        }
    }

}
