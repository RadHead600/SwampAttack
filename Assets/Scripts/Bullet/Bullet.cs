using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private LayerMask blocks;

    public float Speed { get; set; }
    public int Damage { get; set; }
    public Vector3 Direction { get; set; }

    private void Start()
    {
        Destroy(gameObject, 4);
    }

    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, transform.position + Direction, Speed * Time.deltaTime);
    }

    protected void OnTriggerEnter2D(Collider2D collider)
    {
        Units unit = collider.GetComponentInChildren<Units>();
        
        if (unit != null)
        {
            unit.ReceiveDamage(Damage);
            Destroy(gameObject);
        }

        float position = 0.2f;

        float length 0.8f;

        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, position , blocks);
        
        if (colliders.Length > length)
        {
            Destroy(gameObject);
        }
    }
}
