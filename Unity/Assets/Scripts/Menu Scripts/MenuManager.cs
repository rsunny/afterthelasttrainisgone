using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using POLIMIGameCollective;
using System.IO;

public class MenuManager : Singleton<MenuManager> {

	public GameObject	m_splashscreen;
	public GameObject	m_mainmenu;
	public GameObject	m_settings;
	public GameObject	m_about;

	public enum eMenuScreen {SplashScreen=0, MainMenu=1, Settings=3, About=4};

	[Header("Start with Splashscreen?")]
	public bool m_start_with_splashscreen = true;

	private static bool m_has_shown_splashscreen = false;

    private GameObject loadScreenFail;
    private GameObject load;
    private GameObject title;

    void Start () {
        loadScreenFail = GameObject.Find("LoadFail");
        loadScreenFail.SetActive(false);

        load = GameObject.Find("Load");
        load.SetActive(false);

        title = GameObject.Find("gameName");

        if (!m_has_shown_splashscreen && m_start_with_splashscreen) {
			SwitchMenuTo (eMenuScreen.SplashScreen);
			m_has_shown_splashscreen = true;
		} else {
			SwitchMenuTo (eMenuScreen.MainMenu);
		}
		MusicManager.Instance.StopAll ();
		MusicManager.Instance.PlayMusic ("MenuMusic");
	}
	
	public void SwitchMenuTo(eMenuScreen screen) {
		ClearScreens ();
		switch (screen) {
		case eMenuScreen.SplashScreen:
			if (m_splashscreen != null)
				m_splashscreen.SetActive(true);
			break;
		case eMenuScreen.MainMenu:
                if (m_mainmenu != null)
                {
                    m_mainmenu.SetActive(true);

                }
			break;
		case eMenuScreen.Settings:
			if (m_settings!=null) 
				m_settings.SetActive(true);
			break;
		case eMenuScreen.About:
			if (m_about!=null) 
				m_about.SetActive(true);
			break;
		}
	}
    
	void ClearScreens() {
		if (m_splashscreen!=null) 
			m_splashscreen.SetActive(false);
		if (m_mainmenu!=null) 
			m_mainmenu.SetActive(false);
		if (m_settings!=null) 
			m_settings.SetActive(false);
		if (m_about!=null) 
			m_about.SetActive(false);
	}

	public void SwitchToMainMenu() {
		SwitchMenuTo (eMenuScreen.MainMenu);
	}

	public void SwitchToAbout() {
        title.SetActive(true);
		SwitchMenuTo (eMenuScreen.About);
	}

	public void SwitchToSettings()
    {
        title.SetActive(true);
        SwitchMenuTo (eMenuScreen.Settings);
	}

	public void Play() {
        Debug.Log("In play");
		MusicManager.Instance.StopAll ();
		MusicManager.Instance.PlayMusic ("GameplayMusic");
        ManageGame.setHealth(100);
        ManageGame.setSceneName("Menu");
        ManageGame.setState(1);
        title.SetActive(false);
        SceneManager.LoadScene("FirstLevel");
    }


    public void Load()
    {
        if (File.Exists(Application.persistentDataPath + "/FileName.dat"))
        {
            MusicManager.Instance.StopAll();
            MusicManager.Instance.PlayMusic("GameplayMusic");
            StartCoroutine(loadScreen());
            StartCoroutine(loadScreen());
            StartCoroutine(loadScreen());
            ManageGame.Load();
            Debug.Log("In Load");
            Debug.Log(ManageGame.getHealth());
            Debug.Log(ManageGame.getSceneName());
            Debug.Log(ManageGame.getState());
            SceneManager.LoadScene(ManageGame.getSceneName());
        }
        else
        {
            Debug.Log("Load Fail");
            StartCoroutine(loadFailScreen());
        }
    }

    private IEnumerator loadFailScreen(){

        title.SetActive(false);
        loadScreenFail.SetActive(true);
        
        yield return new WaitForSeconds(3);

        loadScreenFail.SetActive(false);
        title.SetActive(true);
    }

    private IEnumerator loadScreen()
    {

        title.SetActive(false);
        load.SetActive(true);
        Debug.Log("Iam here in loadscreen");
        yield return new WaitForSeconds(3);

        load.SetActive(false);
        title.SetActive(true);
    }

    public void Exit()
    {
        Application.Quit();
    }

}
