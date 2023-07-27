using System;
using UnityEngine;

[Serializable] // ������ ����� �������� �� ScriptableObject'�. ��� �������� ������ �����, ��������� ������� �����-��������� � ������� ScriptableObject � Unity. ����� ��������� � ��������� ScriptableObject � ������ �����.
public class EnemyParameters : ScriptableObject
{
    [SerializeField] private Weapon weapon;
    [SerializeField] private int healthPoints;

    public Weapon Weapon => weapon;
    public int HealthPoints => healthPoints;
}
