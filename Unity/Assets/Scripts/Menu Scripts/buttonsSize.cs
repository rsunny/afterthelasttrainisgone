using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class buttonsSize : MonoBehaviour {

    public GameObject newgame;
    public GameObject newgamebig;

    public GameObject loadgame;
    public GameObject loadgamebig;

    public GameObject instructions;
    public GameObject instructionsbig;

    public GameObject credits;
    public GameObject creditsbig;

    public GameObject exit;
    public GameObject exitbig;

    private const int idx = 4;

    public GameObject[] level = new GameObject[idx];
    public GameObject[] levelBig = new GameObject[idx];


    void Start()
    {
        newgamebig.SetActive(false);
        loadgamebig.SetActive(false);
        instructionsbig.SetActive(false);
        creditsbig.SetActive(false);
        exitbig.SetActive(false);
    }

    public void playenter()
    {
        newgame.SetActive(false);
        newgamebig.SetActive(true);
    }

    public void playexit()
    {
        newgame.SetActive(true);
        newgamebig.SetActive(false);
    }

    public void loadenter()
    {
        loadgame.SetActive(false);
        loadgamebig.SetActive(true);
    }

    public void loadexit()
    {
        loadgame.SetActive(true);
        loadgamebig.SetActive(false);
    }

    public void instructionsenter()
    {
        instructions.SetActive(false);
        instructionsbig.SetActive(true);
    }

    public void instructionsexit()
    {
        instructions.SetActive(true);
        instructionsbig.SetActive(false);
    }

    public void creditsenter()
    {
        credits.SetActive(false);
        creditsbig.SetActive(true);
    }

    public void creditsexit()
    {
        credits.SetActive(true);
        creditsbig.SetActive(false);
    }

    public void exitenter()
    {
        exit.SetActive(false);
        exitbig.SetActive(true);
    }

    public void exitexit()
    {
        exit.SetActive(true);
        exitbig.SetActive(false);
    }

    public void levelenter(int i)
    {
        level[i].SetActive(false);
        levelBig[i].SetActive(true);
    }

    public void levelexit(int i)
    {
        level[i].SetActive(true);
        levelBig[i].SetActive(false);
    }

}
