using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopWeaponButton : MonoBehaviour
{ 
    [SerializeField] private Button _button;
    [SerializeField] private TextMeshProUGUI _costText;
    [SerializeField] private TextMeshProUGUI _nameText;
    [SerializeField] private Image _weaponImage;

    public Button Button => _button;
    public TextMeshProUGUI CostText => _costText;
    public TextMeshProUGUI NameText => _nameText;
    public Image WeaponImage => _weaponImage;
}
