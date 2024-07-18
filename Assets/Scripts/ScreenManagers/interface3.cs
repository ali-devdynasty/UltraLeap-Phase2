using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class interface3 : MonoBehaviour
{
    public Button back, withdraw, cancel;

    public Text participantId, sessionId;

    public List<ButtonsToGroup> buttons;
    private void Awake()
    {
        back.onClick.AddListener(OnBackButtonClicked);
        withdraw.onClick.AddListener(OnWithdrawClicked);
        cancel.onClick.AddListener(OnCanelClicked);

        participantId.text = DataManager.instance.sessionData.ParticipantId;
        sessionId.text = DataManager.instance.sessionData.SessionId;
    }
    private void OnEnable()
    {
        foreach(var grp in DataManager.instance.groupPlayedStates)
        {
            if(grp != null)
            {
                if(grp.isPlayed)
                {
                    foreach(var btn in buttons)
                    {
                        if(grp.groupNo == btn.groupNo)
                        {
                            btn.groupBtn.interactable = false;
                        }
                    }
                }
            }
        }

        //check if all group played

        bool iscomplete = true;
        foreach (var grp in DataManager.instance.groupPlayedStates)
        {
            if (grp != null)
            {
                if (!grp.isPlayed)
                {
                    iscomplete = false;
                    break;
                }
            }
        }
        if(iscomplete)
        {
            DataManager.instance.onCompleteSession();
            SceneManager.LoadScene(0);
        }
    }
    public void StartGroup(int groupNo)
    {
        int sceneNo = groupNo + 2;

        SceneManager.LoadScene(sceneNo);
    }

    private void OnCanelClicked()
    {
        SceneManager.LoadScene(1);
        DataManager.instance.OnCancelClicked();
    }

    private void OnWithdrawClicked()
    {
        SceneManager.LoadScene(1);
        DataManager.instance.onWithDrawFromSession();
    }

    private void OnBackButtonClicked()
    {
        SceneManager.LoadScene(1);
        DataManager.instance.onWithDrawFromSession();
    }
}
[Serializable]
public class ButtonsToGroup
{
    public Button groupBtn;
    public int groupNo;
}