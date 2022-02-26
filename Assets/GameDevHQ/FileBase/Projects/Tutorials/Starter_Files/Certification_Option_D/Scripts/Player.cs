using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private int _speed = 10;
    private int _boost = 2;
    private int _speedTotal;
    private bool _thrusters;
    [SerializeField]
    private GameObject _thrusterParent;
    [SerializeField]
    private int _lives = 3;
    [SerializeField]
    private int _health;

    [SerializeField]
    private GameObject[] _laserPrefab;
    private int _laserState = 0;
    Vector3 _laserOffset = new Vector3(4.7f, 0, 0);
    private float _fireRate = 0.4f;
    private float _canFire = -1f;

    [SerializeField]
    private GameObject[] _drones;
    private bool _gamePaused = false;
    private bool _isDead = false;
    [SerializeField]
    private ParticleSystem _explosion;
    [SerializeField]
    private ParticleSystem[] _smoke;

    private void Start()
    {
        _speedTotal = _speed;
        _lives = 3;
        _health = 1;
    }

    private void Update()
    {
        DroneOrbit();
        Boundries();
    }
    public void Move (Vector2 direction)
    {
        if (_isDead == false)
        {
            transform.Translate(direction * Time.deltaTime * _speedTotal);

            //thrusters strength
            var _thrustSize = _thrusterParent.GetComponent<Transform>();
            var size = direction.x;
            switch (size)
            {
                case (-1):
                    _thrustSize.localScale = new Vector3(0f, 1, 1);
                    break;
                case (0):
                    _thrustSize.localScale = new Vector3(0.8f, 1, 1);
                    break;
                case (1):
                    _thrustSize.localScale = new Vector3(1.2f, 1, 1);
                    break;
            }

            var pitch = direction.y;
            switch (pitch)
            {
                case (-1):
                    transform.localEulerAngles = new Vector3(0, 0, -15);
                    _thrustSize.localScale *= 1;
                    break;
                case (0):
                    transform.localEulerAngles = new Vector3(0, 0, 0);
                    _thrustSize.localScale *= 1;
                    break;
                case (1):
                    transform.localEulerAngles = new Vector3(0, 0, 15);
                    _thrustSize.localScale *= 1;
                    break;
            }
        }
        
        else
        {
             return;
        }
    }

    private void Boundries()
    {
        //if (transform.position.y > 12 || transform.position.y < -14 || transform.position.x < -8 || transform.position.x > 24)
        if (transform.position.y > 12)
        {
            transform.position = new Vector3(transform.position.x, 12, 0);
        }

        else if (transform.position.y < -14)
        {
            transform.position = new Vector3(transform.position.x, -14, 0);
        }

        else if (transform.position.x > 0)
        {
            transform.position = new Vector3(0, transform.position.y, 0);
        }

        else if (transform.position.x < -30)
        {
            transform.position = new Vector3(-30, transform.position.y, 0);
        }
    }

    public void Boost(bool thrust)
    {
        if (_isDead == false)
        {
            _thrusters = thrust;

            StartCoroutine(Thrusters());
        }

        else
        {
            return;
        }
        
    }

    IEnumerator Thrusters()
    {
        while (_thrusters == true)
        {
            _speedTotal = _speed * _boost;
            yield return null;
        }

        while (_thrusters == false)
        {
            _speedTotal = _speed;
            yield return null;
        }
    }

    public void Fire()
    {
        if (_isDead == false)
        {
            if (Time.time > _canFire && _laserState <= 2)
            {
                _canFire = Time.time + _fireRate;
                Instantiate(_laserPrefab[_laserState], transform.position, Quaternion.identity);
            }

            if (Time.time > _canFire && _laserState == 3)
            {
                _canFire = Time.time + _fireRate;
                Instantiate(_laserPrefab[3], transform.position, Quaternion.identity);
            }
        }
        else
        {
            return;
        }
        
    }

    public void HealthStatus()
    {
        _health++;
        AudioManager.instance.PowerUp();
        if (_health > 5)
        {
            _health = 5;
        }
    }

    public void PlayerDamage()
    {
        UIManager.instance.Score(-25);
        _health--;
        AudioManager.instance.PlayerHit();
        if (_health == 0)
        {
            Lives();
        }
    }

    private void Lives()
    {
        _health = 1;
        _lives--;
        UIManager.instance.Lives(_lives +1);
        Smoke();
        if (_lives < 1)
        {
            _lives = 0;
            _explosion.Play();
            _isDead = true;
            UIManager.instance.GameOver();
            AudioManager.instance.PlayerDeath();
            Destroy(this.gameObject, 1f);
        }
    }

    private void Smoke()
    {
        if (_lives == 2)
        {
            _smoke[0].Play();
        }
        if (_lives == 1)
        {
            _smoke[1].Play();
        }
    }

    public void DroneOrbit()
    {
        switch (_health)
        {
            case (1):
                _drones[0].SetActive(false);
                _drones[1].SetActive(false);
                _laserState = 0;
                UIManager.instance.WeaponType(0, 1, 2);
                break;
            case (2):
                _drones[0].SetActive(true);
                _drones[1].SetActive(false);
                _laserState = 1;
                UIManager.instance.WeaponType(1, 0, 2);
                break;
            case (3):
                _drones[1].SetActive(true);
                _drones[2].SetActive(false);
                _laserState = 2;
                UIManager.instance.WeaponType(2, 1, 3);
                break;
            case (4):
                _drones[2].SetActive(true);
                _drones[3].SetActive(false);
                _laserState = 3;
                UIManager.instance.WeaponType(3, 2, 1);
                break;
            case (5):
                _drones[3].SetActive(true);
                break;
        }

    }

    public void PauseGame()
    {
        if (_gamePaused == false)
        {
            _gamePaused = true;
            UIManager.instance.Menu();
            Time.timeScale = 0;
            Cursor.visible = true;
        }
        else
        {
            UIManager.instance.CloseMenu();
            Cursor.visible = false;
            _gamePaused = false;
            Time.timeScale = 1;
        }
    }

}
