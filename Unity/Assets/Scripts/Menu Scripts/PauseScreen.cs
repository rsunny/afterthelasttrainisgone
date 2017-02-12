using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class PauseScreen : MonoBehaviour {

    // Use this for initialization

    GameObject PausePanel;
    GameObject loadScreenFail;
    GameObject dummypause;

    void Start () {
        dummypause = GameObject.Find("DummyPause");
        PausePanel = GameObject.Find("PauseScreen");
        loadScreenFail = GameObject.Find("LoadFailPause");
        Debug.Log(dummypause);
        DontDestroyOnLoad(dummypause);
        loadScreenFail.SetActive(false);
        PausePanel.SetActive(false);
    }

    // Update is called once per frame

    void Update () {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Time.timeScale = 0;
            PausePanel.SetActive(true);
            loadScreenFail.SetActive(false);
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            ManageGame.setHealth(90);
            ManageGame.setSceneName("FirstLevel");
            ManageGame.setState(1);
            ManageGame.setSceneName("SecondLevel");
            ManageGame.Save();
        }
    }

   public void resume()
    {
        Time.timeScale = 1;
        PausePanel.SetActive(false);
    }


    public void Play()
    {
        ManageGame.setHealth(100);
        ManageGame.setSceneName("Menu");
        ManageGame.setState(1);
        SceneManager.LoadScene("FirstLevel");
    }

    public void Load()
    {
        if (File.Exists(Application.persistentDataPath + "/FileName.dat"))
        {
            ManageGame.Load();
            SceneManager.LoadScene(ManageGame.getSceneName());
        }
        else
        {
            StartCoroutine(loadFailScreen());
        }
    }

    private IEnumerator loadFailScreen()
    {

        loadScreenFail.SetActive(true);

        yield return new WaitForSeconds(5);

        loadScreenFail.SetActive(false);
    }

    public void Exit()
    {
        Application.Quit();
    }
}
