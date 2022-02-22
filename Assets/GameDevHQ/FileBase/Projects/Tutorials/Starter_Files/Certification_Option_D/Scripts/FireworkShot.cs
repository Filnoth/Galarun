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
    private bool _charged = false;

    private void Awake()
    {
        _instance = this;
        _canExplode = Time.time + _chargeTime;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.right * _speed * Time.deltaTime);

        if (transform.position.x > 35)
        {
            Destroy(transform.parent.gameObject);
        }

        if (Time.time > _canExplode)
        {
            _charged = true;
        }
    }

    public void Explode()
    {
        if (_charged == true)
        {
            gameObject.GetComponent<SpriteRenderer>().enabled = false;
            for (int i = 0; i < _lasers.Length; i++)
            {
                _lasers[i].SetActive(true);
            }
        }
        
    }
}
