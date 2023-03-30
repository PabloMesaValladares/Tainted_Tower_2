using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using I2.Loc;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private SoundManager _soundManager;
    [SerializeField]
    private GameObject _localizationManager;
    //[SerializeField]
   // private PoolingManager _pollingManager;


    // Start is called before the first frame update
    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
