using UnityEngine;

[CreateAssetMenu(fileName = "CharacterParameters", menuName = "CustomParameters/CharacterParameters")]
public class CharacterParameters : ScriptableObject
{
    [SerializeField] private float _speed;
    [SerializeField] private float _jump;
    [SerializeField] private int _healthPoints;
    [SerializeField] private LayerMask _blockStay;

    public float Speed => _speed;
    public float Jump => _jump;
    public int HealthPoints => _healthPoints;
    public LayerMask BlockStay => _blockStay;
}
