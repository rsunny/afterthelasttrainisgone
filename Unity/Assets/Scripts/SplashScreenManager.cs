using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SplashScreenManager : MonoBehaviour {

	public GameObject[] screens = new GameObject[2];
	public float m_splashscreen_totaltime = 4f;
	private float m_splashscreen_showtime = 0f;
    public Image[] imgs = new Image[2];
    public Color clr;

	void OnEnable() {
		for (int i = 0; i < screens.Length; i++)
			screens [i].SetActive(false);
		
		m_splashscreen_showtime = m_splashscreen_totaltime / screens.Length;
		StartCoroutine (FadeScreens ());
	}

	IEnumerator FadeScreens() {
        float fade;
		for (int i = 0; i < screens.Length; i++) {
            fade = 1;

            screens [i].SetActive(true);
            clr = imgs[i].color;
            int loop = 100;
            float fadedelta = fade / (float)loop;
            fade = 0;

            for (int j = 0; j < loop; j++)
            {
                yield return new WaitForSeconds(fadedelta);
                fade += fadedelta;
                if(j == loop-1){
                    fade = 1;
                } 
                clr.a = fade;
                Debug.Log(clr);
                imgs[i].color = clr;
            }

            yield return new WaitForSeconds(m_splashscreen_showtime);

            fade = 1;
            loop = 300;
            fadedelta = fade / (float)loop;
            for (int j = 0; j < loop; j++)
            {
                yield return new WaitForSeconds(fadedelta);
                fade -= fadedelta;
                clr.a = fade;
                Debug.Log(clr);
                imgs[i].color = clr;
            }
            //img = screens[i].transform.GetChild(0).GetComponent<Image>;
            
			screens [i].SetActive(false);

            yield return null;
		}
		MenuManager.Instance.SwitchToMainMenu ();
	}

	void OnDisable() {
		for (int i = 0; i < screens.Length; i++)
			screens [i].SetActive (false);
	}


}
