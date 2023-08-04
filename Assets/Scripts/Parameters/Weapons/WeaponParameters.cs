using System;
using UnityEngine;

[Serializable] 
public class WeaponParameters : ScriptableObject
{
    [SerializeField] private string _weaponName;
    [SerializeField] private int _cost;
    [SerializeField] private Sprite _weaponSprite;

    [SerializeField] private float _rechargeTime;
    [SerializeField] private int _bulletDamage;
    [SerializeField] private float _bulletSpeed;
    [SerializeField] private int _amountBulletsInMagazine;
    [SerializeField] private float _bulletDelay;
    [SerializeField] private Bullet _bullet;

    public string WeaponName => _weaponName;
    public int Cost => _cost;
    public Sprite WeaponSprite => _weaponSprite;
    public float RechargeTime => _rechargeTime;
    public int BulletDamage => _bulletDamage;
    public float BulletSpeed => _bulletSpeed;
    public int AmountBulletsInMagazine => _amountBulletsInMagazine;
    public float BulletDelay => _bulletDelay;
    public Bullet Bullet => _bullet;
}
