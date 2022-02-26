using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Rendering;
using UnityEngine.Rendering.PostProcessing;


public class UIManager : MonoBehaviour
{
    private static UIManager _instance;
    public static UIManager instance
    {
        get
        {
            if (_instance == null)
            {
                Debug.LogError("UIManager is NULL");
            }
            return _instance;
        }
    }



    [SerializeField]
    private Text _waveNumber;
    [SerializeField]
    private Text _gameover;
    [SerializeField]
    private Text _congrats;
    [SerializeField]
    private Text _finalBoss;
    [SerializeField]
    private GameObject[] _weaponTypes;
    [SerializeField]
    private GameObject[] _lives;
    [SerializeField]
    private Text _score;
    private int _totalScore;
    [SerializeField]
    private GameObject _pauseMenu;

    // Start is called before the first frame update
    void Start()
    {
        _instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void WaveCount(int number)
    {
        StartCoroutine(WaveFlash(number));
    }

    IEnumerator WaveFlash(int number)
    {
        _waveNumber.gameObject.SetActive(true);
        _waveNumber.text = ("WAVE NUMBER: " + number);
        _waveNumber.GetComponent<Text>().color = Color.red;
        yield return new WaitForSeconds(0.5f);
        _waveNumber.GetComponent<Text>().color = Color.blue;
        yield return new WaitForSeconds(0.5f);
        _waveNumber.GetComponent<Text>().color = Color.green;
        yield return new WaitForSeconds(0.5f);
        _waveNumber.GetComponent<Text>().color = Color.yellow;
        yield return new WaitForSeconds(0.5f);
        _waveNumber.gameObject.SetActive(false);
        yield return new WaitForSeconds(1f);
    }

    public void WeaponType(int on, int off, int off2)
    {
        _weaponTypes[on].SetActive(true);
        _weaponTypes[off].SetActive(false);
        _weaponTypes[off2].SetActive(false);
    }

    public void Lives(int total)
    {
        _lives[total].SetActive(false);

    }

    public void Score(int score)
    {
        _totalScore += score;
        _score.text = ("Score: " + _totalScore);
    }

    public void Menu()
    {
        _pauseMenu.SetActive(true);
    }

    public void CloseMenu()
    {
        _pauseMenu.SetActive(false);
    }

    public void Volume(float setting)
    {
        AudioListener.volume = setting;
    }

    public void Lighting(float setting)
    {
        PostColorGrade.instance.Lighting(setting);
    }

    public void GameOver()
    {
        _gameover.gameObject.SetActive(true);
        StartCoroutine(GOBlinkText());
    }

    IEnumerator GOBlinkText()
    {
        while (true)
        {
            _gameover.text = "";
            yield return new WaitForSeconds(0.5f);
            _gameover.text = "GAME OVER";
            yield return new WaitForSeconds(0.5f);
        }
    }

    public void Victory()
    {
        _congrats.gameObject.SetActive(true);
    }

    IEnumerator VictoryBlinkText()
    {
        while (true)
        {
            _congrats.text = "";
            yield return new WaitForSeconds(0.5f);
            _congrats.text = "CONGRATULATIONS\nYOU WIN";
            yield return new WaitForSeconds(0.5f);
        }
    }

    public void FinalBoss()
    {
        _finalBoss.gameObject.SetActive(true);
    }
}
