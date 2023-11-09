using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ObjectivesManager : MonoBehaviour
{
    public float GameDuration = 600.0f;
    public GameObject WelcomeScreen;
    public GameObject PlayScreen;
    public GameObject WonScreen;
    public GameObject LostScreen;
    public GameObject ResetScreen;

    public TextMeshProUGUI CountdownTMP;
    public TextMeshProUGUI ScoreTMP;
    public TextMeshProUGUI Objective1TMP;
    public TextMeshProUGUI Objective2TMP;
    public TextMeshProUGUI Objective3TMP;
    public TextMeshProUGUI Objective4TMP;
    public TextMeshProUGUI Objective5TMP;

    private int[] _objectivesStatusConfig = new int[5] { 1, 1, 3, 1, 1 }; // never changes
    private int[] _objectivesStatusRT; // real-time : varies depending on the player
    private bool _isPlaying;
    private float _internalCountdown;

    public void Start()
    {
        // let's toggle the screens
        // so we're never confuse by playing play if we missed to deativate/activate one
        WelcomeScreen.SetActive(true);
        ResetScreen.SetActive(false);
        PlayScreen.SetActive(false);
        WonScreen.SetActive(false);
        LostScreen.SetActive(false);

        _objectivesStatusRT = new int[_objectivesStatusConfig.Length];
        _objectivesStatusConfig.CopyTo(_objectivesStatusRT, 0);
        _isPlaying = false;
        _internalCountdown = 0.0f;

        if (WelcomeScreen == null)
        {
            Debug.LogError("Please link the Welcome Screen GO to ObjectivesManager");
            return;
        }

        if (PlayScreen == null)
        {
            Debug.LogError("Please link the Play Screen GO to ObjectivesManager");
            return;
        }

        if (ResetScreen == null)
        {
            Debug.LogError("Please link the Reset Screen GO to ObjectivesManager");
            return;
        }

        if (CountdownTMP == null)
        {
            Debug.LogError("Please link the Countdown TMP to ObjectivesManager");
            return;
        }

        if (Objective1TMP == null || Objective2TMP == null || Objective3TMP == null || Objective4TMP == null || Objective5TMP == null)
        {
            Debug.LogError("Please make sure all Objectives are linked to ObjectivesManager Script to ensure it works.");
            return;
        }
    }

#region Objectives-Specific
    public bool IsValidated(int pObjectiveIndex)
    {
        return _objectivesStatusRT[pObjectiveIndex] == 0;
    }
    public void ValidateObjective_1()
    {
        if (! _isPlaying || IsValidated(0))
        {
            return;
        }

        _objectivesStatusRT[0] = 0;
        UpdateObjectivesGUI();
        CheckWin();
    }

    public void ValidateObjective_2()
    {
        if (!_isPlaying || IsValidated(1))
        {
            return;
        }

        _objectivesStatusRT[1] = 0;
        UpdateObjectivesGUI();
        CheckWin();
    }

    public void ValidateObjective_3()
    {
        if (!_isPlaying || IsValidated(2))
        {
            return;
        }

        if (_objectivesStatusRT[2] - 1 <= 0)
        {
            _objectivesStatusRT[2] = 0;
        }
        else
        {
            _objectivesStatusRT[2]--;
        }

        UpdateObjectivesGUI();
        CheckWin();
    }

    public void ValidateObjective_4()
    {
        if (!_isPlaying || IsValidated(3))
        {
            return;
        }

        _objectivesStatusRT[3] = 0;
        UpdateObjectivesGUI();
        CheckWin();
    }


    public void ValidateObjective_5()
    {
        if (!_isPlaying || IsValidated(4))
        {
            return;
        }

        _objectivesStatusRT[4] = 0;
        UpdateObjectivesGUI();
        CheckWin();
    }
    #endregion

    public void UpdateObjectivesGUI()
    {
        Objective1TMP.text = (_objectivesStatusConfig[0] - _objectivesStatusRT[0]).ToString();
        Objective2TMP.text = (_objectivesStatusConfig[1] - _objectivesStatusRT[1]).ToString();
        Objective3TMP.text = (_objectivesStatusConfig[2] - _objectivesStatusRT[2]).ToString();
        Objective4TMP.text = (_objectivesStatusConfig[3] - _objectivesStatusRT[3]).ToString();
        Objective5TMP.text = (_objectivesStatusConfig[4] - _objectivesStatusRT[4]).ToString();
    }

    public void CheckWin()
    {
        int lNbObjectives = _objectivesStatusRT.Length;
        for (int i = 0; i < lNbObjectives; ++i)
        {
            if (_objectivesStatusRT[i] > 0)
            {
                return;
            }
        }

        GameWon();
    }

    public void Update()
    {
        // not doing anything until it starts
        if (! _isPlaying)
        {
            return;
        }

        _internalCountdown -= Time.deltaTime;

        // Update Countdown to format mm:ss
        CountdownTMP.text = string.Format("{0:00}:{1:00}", 
            Mathf.FloorToInt(_internalCountdown / 60), 
            Mathf.FloorToInt(_internalCountdown % 60));

        // We only handle here GameLost
        // GameWon will be in the validate objectives steps
        if (_internalCountdown <= 0.0f)
        {
            GameLost();
        }
    }

    public void StartGame()
    {
        WelcomeScreen.SetActive(false);
        ResetScreen.SetActive(false);
        PlayScreen.SetActive(true);
        WonScreen.SetActive(false);
        LostScreen.SetActive(false);


        _isPlaying = true;
        _internalCountdown = GameDuration;
        _objectivesStatusRT = new int[_objectivesStatusConfig.Length];
        _objectivesStatusConfig.CopyTo(_objectivesStatusRT, 0);
    }

    public void GameWon()
    {
        ScoreTMP.text = Mathf.Floor(_internalCountdown).ToString();
        WelcomeScreen.SetActive(false);
        PlayScreen.SetActive(false);
        WonScreen.SetActive(true);
        LostScreen.SetActive(false);
        PlayScreen.SetActive(false);
        ResetScreen.SetActive(false);
        _internalCountdown = 0.0f;
        _isPlaying = false;
    }

    public void GameLost()
    {
        WelcomeScreen.SetActive(false);
        PlayScreen.SetActive(false);
        WonScreen.SetActive(false);
        LostScreen.SetActive(true);
        PlayScreen.SetActive(false);
        ResetScreen.SetActive(false);
        _internalCountdown = 0.0f;
        _isPlaying = false;
    }

    public void ResetGame()
    {
        WelcomeScreen.SetActive(true);
        PlayScreen.SetActive(false);
        WonScreen.SetActive(false);
        LostScreen.SetActive(false);
        PlayScreen.SetActive(false);
        ResetScreen.SetActive(false);
        _isPlaying = false;
        _internalCountdown = 0.0f;

        _objectivesStatusRT = new int[_objectivesStatusConfig.Length];
        _objectivesStatusConfig.CopyTo(_objectivesStatusRT, 0);
    }
}
