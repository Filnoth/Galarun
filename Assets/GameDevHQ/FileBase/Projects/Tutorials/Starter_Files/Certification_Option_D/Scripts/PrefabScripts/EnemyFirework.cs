using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFirework : MonoBehaviour
{
    
    private int _speed = 25;
    [SerializeField]
    private GameObject[] _lasers;
    private float _chargeTime = 1f;
    private float _canExplode;

    private void Awake()
    {
        _canExplode = Time.time + _chargeTime;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.left * _speed * Time.deltaTime);
        if (Time.time > _canExplode)
        {
            gameObject.GetComponent<SpriteRenderer>().enabled = false;
            for (int i = 0; i < _lasers.Length; i++)
            {
                _lasers[i].SetActive(true);
                
            }
            if (transform.position.x < -35)
            {
                Destroy(transform.parent.gameObject);
            }
        }
    }
}
