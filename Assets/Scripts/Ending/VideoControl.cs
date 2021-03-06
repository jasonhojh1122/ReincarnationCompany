using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VideoControl : MonoBehaviour
{
    [SerializeField] UnityEngine.Video.VideoPlayer videoPlayer;
    [SerializeField] string loadSceneName;

    void Awake()
    {
        videoPlayer.loopPointReached += VideoEnd;
        // videoPlayer.Play();
        // videoPlayer.Prepare();
        // StartCoroutine(PlayVideo());
    }

    IEnumerator PlayVideo()
    {
        while (!videoPlayer.isPrepared)
        {
            yield return null;
        }
        videoPlayer.Play();
    }

    public void VideoEnd(UnityEngine.Video.VideoPlayer vp)
    {
        vp.Pause();
        GameManager.Instance.LoadSceneAndClose(loadSceneName);
    }
}
