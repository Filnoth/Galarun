using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private static AudioManager _instance;
    public static AudioManager instance
    {
        get
        {
            if (_instance == null)
            {
                Debug.LogError("AudioManager is NULL");
            }
            return _instance;
        }
    }

    [SerializeField]
    private AudioSource _upgrade, _playerDeath, _death, _backgroundMusic, _bossMusic, _victory, _laser, _hit, _spawn, _playerHit;
    

    private void Awake()
    {
        _instance = this;
        _backgroundMusic.Play();
    }

    public void PowerUp()
    {
        _upgrade.Play();
    }

    public void Death()
    {
        _death.Play();
    }

    public void PlayerDeath()
    {
        _playerDeath.Play();
    }

    public void Victory()
    {
        _victory.Play();
        _backgroundMusic.Stop();
        _bossMusic.Stop();
    }

    public void BossMusic()
    {
        _bossMusic.Play();
        _backgroundMusic.Stop();
    }
   
    public void Laser()
    {
        _laser.Play();
    }

    public void Hit()
    {
        _hit.Play();
    }

    public void Spawn()
    {
        _spawn.Play();
    }

    public void PlayerHit()
    {
        _playerHit.Play();
    }
}
