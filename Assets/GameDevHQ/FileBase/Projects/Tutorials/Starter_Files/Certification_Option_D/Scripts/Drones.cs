using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drones : MonoBehaviour
{
    [SerializeField]
    private GameObject _laserPrefab;
    Vector3 _laserOffset = new Vector3(1.3f, 0, 0);
    private float _canFire = -1f;
    private float _fireRate = 0.25f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       
    }

}
