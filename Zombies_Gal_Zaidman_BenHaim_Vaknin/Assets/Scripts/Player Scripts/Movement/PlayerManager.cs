 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour
{
    [SerializeField]
    private Rigidbody2D _myBody;

    [SerializeField]
    private GameObject _deafultWeapon, _machineGun, _canonWeapon, _bullet;

    [SerializeField]
    private Transform _bulletLocation, _gunPos;

    [SerializeField]
    private Image _currentWeaponSprite;

    [SerializeField]
    private List<SpriteRenderer> _allWeaponSprites;

    [SerializeField]
    private float _readyToShoot = 0.3f, _bulletForce = 20, _rotationSpeed = 100;

    private Vector2 moveVelocity;
    private bool _holdingDefaultWeapon = true;
    private bool _holdingMachineGunWeapon, _holdingCanonWeapon, _isShooting = false;

    public float speed;
    public JoyStick moveJoystick;
    public JoyStick shootJoystick;

    public bool IsShooting { get => _isShooting; set => _isShooting = value; }



    public bool canShoot;

    private void Update()
    {
        _readyToShoot -= Time.deltaTime;
        if (Input.GetMouseButton(0) && canShoot)
        {
            if (_readyToShoot <= 0)
            {

                Shoot();

                if (_holdingDefaultWeapon)
                {
                    _readyToShoot = 0.3f;
                    _bulletForce = 20f;
                }

                if (_holdingMachineGunWeapon)
                {
                    _readyToShoot = 0.1f;
                    _bulletForce = 10f;
                }

                if (_holdingCanonWeapon)
                {
                    _readyToShoot = 0.5f;
                    _bulletForce = 50f;
                }
            }
        }

        Rotation();
        Movement();
    }

    private void FixedUpdate()
    {
        MoveRb();
    }

    void Movement()
    {
        Vector2 moveInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        if (moveJoystick.InputDir != Vector3.zero)
        {
            moveInput = moveJoystick.InputDir;
        }
        moveVelocity = moveInput.normalized * speed;
    }

    void MoveRb()
    {
        _myBody.MovePosition(_myBody.position + moveVelocity * Time.fixedDeltaTime);

/*        if (moveVelocity == Vector2.zero)
        {
            //anim
        }
        else
        {
            //anim
        }*/
    }

    private void Shoot()
    {
        GameObject _shotClone = Instantiate(_bullet, _bulletLocation.position, _bulletLocation.rotation);
        Rigidbody2D rb = _shotClone.GetComponent<Rigidbody2D>();
        rb.AddForce(_bulletLocation.up * _bulletForce, ForceMode2D.Impulse);
    }

    public void ShootCycle()
    {
        if (_holdingDefaultWeapon)
        {
            _deafultWeapon.SetActive(false);
            _machineGun.SetActive(true);
            _holdingMachineGunWeapon = true;
            _holdingDefaultWeapon = false;
            _currentWeaponSprite.sprite = _allWeaponSprites[1].sprite;
            Debug.Log("machineGun");
        }
        else if (_holdingMachineGunWeapon)
        {
            _machineGun.SetActive(false);
            _canonWeapon.SetActive(true);
            _holdingMachineGunWeapon = false;
            _holdingCanonWeapon = true;
            _currentWeaponSprite.sprite = _allWeaponSprites[2].sprite;
            Debug.Log("canonGun");
        }
        else if (_holdingCanonWeapon)
        {
            _canonWeapon.SetActive(false);
            _deafultWeapon.SetActive(true);
            _holdingMachineGunWeapon = false;
            _holdingCanonWeapon = false;
            _holdingDefaultWeapon = true;
            _currentWeaponSprite.sprite = _allWeaponSprites[0].sprite;
            Debug.Log("defaultGun");
        }
    }

    public void Rotation()
    {
        Vector2 dir = Camera.main.ScreenToWorldPoint(Input.mousePosition) - _gunPos.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg - 90;

        if (shootJoystick.InputDir != Vector3.zero)
            angle = Mathf.Atan2(shootJoystick.InputDir.y, shootJoystick.InputDir.x) * Mathf.Rad2Deg - 90;

        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        _gunPos.rotation = Quaternion.Slerp(_gunPos.rotation, rotation, _rotationSpeed * Time.fixedDeltaTime);
    }
}
