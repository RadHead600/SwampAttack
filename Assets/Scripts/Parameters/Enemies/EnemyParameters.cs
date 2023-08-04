using System;
using UnityEngine;

[Serializable]
public class EnemyParameters : ScriptableObject
{
    [SerializeField] private Weapon _weapon;
    [SerializeField] private int _healthPoints;

    public Weapon Weapon => _weapon;
    public int HealthPoints => _healthPoints;
}
