using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileScript : MonoBehaviour
{

    private Rigidbody2D myRigidBody;

    [SerializeField]
    private float speed;

    public Transform MyTarget { get; private set; }

    private int damage;

    //private bool alive = true;

    // Start is called before the first frame update
    void Start()
    {
        myRigidBody = GetComponent<Rigidbody2D>();
    }

    public void Initialize(Transform target, int damage)
    {
        this.MyTarget = target;
        this.damage = damage;

    }

    private  void FixedUpdate()
    {
        if(MyTarget != null)
        {
            Vector2 direction = MyTarget.position - transform.position;

            myRigidBody.velocity = direction.normalized * speed;

            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

        }
     }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "HitBox" && collision.transform == MyTarget)
        {
            speed = 0;
            collision.GetComponentInParent<Enemy>().TakeDamage(damage);
            GetComponent<Animator>().SetTrigger("impact");
            myRigidBody.velocity = Vector2.zero;
            MyTarget = null;
        }
    }
}
