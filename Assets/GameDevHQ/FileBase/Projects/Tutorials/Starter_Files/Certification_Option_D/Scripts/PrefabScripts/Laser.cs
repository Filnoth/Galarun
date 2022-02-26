using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    private float _speed = 40f;
    private Enemy _enemy;
    private AudioSource _audio;
    private MidBoss _midBoss;
    private FinalBossScript _final;
    // Start is called before the first frame update
    void Start()
    {
        _audio = GetComponent<AudioSource>();
        AudioManager.instance.Laser();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);
       

        if (transform.position.x > 35 || transform.position.x < -35 || transform.position.y > 22 || transform.position.y < -22)
        {
            Destroy(transform.parent.gameObject);
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
            Destroy(transform.parent.gameObject);
        }
    }
}
