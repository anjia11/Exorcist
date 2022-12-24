using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    // Start is called before the first frame update
    Rigidbody2D rb;
    [SerializeField] float speed;
    [SerializeField] Enemy target;
    public float lifeTime;
    [SerializeField] GameObject enemy;

    public float distance;
    public LayerMask hitable;

    public int damage;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        target = GameObject.FindObjectOfType<Enemy>();

        Vector3 moveDirection = (target.transform.position - transform.position);

        rb.velocity = new Vector2(moveDirection.x, moveDirection.y).normalized * speed;
        
        float rot = Mathf.Atan2(-moveDirection.y, -moveDirection.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, rot - 90);

        Invoke("DestroyPedangTerbang", lifeTime);

    }
    void Update() {
        RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, transform.up, distance, hitable);
        if (hitInfo.collider != null)
        {
            if (hitInfo.collider.CompareTag("Enemy"))
            {
                Debug.Log("Kena Hit");
                hitInfo.collider.GetComponent<Enemy>().KenaDamage(damage);
            }
        }
        Debug.DrawRay(transform.position, transform.up, Color.red, 2f);
        //DestroyPedangTerbang();
        //transform.Translate(transform.up * speed * Time.deltaTime);
    }

    void DestroyPedangTerbang(){
        Destroy(gameObject);
    }
}