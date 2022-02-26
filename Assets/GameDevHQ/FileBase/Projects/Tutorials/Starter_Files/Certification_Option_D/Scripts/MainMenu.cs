using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Rendering;
using UnityEngine.Rendering.PostProcessing;

public class MainMenu : MonoBehaviour
{
    [SerializeField]
    private GameObject _options;

    // Start is called before the first frame update
    void Start()
    {
        
    }


    // Update is called once per frame
    void Update()
    {

    }

    public void StartGame()
    {
        SceneManager.LoadScene("Main_Game");
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void Options()
    {
        _options.SetActive(true);
    }

    public void CloseOptions()
    {
        _options.SetActive(false);
    }

    public void Volume(float setting)
    {
        AudioListener.volume = setting;
    }

    public void Lighting(float setting)
    {
        PostColorGrade.instance.Lighting(setting);
    }
}
