using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    // Start is called before the first frame update
    Rigidbody2D rb;
    [SerializeField] float speed;
    public float lifeTime;
    [SerializeField] GameObject [] target;
    public bool isHit;
    public float distance;
    public float minRangeAttack;
    [SerializeField] public LayerMask hitable;
    public Transform closeEnemy = null;

    PlayerMovement player;



    public int damage;

    void Start()
    {
        target = GameObject.FindGameObjectsWithTag("Enemy");
        isHit = true;
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.Find("PlayerM").GetComponent<PlayerMovement>();
        closeEnemy = GetClosestEnemy();

        if (closeEnemy != null){
            GetClosestEnemy();
        }else{
            NoEnemy();
        }



    }
    void Update() {
        RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, transform.up, distance, hitable);
        if (hitInfo.collider != null)
        {
            if (hitInfo.collider.CompareTag("Enemy") && isHit)
            {
                Debug.Log("Kena Hit");
                hitInfo.collider.GetComponent<Enemy>().KenaDamage(damage);
                Invoke("DestroyPedangTerbang", 0f);
                isHit = false;
                Debug.DrawRay(transform.position, transform.up, Color.red, 0.5f);
            }
        }
    }

    public Transform GetClosestEnemy(){
        
        float closeDistance = minRangeAttack;
        Transform trans = null;
        
        foreach (GameObject go in target){
            float currentDistance = Vector3.Distance(transform.position, go.transform.position);
            if (currentDistance < closeDistance){
                closeDistance = currentDistance;
                trans = go.transform;
                Vector3 moveDirection = (go.transform.position - transform.position);

                rb.velocity = new Vector2(moveDirection.x, moveDirection.y).normalized * speed;
                
                float rot = Mathf.Atan2(-moveDirection.y, -moveDirection.x) * Mathf.Rad2Deg;
                transform.rotation = Quaternion.Euler(0, 0, rot - 90);
                Invoke("DestroyPedangTerbang", lifeTime);
                Debug.DrawLine(transform.position, go.transform.position, Color.green, 0.5f);
            }
        }
        return trans;
    }

    void NoEnemy(){
        if (player.isFacingLeft == true){
            rb.velocity = -transform.right * speed;
        }else{
            rb.velocity = transform.right * speed;
        }
        transform.rotation = Quaternion.Euler(0,0, -90);
        Invoke("DestroyPedangTerbang", lifeTime);
    }
    // void OnCollisionEnter2D(Collision2D other) {
    //     if (other.gameObject.tag == "Enemy"){
    //         Destroy(gameObject);
    //     }
    // }

    void DestroyPedangTerbang(){
        Destroy(gameObject);
    }
}