using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : MonoBehaviour
{
    private float _speed = 20f;
    private Player _player;
    // Start is called before the first frame update
    void Start()
    {
        transform.eulerAngles = new Vector3(0, 0, 90);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.up * _speed * Time.deltaTime);

        if (transform.position.x < -34)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            _player = other.transform.GetComponent<Player>();
            if (_player != null)
            {
                _player.HealthStatus();
            }
            Destroy(this.gameObject);
        }
    }
}


