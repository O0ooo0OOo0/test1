using System.Collections;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartVideoPlay : MonoBehaviour
{
    public VideoPlayer videoPlayer;
    public Button skip;
    public GameObject black;
    public float fadeOutSpeed = 1f;

    private void Start()
    {
        black.SetActive(false);

        if (skip != null)
        {
            skip.onClick.AddListener(SkipVideo);
        }

        // 设置视频播放参数
        videoPlayer.loopPointReached += EndReached; // 设置视频播放结束时的回调
        videoPlayer.Play(); 
    }

    private void EndReached(VideoPlayer source)
    {
        SceneManager.LoadScene("1-1");
    }

    public void SkipVideo()
    {
        StartCoroutine(ExitScene());
    }

    IEnumerator ExitScene()
    {
        black.SetActive(true);
        float alpha = 0f;
        while (alpha < 1f)
        {
            alpha += Time.deltaTime * fadeOutSpeed;
            black.GetComponent<Image>().color = new Color(0, 0, 0, alpha);
            yield return null;
        }
        alpha = 1f;
        black.GetComponent<Image>().color = new Color(0, 0, 0, alpha);
        videoPlayer.Stop();
        SceneManager.LoadScene("1-1");
    }
}