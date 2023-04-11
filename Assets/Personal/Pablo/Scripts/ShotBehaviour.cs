using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotBehaviour : MonoBehaviour
{
    [SerializeField] private List<GameObject> _bullets;
    [SerializeField] private List<bool> _bulletsActivated;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public GameObject EnableBullet()
    {
        for(int i = 0; i < _bullets.Count; i++)
        {
            if(_bulletsActivated[i] == false)
            {
                _bulletsActivated[i] = true;
                _bullets[i].SetActive(true);
                return _bullets[i];
            }
        }

        return null;
    }
}
