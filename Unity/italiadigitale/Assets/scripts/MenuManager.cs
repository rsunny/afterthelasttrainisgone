using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine.Events;

public class MenuManager : MonoBehaviour
{

    public GameObject m_play;
    public GameObject m_credits;
    public GameObject m_exit;
    public GameObject m_mainmenu;
    public GameObject m_newsfeed;
    public GameObject m_creditfeed;
    public GameObject m_creditbutton;

    void Start()
    {
        m_mainmenu.SetActive(true);
        m_newsfeed.SetActive(false);
        m_play.SetActive(true);
        m_credits.SetActive(true);
        m_exit.SetActive(true);
        m_creditfeed.SetActive(false);
        m_creditbutton.SetActive(false);
    }

    public void PlayButton()
    {
        m_mainmenu.SetActive(false);
        m_newsfeed.SetActive(true);
    }

    public void GoBackButton()
    {
        m_mainmenu.SetActive(true);
        m_newsfeed.SetActive(false);
    }

    public void CreditGoBackButton()
    {
        m_play.SetActive(true);
        m_credits.SetActive(true);
        m_exit.SetActive(true);
        m_creditfeed.SetActive(false);
        m_creditbutton.SetActive(false);
    }

    public void CreditsButton()
    {
        m_play.SetActive(false);
        m_credits.SetActive(false);
        m_exit.SetActive(false);
        m_creditfeed.SetActive(true);
        m_creditbutton.SetActive(true);
    }

    public void ExitButton()
    {
        Debug.Log("In exit");
        Application.Quit();
    }

    public void MysteryButton()
    {
        SceneManager.LoadScene("Article");
    }

}
