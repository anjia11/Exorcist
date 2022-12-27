using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Health : MonoBehaviour
{
    [SerializeField] public float playerHealth;
    [SerializeField] EnemyPatrol enemyPatrol;

    // Update is called once per frame
    void Update()
    {
        if(playerHealth <= 0){
            enemyPatrol.player = null;
            Destroy(gameObject);
            int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
            SceneManager.LoadScene(currentSceneIndex);

        }
    }

    public void TakeDamage(int damage){
        if(gameObject != null){
            playerHealth -= damage;
        }
    }
}
