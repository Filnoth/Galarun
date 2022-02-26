using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireworkShot : MonoBehaviour
{
    private static FireworkShot _instance;
    public static FireworkShot Instance
    {
        get
        {
            if (_instance == null)
            {
                Debug.LogError("Fireworkshot is NULL");
            }
            return _instance;
        }
    }

    private int _speed = 25;
    [SerializeField]
    private GameObject[] _lasers;
    private float _chargeTime = 1f;
    private float _canExplode;
    private Enemy _enemy;
    private MidBoss _midBoss;
    private FinalBossScript _final;

    

    private void Start()
    {
        _instance = this;
        _canExplode = Time.time + _chargeTime;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.right * _speed * Time.deltaTime);
        if (Time.time > _canExplode)
        {
            gameObject.GetComponent<SpriteRenderer>().enabled = false;
            for (int i = 0; i < _lasers.Length; i++)
            {
                _lasers[i].SetActive(true);

            }
            if (transform.position.x > 35)
            {
                Destroy(transform.gameObject);
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
            _enemy = other.transform.GetComponent<Enemy>();
            _midBoss = other.transform.GetComponent<MidBoss>();
            _final = other.transform.GetComponent<FinalBossScript>();
            if (_enemy != null)
            {
                _enemy.Damaged();
            }
            else if (_midBoss != null)
            {
                _midBoss.Damaged();
            }
            else if (_final != null)
            {
                _final.Damaged();
            }
            Destroy(transform.gameObject);
        }
    }
}
