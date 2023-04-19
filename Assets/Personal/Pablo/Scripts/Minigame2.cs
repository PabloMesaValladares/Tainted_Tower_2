using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Minigame2 : MonoBehaviour
{
    [SerializeField]
    private Vector3 doorDown, doorUp;

    [SerializeField]
    private float timeFade, timer, maxTimer;

    public UnityEvent plateEngage;
    // Start is called before the first frame update

    public void UpWalls(GameObject w)
    {
        StartCoroutine(startMove(w, new Vector3(w.transform.position.x, doorUp.y, w.transform.position.z), timeFade)); 
    }

    public void DownWalls(GameObject w)
    {
        StartCoroutine(startMove(w, new Vector3(w.transform.position.x, doorDown.y, w.transform.position.z), timeFade));
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent<PlayerController>(out PlayerController _playerController))
        {
            plateEngage.Invoke();
        }
    }


    public static IEnumerator startMove(GameObject door, Vector3 newPos, float duration)
    {
        float currentTime = 0;
        Vector3 start = door.transform.position;
        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            door.transform.position = Vector3.Lerp(start, newPos, currentTime);
            yield return null;
        }
        yield break;
    }
}
