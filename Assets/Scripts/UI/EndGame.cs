using TMPro;
using UnityEngine;

public class EndGame : MonoBehaviour
{
    [SerializeField] private DropParameters _dropParameters;
    [SerializeField] private GameObject _menuEndGame;
    [SerializeField] private GameObject _lossMenu;
    [SerializeField] private TextMeshProUGUI _textMurders;
    [SerializeField] private TextMeshProUGUI _textMoney;

    void Start()
    {
        _menuEndGame.SetActive(false);
        _lossMenu.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Units unit = collision.GetComponent<Character>();
        
        if (unit == null)
            return;
            
        Time.timeScale = 0;
        _menuEndGame.SetActive(true);

        _textMurders.text = "Убийств: " + SaveParameters.numberKilled[SaveParameters.levelActive].ToString();
        _textMoney.text = (SaveParameters.numberOfCoinsRaised * _dropParameters.MoneyForCoin).ToString();
    }

    public void LossCanvas()
    {
        Time.timeScale = 0;
        _lossMenu.SetActive(true);
    }
}
