using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalBossScript : MonoBehaviour
{
    [SerializeField]
    private GameObject[] _enemyLaser;
    [SerializeField]
    private int _health = 70;
    private AudioSource _death;
    private Animator _anim;
    [SerializeField]
    private ParticleSystem[] _explosion, _smokeCloud;
    private Renderer _render;
    private int state;
    [SerializeField]
    private GameObject  _enemyDown;

    // Start is called before the first frame update
    void Start()
    {
        _death = GetComponent<AudioSource>();
        _anim = GetComponent<Animator>();
        _render = GetComponent<MeshRenderer>();
        AudioManager.instance.BossMusic();
    }

    // Update is called once per frame
    void Update()
    {
        FireState();
    }

    public void FireStraight()
    {
        Instantiate(_enemyLaser[state], transform.position, Quaternion.identity);
    }

    public void Damaged()
    {
        Debug.Log("Hit boss");
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
        StartCoroutine(ExplosionDelay());
        _anim.enabled = false;
        UIManager.instance.Victory();
        AudioManager.instance.Victory();
        
    }

    IEnumerator ExplosionDelay()
    {
        for (int i = 0; i < _explosion.Length; i++)
        {
            _explosion[i].Play();
            yield return new WaitForSeconds(0.2f);
        }
    }

    private void FireState()
    {
        switch (
            _health < 15 ? "Quint" :
            _health < 26 ? "Quad" :
            _health < 41 ? "Triple" :
            _health < 51 ? "Double" :
            _health <= 70 ? "Single" :
             "Null")
        {
            case ("Single"):
                state = 0;
                break;
            case ("Double"):
                state = 1;
                _smokeCloud[0].Play();
                _anim.SetTrigger("QuarterDead");
                break;
            case ("Triple"):
                state = 2;
                break;
            case ("Quad"):
                state = 3;
                _smokeCloud[1].Play();
                _anim.SetTrigger("CritHealth");
                break;
            case ("Quint"):
                state = 4;
                break;
        }
    }

    public void SummonAllies()
    {
        Instantiate(_enemyDown, new Vector3(-40, 0, 0), Quaternion.identity);
        AudioManager.instance.Spawn();
    }

}
