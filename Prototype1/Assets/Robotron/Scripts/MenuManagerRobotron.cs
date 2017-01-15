using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class MenuManagerRobotron : MonoBehaviour {

	public GameObject m_mainmenu;
	public GameObject m_credits;
	public GameObject m_settings;

	void Start() {
		m_mainmenu.SetActive (true);
		m_credits.SetActive (false);
		m_settings.SetActive (false);
	}

	public void PlayButton() {
		SceneManager.LoadScene ("Gameplay");
	}

	public void CreditsButton() {
		m_mainmenu.SetActive (false);
		m_credits.SetActive (true);
		m_settings.SetActive (false);
		//SceneManager.LoadScene ("Gameplay");
	}

	public void BackButton() {
		m_mainmenu.SetActive (true);
		m_credits.SetActive (false);
		m_settings.SetActive (false);
		//SceneManager.LoadScene ("Gameplay");
	}

}
