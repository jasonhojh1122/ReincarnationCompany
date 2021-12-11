using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VideoControl : MonoBehaviour
{
    [SerializeField] UnityEngine.Video.VideoPlayer videoPlayer;
    [SerializeField] string loadSceneName;

    void Start()
    {
        videoPlayer.loopPointReached += VideoEnd;
        videoPlayer.Prepare();
        StartCoroutine(PlayVideo());
    }

    IEnumerator PlayVideo()
    {
        while (!videoPlayer.isPrepared)
        {
            Debug.Log("Not Prepared");
            yield return null;
        }
        videoPlayer.Play();
        Debug.Log("Played");
    }

    void VideoEnd(UnityEngine.Video.VideoPlayer vp)
    {
        vp.Stop();
        GameManager.Instance.LoadSceneAndClose(loadSceneName);
    }
}
