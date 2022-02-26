using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class PostColorGrade : MonoBehaviour
{
    private static PostColorGrade _instance;
    public static PostColorGrade instance
    {
        get
        {
            if (_instance == null)
            {
                Debug.LogError("Colorgrade is NULL");
            }
            return _instance;
        }
    }

    ColorGrading _colorGrade = null;

    private void Start()
    {
        _instance = this;
        PostProcessVolume _volume = gameObject.GetComponent<PostProcessVolume>();
        _volume.profile.TryGetSettings(out _colorGrade);
    }

    public void Lighting(float settings)
    {
        _colorGrade.enabled.value = true;
        _colorGrade.postExposure.value = settings;
    }
}
