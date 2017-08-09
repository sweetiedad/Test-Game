using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System;
using UnityEngine.SceneManagement;

public class GodObject : MonoBehaviour
{
    [SerializeField]
    List<Hamster> Hamsters;
    [SerializeField]
    float StopAllHamstersTime;
    [SerializeField]
    List<GameObject> LooseObject;
    [SerializeField]
    Text Score;
    [SerializeField]
    Text LoseScore;
    [SerializeField]
    GameObject StartTextContainer;
    [SerializeField]
    List<Text> StartText;
    float startTimer;
    float timer;
    bool StartRoundGame;
    int record;
    enum StateTypeText
    {
        STATE_START,
        STATE_READY,
        STATE_SET,
        STATE_GO,
        STATE_OFF_ALL
    }
    StateTypeText StateStartText;

    void Start()
    {
        OffOnHamsters(false);
        for (int i = 0; i < StartText.Count; i++)
        {
            StartText[i].gameObject.SetActive(false);
        }
        StateStartText = StateTypeText.STATE_START;
    }

    void Update()
    {
        CheckStateStartText();
        StartGO();
        RoundScore();
    }

    void CheckStateStartText()
    {
        if (StateStartText == StateTypeText.STATE_START)
        {
            startTimer += Time.deltaTime;
            if (startTimer >= 1)
            {
                StartText[0].gameObject.SetActive(true);
                StateStartText = StateTypeText.STATE_READY;
                startTimer = 0;
            }
        }
        else if (StateStartText == StateTypeText.STATE_READY)
        {
            startTimer += Time.deltaTime;
            if (startTimer >= 1)
            {
                StartText[0].gameObject.SetActive(false);
                StartText[1].gameObject.SetActive(true);
                StateStartText = StateTypeText.STATE_SET;
                startTimer = 0;
            }
        }
        else if (StateStartText == StateTypeText.STATE_SET)
        {
            startTimer += Time.deltaTime;

            if (startTimer >= 1)
            {
                StartText[1].gameObject.SetActive(false);
                StartText[2].gameObject.SetActive(true);
                StateStartText = StateTypeText.STATE_GO;
                startTimer = 0;
            }
        }
        else if (StateStartText == StateTypeText.STATE_GO)
        {
            startTimer += Time.deltaTime;
            if (startTimer >= 1)
            {
                StartTextContainer.SetActive(false);
                StateStartText = StateTypeText.STATE_OFF_ALL;
                startTimer = 0;
            }
        }
        else if (StateStartText == StateTypeText.STATE_OFF_ALL)
        {
            StartRoundGame = true;
        }
    }

    void StartGO()
    {
        if (StartRoundGame == true)
        {
            OffOnHamsters(true);
            timer += Time.deltaTime;
            if (timer >= StopAllHamstersTime)
            {
                int hamstersDown = 0;
                for (int i = 0; i < Hamsters.Count; i++)
                {
                    Hamsters[i].DownAndStopHamsters();
                }
                for (int i = 0; i < Hamsters.Count; i++)
                {
                    if (Hamsters[i].transform.position.y <= Hamsters[i].Bottom.position.y)
                    {
                        hamstersDown += 1;
                    }
                }
                if (hamstersDown == Hamsters.Count)
                {
                    GameEndAndLoosePanel(true);
                }
            }
        }
    }

    public void ResetHamsters()
    {
        timer = 0;
        Points.GetInstance().point = 0;
        StartRoundGame = false;
        StateStartText = StateTypeText.STATE_START;
        StartTextContainer.SetActive(true);
        for (int i = 0; i < StartText.Count; i++)
        {
            StartText[i].gameObject.SetActive(false);
        }
        if (StateStartText == StateTypeText.STATE_OFF_ALL)
        {
            for (int i = 0; i < Hamsters.Count; i++)
            {
                Hamsters[i].ResetHamster();
            }
        }
        GameEndAndLoosePanel(false);
    }

    public void ExitHamster()
    {

        if (PlayerPrefs.GetInt("record") < Points.GetInstance().point)
        {
            record = Points.GetInstance().point;
            PlayerPrefs.SetInt("record", record);
            PlayerPrefs.Save();
        }
        Points.GetInstance().point = 0;
        timer = 0;
        SceneManager.LoadScene("greeting");

    }

    void GameEndAndLoosePanel(bool a)
    {
        foreach (GameObject resetOb in LooseObject)
        {
            resetOb.SetActive(a);
        }
        this.enabled = !a;
        LoseScore.text = ("YOU SCORE:" + Points.GetInstance().point);

        if (PlayerPrefs.GetInt("record") < Points.GetInstance().point)
        {
            record = Points.GetInstance().point;
            PlayerPrefs.SetInt("record", record);
            PlayerPrefs.Save();
        }
    }

    void RoundScore()
    {
        Score.text = Convert.ToString("Score: " + Points.GetInstance().point);
    }

    void OffOnHamsters(bool a)
    {
        for (int i = 0; i < Hamsters.Count; i++)
        {
            Hamsters[i].enabled = a;
        }
    }
}



