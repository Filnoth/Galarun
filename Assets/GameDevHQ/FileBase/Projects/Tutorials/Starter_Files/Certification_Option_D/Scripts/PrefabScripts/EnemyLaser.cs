using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLaser : MonoBehaviour
{
    private float _speed = 25f;
    private Player _player;
    private AudioSource _audio;

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
        if (other.tag == "Player")
        {
            _player = other.transform.GetComponent<Player>();
            if (_player != null)
            {
                _player.PlayerDamage();
            }
            Destroy(transform.parent.gameObject);
        }
    }
}
