using UnityEngine;

public abstract class Enemy : Units
{
    [SerializeField] protected GameObject player;
    [SerializeField] private EnemyParameters _parameters;
    [SerializeField] private DropParameters _dropParameters;
    [SerializeField] private Rigidbody2D _rigidBody;
    [SerializeField] private ParticleSystem bloodParticles;

    public EnemyParameters Parameters => _parameters;

    public override int ReceiveDamage(int damage)
    {
        _rigidBody.velocity = Vector3.zero;
        _rigidBody.AddForce(transform.up * 2.0F, ForceMode2D.Impulse);

        ParticleSystem particle = Instantiate(bloodParticles, transform.position, transform.rotation);
        particle.Play();
        Destroy(particle.gameObject, particle.startLifetime);

        if (HealthPoints - damage <= 0)
        {
            Die();
            return 0;
        }
        return HealthPoints -= damage;
    }

    public override void Die()
    {
        SaveParameters.numberKilled[SaveParameters.levelActive] += 1;
        Instantiate(_dropParameters.Coin, transform.position + new Vector3(0, 1, 0), _dropParameters.Coin.transform.rotation);
        Destroy(gameObject);
    }
}
