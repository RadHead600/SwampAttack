using System;
using UnityEngine;

[Serializable] 
public class WeaponParameters : ScriptableObject
{
    [SerializeField] private string weaponName;
    [SerializeField] private int cost;
    [SerializeField] private Sprite weaponSprite;

    [SerializeField] private float rechargeTime;
    [SerializeField] private int bulletDamage;
    [SerializeField] private float bulletSpeed;
    [SerializeField] private int amountBulletsInMagazine;
    [SerializeField] private float bulletDelay;
    [SerializeField] private Bullet bullet;

    public string WeaponName => weaponName;
    public int Cost => cost;
    public Sprite WeaponSprite => weaponSprite;
    public float RechargeTime => rechargeTime;
    public int BulletDamage => bulletDamage;
    public float BulletSpeed => bulletSpeed;
    public int AmountBulletsInMagazine => amountBulletsInMagazine;
    public float BulletDelay => bulletDelay;
    public Bullet Bullet => bullet;
}
