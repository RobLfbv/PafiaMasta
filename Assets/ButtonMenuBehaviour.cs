using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonMenuBehaviour : MonoBehaviour
{
    [SerializeField]
    private Button[] buttons;
    void Start()
    {
        buttons[0].Select();
    }

    public void Play()
    {
        print(SceneManager.GetAllScenes().Length);
        SceneManager.SetActiveScene(SceneManager.GetSceneByBuildIndex(1));
    }
}
