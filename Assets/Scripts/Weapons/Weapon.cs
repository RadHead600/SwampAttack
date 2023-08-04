using System;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] private WeaponParameters _parameters;
    [SerializeField] private GameObject _posAttack;

    [NonSerialized] public bool isEnemy = false;
    
    private float _timerRecharge;
    private int _amountBulletsInMagazine;

    public delegate void ReloadingDelegate();
    public event ReloadingDelegate Reloading;
    public delegate void ReloadedDelegate();
    public event ReloadedDelegate Reloaded;

    public WeaponParameters Parameters => _parameters;
    public int AmountBulletsInMagazine => _amountBulletsInMagazine;
    public int AmountBulletsInMagazineStandart => _parameters.AmountBulletsInMagazine;

    private void Start()
    {
        _timerRecharge = _parameters.RechargeTime;
        _amountBulletsInMagazine = _parameters.AmountBulletsInMagazine;
    }

    private void Update()
    {
        if(_timerRecharge > 0)
            _timerRecharge -= Time.deltaTime;
        else if (_amountBulletsInMagazine == 0)
        {
            if (Reloaded != null)
                Reloaded.Invoke();
            _amountBulletsInMagazine = _parameters.AmountBulletsInMagazine;
        }
    }

    public void Attack(Vector3 difference)
    {
        if (_timerRecharge > 0 || _posAttack == null)
            return;

        Bullet newBullet = Instantiate(_parameters.Bullet, _posAttack.transform.position, _posAttack.transform.rotation);

        if (isEnemy)
            newBullet.gameObject.layer = LayerMask.NameToLayer("BulletEnemy");

        newBullet.Speed = _parameters.BulletSpeed;
        newBullet.Damage = _parameters.BulletDamage;
        newBullet.Direction = newBullet.transform.right * (difference.x < 0 ? -1 : 1);

        TakeAwayBullet(1);
    }

    public void TakeAwayBullet(int quantity)
    {
        _amountBulletsInMagazine -= quantity;
        
        if (_amountBulletsInMagazine <= 0)
        {
            if (Reloading != null)
                Reloading.Invoke();
            _timerRecharge = _parameters.RechargeTime;
            return;
        }
    }
}
