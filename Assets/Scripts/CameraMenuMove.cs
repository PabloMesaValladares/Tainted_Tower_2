using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CameraMenuMove : MonoBehaviour
{

    [Header("Objects")]
    [SerializeField] private GameObject _camera;
    [SerializeField] private List<GameObject> _cameraPoint;


    [Header("Values")]
    [SerializeField] private int pos, nullPos;
    [SerializeField] private float speed, rotSpeed;
    [SerializeField] private bool moveOn;

    // Start is called before the first frame update
    void Awake()
    {
        pos = nullPos;
    }

    // Update is called once per frame
    void Update()
    {
        if(moveOn)
        {
            
            if (_camera.transform.position != _cameraPoint[pos].transform.position)
            {
                _camera.transform.position = Vector3.MoveTowards(_camera.transform.position, _cameraPoint[pos].transform.position, speed * Time.deltaTime);
                _camera.transform.rotation = Quaternion.Slerp(_camera.transform.rotation, _cameraPoint[pos].transform.rotation, rotSpeed);
            }
            else if(_camera.transform.position == _cameraPoint[pos].transform.position)
            {
                pos = nullPos;
                moveOn = false;
            }
            
        }
    }

    public void MoveToPoint(int i)
    {
        pos = i;
        moveOn = true;
    }
}
