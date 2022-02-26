using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private GameObject[] _enemyLaser;
    [SerializeField]
    private GameObject _upgrade;
    [SerializeField]
    private int _health;
    private AudioSource _death;
    private Animator _anim;
    [SerializeField]
    private ParticleSystem _explosion;
    private Renderer _render;
    private int _upgradeChance;
    private BoxCollider _collider;

    // Start is called before the first frame update
    void Start()
    {
        _death = GetComponent<AudioSource>();
        _anim = GetComponent<Animator>();
        _render = GetComponent<MeshRenderer>();
        _collider = GetComponent<BoxCollider>();
        PowerUpChance();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void FireDown()
    {
        Instantiate(_enemyLaser[1], transform.position, Quaternion.identity);
    }

    public void FireStraight()
    {
        Instantiate(_enemyLaser[0], transform.position, Quaternion.identity);
    }

    public void FireUp()
    {
        Instantiate(_enemyLaser[1], transform.position, Quaternion.identity);
    }

    public void Damaged()
    {
        if (transform.position.x < 36)
        {
            _health--;
            AudioManager.instance.Hit();

            if (_health > 0)
            {
                UIManager.instance.Score(50);
            }

            else if (_health <= 0)
            {
                UIManager.instance.Score(100);
                Death();
            }
        }
    }

    public void Death()
    {
        _collider.enabled = false;
        _explosion.Play();
        _anim.enabled = false;
        AudioManager.instance.Death();
        _render.enabled = false;
        if (_upgradeChance == 4)
        {
            Instantiate(_upgrade, transform.position, Quaternion.identity);
        }
        Destroy(this.gameObject, 2f);
    }

    private void PowerUpChance()
    {
        _upgradeChance = Random.Range(0, 5);

        switch (_upgradeChance)
        {
            case (0):
                _render.material.color = Color.white;
                break;

            case (1):
                _render.material.color = Color.white;
                break;

            case (2):
                _render.material.color = Color.white;
                break;

            case (3):
                _render.material.color = Color.white;
                break;

            case (4):
                _render.material.color = Color.red;
                break;

        }
            

    }
}
