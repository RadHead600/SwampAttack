using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Rigidbody2D))]
public class Character : Units
{
    [SerializeField] private CharacterParameters _parameters;

    [SerializeField] private GameObject _HPText;
    [SerializeField] private GameObject _bulletsText;
    [SerializeField] private ParticleSystem _bloodParticles;

    [SerializeField] private Texture2D _aimCursorTexture;
    [SerializeField] private Texture2D _reloadCursorTexture;
    [SerializeField] private GameObject _hand;
    [SerializeField] private Transform _body;
    [SerializeField] private GameObject[] _legs;

    private float _offset;
    private bool _isLockShootCoroutine;
    private Rigidbody2D _rigidBody;
    private Weapon _weaponAttack;

    private void Awake()
    {
        Time.timeScale = 1;
        _rigidBody = GetComponent<Rigidbody2D>();
        SetWeapon(SaveParameters.weaponsBought[SaveParameters.weaponEquip]);
        _weaponAttack.Reloading += SetReloadCursor;
        _weaponAttack.Reloaded += SetAimCursor;
    }

    private void Start()
    {
        HealthPoints = _parameters.HealthPoints;
        SaveParameters.numberOfCoinsRaised = 0;
        SetAimCursor();
    }

    private void FixedUpdate()
    {
        _HPText.GetComponent<Text>().text = $"{HealthPoints}";
        _bulletsText.GetComponent<Text>().text = $"{_weaponAttack.AmountBulletsInMagazine + "/" + _weaponAttack.AmountBulletsInMagazineStandart}";
        RotateWeapons();
        RotateBody();
    }

    private void Update()
    {
        IsGrounded();
        Run();

        if (Input.GetButtonDown("Jump") && IsGrounded())
            Jump();
        if (Input.GetMouseButton(0) && !_isLockShootCoroutine)
            StartCoroutine(Shoot());
    }

    public void RotateWeapons()
    {
        Vector3 difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position; 
        float rotate = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        _hand.transform.rotation = Quaternion.Euler(0f, 0f, rotate + _offset);
    }

    public void RotateBody()
    {
        Vector3 difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        Vector3 pos = _body.transform.localScale;
        _body.transform.localScale = new Vector3(
            (difference.x < 0 ? Math.Abs(pos.x) * -1 : Math.Abs(pos.x)) * (transform.localScale.x > 0 ? -1 : 1),
            pos.y,
            pos.z
            );

        if (pos.x > 0)
        {
            _offset = -180;
            if (transform.localScale.x < 0)
                _offset = 0;
            return;
        }
        _offset = 0;
        if (transform.localScale.x < 0)
            _offset = -180;
    }

    private void Run()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        gameObject.GetComponentInChildren<Animation>().IsRun = horizontal != 0;
        _legs[0].GetComponentInChildren<SpriteRenderer>().flipX = (horizontal > 0 && _offset > 0);
        _legs[1].GetComponentInChildren<SpriteRenderer>().flipX = (horizontal > 0 && _offset > 0);

        Vector2 movement = new Vector3(horizontal * Time.deltaTime, 0, 0);
        if (movement.x == 0)
        {
            _rigidBody.velocity = new Vector2(0, _rigidBody.velocity.y);
            return;
        }
        if (_rigidBody.velocity.x > -_parameters.Speed && _rigidBody.velocity.x < _parameters.Speed)
        {
            _rigidBody.AddForce(movement.normalized, ForceMode2D.Impulse);
            return;
        }
        _rigidBody.velocity = new Vector2(_parameters.Speed * horizontal, _rigidBody.velocity.y);
    }

    public void Jump()
    {
        _rigidBody.AddForce(transform.up.normalized * _parameters.Jump, ForceMode2D.Impulse);
    }

    public bool IsGrounded()
    {
        return Physics2D.OverlapCircleAll(transform.position, .3F, _parameters.BlockStay).Length > 0.8;
    }

    private IEnumerator Shoot()
    {
        _isLockShootCoroutine = true;
        yield return new WaitForSeconds(_weaponAttack.Parameters.BulletDelay);

        Vector3 difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        _weaponAttack.Attack(difference);
        _isLockShootCoroutine = false;
    }

    public override int ReceiveDamage(int damage)
    {
        _rigidBody.velocity = Vector3.zero;
        _rigidBody.AddForce(transform.up * (_parameters.Jump / 3), ForceMode2D.Impulse);
        
        ParticleSystem particle = Instantiate(_bloodParticles, transform.position, transform.rotation);
        particle.Play();
        Destroy(particle.gameObject, particle.startLifetime);

        if (HealthPoints - damage <= 0)
        {
            FindObjectOfType<EndGame>().LossCanvas();
            return 0;
        }
        return HealthPoints -= damage;
    }

    public void SetWeapon(Weapon weapon)
    {
        if (SaveParameters.weaponsBought != null)
        {
            foreach (Transform weaponT in _hand.GetComponentInChildren<Transform>())
                Destroy(weaponT.gameObject);
            _weaponAttack = Instantiate(weapon);
            _weaponAttack.transform.parent = _hand.transform;
            _weaponAttack.transform.localScale = weapon.transform.localScale;
            _weaponAttack.transform.localPosition = Vector3.zero;
            _weaponAttack.transform.localRotation = Quaternion.identity;
        }
    }

    private void SetAimCursor()
    {
        Cursor.SetCursor(_aimCursorTexture, new Vector2(100, 100), CursorMode.Auto);
    }

    private void SetReloadCursor()
    {
        Cursor.SetCursor(_reloadCursorTexture, Vector2.zero, CursorMode.Auto);
    }

    private void OnDestroy()
    {
        _weaponAttack.Reloading -= SetReloadCursor;
        _weaponAttack.Reloaded -= SetAimCursor;
    }
}
