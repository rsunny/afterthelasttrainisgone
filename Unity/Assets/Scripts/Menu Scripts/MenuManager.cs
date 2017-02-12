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

    private const int idx = 4;
    private GameObject loadScreenFail;
    private GameObject load;
    private GameObject title;

    public GameObject[] levels = new GameObject[idx];
    public GameObject[] levelsBig = new GameObject[idx];
    public GameObject[] levelsButtons = new GameObject[idx];

    public Button levelButton;
    public GameObject gamename;

    private string[] levelname = { "FirstLevel", "MainTerminalVictoria", "FinalLevel" };

    void Start () {
        loadScreenFail = GameObject.Find("LoadFail");
        loadScreenFail.SetActive(false);

        load = GameObject.Find("Load");
        load.SetActive(false);
        leveldeactivate();
        levelactivate();
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
	
	public void SwitchMenuTo(eMenuScreen screen)
    {
        leveldeactivate();
        levelactivate();
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

	public void Play()
    {
        leveldeactivate();
		MusicManager.Instance.StopAll ();
		MusicManager.Instance.PlayMusic ("GameplayMusic");
        ManageGame.setHealth(100);
        ManageGame.setSceneName("Menu");
        ManageGame.setState(0);
        SceneManager.LoadScene("FirstLevel");
    }

    public void level()
    {
        leveldeactivate();
        levelactivate();
    }

    public void levelSelect(int i)
    {
        MusicManager.Instance.StopAll();
        MusicManager.Instance.PlayMusic("GameplayMusic");
        ManageGame.setHealth(100);
        ManageGame.setSceneName(levelname[i]);
        ManageGame.setState(1);
        SceneManager.LoadScene(levelname[i]);
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
            SceneManager.LoadScene(ManageGame.getSceneName());
        }
        else
        {
            StartCoroutine(loadFailScreen());
        }
    }

    private IEnumerator loadFailScreen(){
        
        loadScreenFail.SetActive(true);
        leveldeactivate();

        yield return new WaitForSeconds(3);

        loadScreenFail.SetActive(false);
        title.SetActive(true);
        levelactivate();
    }

    private IEnumerator loadScreen()
    {

        leveldeactivate();
        load.SetActive(true);
        yield return new WaitForSeconds(3);
        load.SetActive(false);
        title.SetActive(true);
        levelactivate();
    }

    public void Exit()
    {
        Application.Quit();
    }

    public void levelactivate()
    {
        for(int i = 0; i < idx; i++)
        {
            levels[i].SetActive(true);
            levelsButtons[i].SetActive(true);
        }

    }

    public void leveldeactivate()
    {
        gamename.SetActive(true);
        for (int i = 0; i < idx; i++)
        {
            levels[i].SetActive(false);
            levelsBig[i].SetActive(false);
            levelsButtons[i].SetActive(false);
        }
    }

}
