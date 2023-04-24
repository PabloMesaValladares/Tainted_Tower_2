using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProgressBar : MonoBehaviour
{
    public RectTransform Bar;
    public void SetAsyncOperation(AsyncOperation operation)
    {
        StartCoroutine(UpdateProgessBar(operation));
    }
    private IEnumerator UpdateProgessBar(AsyncOperation operation)
    {
        while (!operation.isDone)
        {
            Bar.anchorMax = new Vector2(operation.progress, Bar.anchorMax.y);
            yield return null;
        }
    }
}