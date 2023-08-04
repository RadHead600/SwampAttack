using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField] private DropParameters _parameters;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponentInChildren<Character>() != null)
        {
            SaveParameters.numberOfCoinsRaised++;
            SaveParameters.money += _parameters.MoneyForCoin;
            Destroy(gameObject);
        }   
    }
}
