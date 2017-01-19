using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FightManager : MonoBehaviour {

	public GameObject m_enemy;
	public GameObject m_player;

	//GUI styles
	public GUIStyle m_guiStyle;
	public GUIStyle m_guiStyle2;
	public GUIStyle m_guiStyleHealth;
	public GUIStyle m_guiStyleHealth2;

	//gui boolean
	private bool m_display1 = false ;
	private bool m_display2 = false ;
	private bool m_display3 = false ;
	private bool m_display4 = false ;

	public GameObject m_enemyHealthGO;
	public GameObject m_playerHealthGO;
	private int m_enemyHealth;
	private int m_playerHealth;

	private bool m_playerLose = false;
	private bool m_playerWin = false;


	public string m_sceneToLoadOnLose;


	// Use this for initialization
	void Start () {
		StartCoroutine (StartCoroutine ());		
	}
	
	// Update is called once per frame
	void Update () {
		if (m_enemyHealthGO != null) {
			m_enemyHealth = Mathf.Max(m_enemyHealthGO.GetComponent<Health> ().m_health,0);
		}
		if (m_playerHealthGO != null) {
			m_playerHealth = Mathf.Max(m_playerHealthGO.GetComponent<Health> ().m_health,0);
		}
		if (m_playerHealth <= 0 && !m_playerLose) {
			StartCoroutine(PlayerLose ());
		}
		if (m_enemyHealth <= 0 && !m_playerWin) {
			StartCoroutine(PlayerWin ());
		}
	}

	private IEnumerator PlayerLose(){
		m_player.GetComponent<PlayerManager> ().ManagerStop (true);
		m_enemy.GetComponent<PlayerManager> ().ManagerStop (true);
		m_enemy.GetComponent<MonsterBasicMove> ().StopPlayer();
		m_playerLose = true;
		yield return new WaitForSeconds (4);
		SceneManager.LoadScene (m_sceneToLoadOnLose);
	}

	private IEnumerator PlayerWin(){
		m_enemy.GetComponent<PlayerManager> ().ManagerStop (true);
		m_enemy.GetComponent<MonsterBasicMove> ().StopPlayer();
		m_playerWin = true;
		StateManager.updateState();
		yield return new WaitForSeconds (0);
	}
		
	private IEnumerator StartCoroutine(){
		m_enemy.GetComponent<PlayerManager> ().ManagerStop (true);
		m_enemy.GetComponent<MonsterBasicMove> ().StopPlayer();
		m_player.GetComponent<PlayerManager> ().ManagerStop (true);
		float time = 1.4f;
		yield return new WaitForSeconds (time);
		m_display1 = true;
		m_display2 = false;
		m_display3 = false;
		m_display4 = false;
		yield return new WaitForSeconds (time);
		m_display1 = false;
		m_display2 = true;
		m_display3 = false;
		m_display4 = false;
		yield return new WaitForSeconds (time);
		m_display1 = false;
		m_display2 = false;
		m_display3 = true;
		m_display4 = false;
		yield return new WaitForSeconds (time);
		m_display1 = false;
		m_display2 = false;
		m_display3 = false;
		m_display4 = true;
		m_enemy.GetComponent<PlayerManager> ().ManagerStop (false);
		m_enemy.GetComponent<MonsterBasicMove> ().UnstopPlayer();
		m_player.GetComponent<PlayerManager> ().ManagerStop (false);
		yield return new WaitForSeconds (time);
		m_display1 = false;
		m_display2 = false;
		m_display3 = false;
		m_display4 = false;
	}


	private void OnGUI() {
		GUI.Label (new Rect (Screen.width/20, 50, 100, 50), m_enemyHealth.ToString(), m_guiStyleHealth2);
		GUI.Label (new Rect (Screen.width/20, 50, 100, 50), m_enemyHealth.ToString(), m_guiStyleHealth);
		GUI.Label (new Rect (Screen.width - Screen.width/10, 50, 100, 50), m_playerHealth.ToString(), m_guiStyleHealth2);
		GUI.Label (new Rect (Screen.width - Screen.width/10, 50, 100, 50), m_playerHealth.ToString(), m_guiStyleHealth);
		string str;
		if (m_display1) {
			str = "1";
			GUI.Label (new Rect (Screen.width / 2 - 50, 50, 100, 50), str, m_guiStyle2);
			GUI.Label (new Rect (Screen.width / 2 - 50, 50, 100, 50), str, m_guiStyle);
		}
		if (m_display2) {
			str = "2";
			GUI.Label (new Rect (Screen.width / 2 - 50, 50, 100, 50), str, m_guiStyle2);
			GUI.Label (new Rect (Screen.width / 2 - 50, 50, 100, 50), str, m_guiStyle);

		}
		if (m_display3) {
			str = "3";
			GUI.Label (new Rect (Screen.width / 2 - 50, 50, 100, 50), str, m_guiStyle2);
			GUI.Label (new Rect (Screen.width / 2 - 50, 50, 100, 50), str, m_guiStyle);

		}
		if (m_display4) {
			str = "FIGHT !";
			GUI.Label (new Rect (Screen.width / 2 - 50, 50, 100, 50), str, m_guiStyle2);
			GUI.Label (new Rect (Screen.width / 2 - 50, 50, 100, 50), str, m_guiStyle);

		}
		if (m_playerLose) {
			str = "Game Over !!";
			GUI.Label (new Rect (Screen.width / 2 - 50, 50, 100, 50), str, m_guiStyle2);
			GUI.Label (new Rect (Screen.width / 2 - 50, 50, 100, 50), str, m_guiStyle);
		}
		if (m_playerWin && !m_playerLose) {
			str = "You won !!";
			GUI.Label (new Rect (Screen.width / 2 - 50, 50, 100, 50), str, m_guiStyle2);
			GUI.Label (new Rect (Screen.width / 2 - 50, 50, 100, 50), str, m_guiStyle);
		}
	}

}
