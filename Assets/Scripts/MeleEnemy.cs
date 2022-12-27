using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleEnemy : MonoBehaviour
{
    [SerializeField] private float attackCd;
    [SerializeField] private int damage;
    [SerializeField] private float range;
    [SerializeField] private CapsuleCollider2D boxCollider;
    [SerializeField] private LayerMask playerLayer;
    private float cdTimer = Mathf.Infinity;
    private Health playerHealth;
    GameObject player;
    Rigidbody2D rb;
    private EnemyPatrol enemyPatrol;

    private Animator anim;

    [SerializeField] public PlayerInfo playerInfo;

    public PlayerMovement playerMovement;
    

    private void Start() {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        enemyPatrol = GetComponentInParent<EnemyPatrol>();
    }

    // Start is called before the first frame update
    // Update is called once per frame
    void Update()
    {
        cdTimer += Time.deltaTime;

        if(PlayerInSight()){
            anim.SetBool("IsRun", false);
            if(cdTimer >= attackCd){
                cdTimer = 0;
                anim.SetTrigger("meleAttack");
                DamagePlayer();
            }
        }

        if(enemyPatrol != null){
            enemyPatrol.enabled = !PlayerInSight();
        }
    }

    private bool PlayerInSight(){
        RaycastHit2D hit = Physics2D.BoxCast(boxCollider.bounds.center + transform.right * range * transform.localScale.x,
        new Vector3(boxCollider.bounds.size.x * range, boxCollider.bounds.size.y, boxCollider.bounds.size.z), 0, Vector2.left, 0, playerLayer);
        
        if(hit.collider != null){
            playerHealth = hit.transform.GetComponent<Health>();
        }
        
        return hit.collider != null;
    }

    private void OnDrawGizmos() {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(boxCollider.bounds.center + transform.right * range * transform.localScale.x, boxCollider.bounds.size);
    }

    private void DamagePlayer(){
        if(PlayerInSight()){
            playerHealth.TakeDamage(damage);
        }
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.tag == "Player"){
            playerMovement.KBCounter = playerMovement.KBTotalTime;
            if (other.transform.position.x <= transform.position.x){
                playerMovement.KBFromRight = true;
            }
            else if (other.transform.position.x > transform.position.x){
                playerMovement.KBFromRight = false;
            }
        }
    }
}
