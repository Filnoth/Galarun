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
    private int _lives;
    [SerializeField]
    private int _health;

    [SerializeField]
    private GameObject[] _laserPrefab;
    private int _laserState = 0;
    Vector3 _laserOffset = new Vector3(4.7f, 0, 0);
    private float _fireRate = 0.25f;
    private float _canFire = -1f;

    [SerializeField]
    private GameObject[] _drones;

    private void Start()
    {
        _speedTotal = _speed;
        _lives = 3;
        _health = 1;
    }

    private void Update()
    {
        DroneOrbit();
    }
    public void Move (Vector2 direction)
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
                break;
            case (0):
                transform.localEulerAngles = new Vector3(0, 0, 0);
                break;
            case (1):
                transform.localEulerAngles = new Vector3(0, 0, 15);
                break;
        }
    }

    public void Boost(bool thrust)
    {
        
        _thrusters = thrust;
        StartCoroutine(Thrusters());
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

    public void Explode()
    {
        FireworkShot.Instance.Explode();
    }

    public void HealthStatus()
    {
        _health++;
    }

    public void DroneOrbit()
    {
        switch (_health)
        {
            case (1):
                _drones[0].SetActive(false);
                _drones[1].SetActive(false);
                _laserState = 0;
                break;
            case (2):
                _drones[0].SetActive(true);
                _drones[1].SetActive(false);
                _laserState = 1;
                break;
            case (3):
                _drones[1].SetActive(true);
                _drones[2].SetActive(false);
                _laserState = 2;
                break;
            case (4):
                _drones[2].SetActive(true);
                _drones[3].SetActive(false);
                _laserState = 3;
                break;
            case (5):
                _drones[3].SetActive(true);
                break;
        }
    }

    
}
