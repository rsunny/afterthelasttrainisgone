using UnityEngine;
using System.Collections;

public class SoundManager : MonoBehaviour {


	public static SoundManager Instance = null;

	public AudioSource m_background_music;

	[Header("Player Sound Effects")]
	public AudioSource m_player_effects;
	public AudioClip m_shoot_audioclip;
	public AudioClip m_enemy_dies_audioclip;
    public AudioClip m_player_dies_audioclip;

	[Header("background music")]
	public AudioClip m_menu_background;

	void Awake() {
		if (Instance == null) {
			Instance = this;
			DontDestroyOnLoad (gameObject);
		} else {
			Destroy (gameObject);
		}
	}

	void Start() {
		m_background_music.clip = m_menu_background;
		m_background_music.Play ();
	}

	public void PlayerShoots() {
		m_player_effects.PlayOneShot (m_shoot_audioclip);
	}
		

    public void PlayerDies()
    {
        m_player_effects.PlayOneShot (m_player_dies_audioclip);
    }

	public void EnemyDies()
	{
		m_player_effects.PlayOneShot (m_enemy_dies_audioclip);
	}
}
