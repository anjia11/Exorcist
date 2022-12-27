using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    [SerializeField] private Transform leftEdge;
    [SerializeField] private Transform rightEdge;

    [SerializeField] private Transform enemy;

    [SerializeField] private float speed;
    [SerializeField] private float idleDuration;
    private float idleTime;
    private Vector3 initScale;
    private bool movingLeft;
    [SerializeField] Rigidbody2D rb;
    Enemy die;
    [SerializeField] Animator animator;

    public Transform player;
    public Transform eny;
    public PlayerMovement playerMovement;
    


    // Start is called before the first frame update
    private void Awake() {
        initScale = enemy.localScale;
    }

    private void Start() {
        rb = transform.GetChild(0).GetComponent<Rigidbody2D>();
        die = GetComponentInChildren<Enemy>();
    }

    // Update is called once per frame
    void Update()
    {
        patrol();
        if(die.healtPoint <= 0){
            Destroy(gameObject);
        }
    }

    private void patrol(){
        if(movingLeft){
            if(enemy.position.x >= leftEdge.position.x){
                MoveInDirection(-1);
                if (player.transform.position.x <= eny.transform.position.x){
                    playerMovement.KBFromRight = false;
                    
                }
            }else{
                DirectionChange();
            }
        }else{
            if(enemy.position.x <= rightEdge.position.x){
                MoveInDirection(1);
                if (player.transform.position.x > eny.transform.position.x){
                    playerMovement.KBFromRight = true;
                    
                }
            }else{
                DirectionChange();
            }
        }
    }


    private void DirectionChange(){
        idleTime += Time.deltaTime;

        if(idleDuration < idleTime){
            movingLeft = !movingLeft;
        }
        animator.SetBool("IsRun", false);
    }

    private void MoveInDirection(int _direction){
        idleTime = 0;

        enemy.localScale = new Vector3(Mathf.Abs(initScale.x) * _direction, initScale.y, initScale.z);

        enemy.position = new Vector3(enemy.position.x + Time.deltaTime * _direction * speed, enemy.position.y, enemy.position.z);
        animator.SetBool("IsRun", true);
    }
}
