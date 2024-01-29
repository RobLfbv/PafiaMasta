using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class VideoBehaviour : MonoBehaviour
{
    public VideoPlayer video;

    private void Start()
    {
        video.loopPointReached += ReturnMenu;
    }

    void ReturnMenu(VideoPlayer vp)
    {
        SceneManager.LoadScene(0);
    }
}
