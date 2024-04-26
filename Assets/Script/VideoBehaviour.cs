using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;
using UnityEditor;

[RequireComponent(typeof(VideoPlayer))]
public class VideoBehaviour : MonoBehaviour
{
    [SerializeField] private string videoFileName;

    private void Start()
    {
        #if UNITY_STANDALONE_WIN
        PlayerSettings.WebGL.useEmbeddedResources = true;
        #endif
        VideoPlayer video = GetComponent<VideoPlayer>();
        video.loopPointReached += ReturnMenu;
        string videoPath = System.IO.Path.Combine(Application.streamingAssetsPath, videoFileName);
        print(videoPath);
        video.url = videoPath;
        video.Play();
    }

    void ReturnMenu(VideoPlayer vp)
    {
        SceneManager.LoadScene(0);
    }
/*
    void Start()
    {

        GameObject camera = GameObject.Find("Main Camera");

        var videoPlayer = camera.AddComponent<VideoPlayer>();

        videoPlayer.playOnAwake = false;
        videoPlayer.renderMode = VideoRenderMode.CameraNearPlane;

        videoPlayer.targetCameraAlpha = 1F;

        videoPlayer.url = Application.dataPath + "/Resources/" + video.clip.name + ".mp4";

        videoPlayer.isLooping = true;

        videoPlayer.loopPointReached += EndReached;

        videoPlayer.Play();
    }

    void EndReached(VideoPlayer vp)
    {
        SceneManager.LoadScene(0);
    }*/
}
