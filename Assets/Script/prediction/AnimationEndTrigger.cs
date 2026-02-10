using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class AnimationEndTrigger : MonoBehaviour
{
    public bool iscanBeShow;
    //public bool isRunning;

    private void Start()
    {
        iscanBeShow = false;
        //isRunning = true;
    }

    public void OnAnimationEnd(int isShow)
    {
        if (isShow == 1)
        {
            iscanBeShow = true;
            //StartCoroutine(DelayAction(0.2f));
            //isRunning = false;
        }
        else if (isShow == 0)
        {
            iscanBeShow = false;
            //StartCoroutine(DelayAction(0.2f));
            //isRunning = true;
        }

    }

    IEnumerator DelayAction(float delay)
    {
        yield return new WaitForSeconds(delay);
    }
}