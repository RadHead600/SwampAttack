using UnityEngine;

public class Tavern : MonoBehaviour
{
    [SerializeField] private GameObject _shopCanvas;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (Input.GetKey(KeyCode.E))
        {
            if (collision.GetComponentInChildren<Character>() != null)
            {
                _shopCanvas.SetActive(true);
            }
        }
    }
}
