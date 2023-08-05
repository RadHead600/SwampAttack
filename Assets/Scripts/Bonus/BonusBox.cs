using UnityEngine;

public class BonusBox : Units
{
    [SerializeField] private GameObject[] _bonuses;

    private void Start()
    {
        HealthPoints = 1;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Bullet bullet = collision.GetComponentInChildren<Bullet>();
        
        if(bullet != null)
            DropBonus();
    }

    public void DropBonus()
    {
        GameObject dropBonus = Instantiate(_bonuses[Random.Range(0, _bonuses.Length)].gameObject,
        transform.position, transform.rotation) as GameObject;
        dropBonus.SetActive(true);
        Destroy(gameObject);  
    }
}
