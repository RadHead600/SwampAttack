using UnityEngine;

public class BonusHpUp : Bonus
{
    [SerializeField] private int _hpAdd;

    protected override void GiveBonus()
    {
        unit.GetComponentInChildren<Character>().HealthPoints += _hpAdd;
        Destroy(gameObject);
    }
}
