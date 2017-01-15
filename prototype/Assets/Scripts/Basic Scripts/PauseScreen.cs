using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseScreen : MonoBehaviour {

    // Use this for initialization

    GameObject PausePanel;

    void Start () {
        PausePanel = GameObject.Find("PauseGame");
        PausePanel.SetActive(false);
        Debug.Log("In puase screen");
        Debug.Log(ManageGame.getHealth());
        Debug.Log(ManageGame.getSceneName());
        Debug.Log(ManageGame.getState());
    }

    // Update is called once per frame

    void Update () {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Time.timeScale = 0;
            PausePanel.SetActive(true);
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            ManageGame.setHealth(90);
            ManageGame.setSceneName("FirstLevel");
            ManageGame.setState(1);
            Debug.Log("In collison");
            Debug.Log(ManageGame.getHealth());
            Debug.Log(ManageGame.getSceneName());
            Debug.Log(ManageGame.getState());
            ManageGame.setSceneName("SecondLevel");
            ManageGame.Save();
        }
    }

   public void resume()
    {
        Time.timeScale = 1;
        PausePanel.SetActive(false);
    }

    public void mainmenu()
    {
        SceneManager.LoadScene("Menu");
    }

    public void loadgame()
    {

    }

    public void quit()
    {
        Application.Quit();
    }

    public void instructions()
    {

    }
}
