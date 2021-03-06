﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class ResultManager : MonoBehaviour
{

    public enum State
    {
        none,
        result,
        askEnd,
        finish
    }
    public enum PanelState
    {
        result,
        ranking,
    }
    State m_state;
    PanelState panelState;
    bool isMessagePanelActive;
    [SerializeField] ResultPanel result;
    [SerializeField] Animator messagePanel;
    [SerializeField] RankingPanel ranking;
    [SerializeField] Animator animator;
    [SerializeField] SelectButtonMgr buttonMgr;
    private float time;

    // Start is called before the first frame update
    void Awake()
    {
        isMessagePanelActive = false;
        messagePanel.gameObject.SetActive(false);
        result.canScroll = true;
        ChangePanel(PanelState.result);
        time = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time - time < 1)
            return;
        if (Player.InputSystem.GetKeyDown(KeyCode.B))
        {
            //メッセージパネルがあるならスクロール操作をしない
            isMessagePanelActive ^= true;
            messagePanel.gameObject.SetActive(isMessagePanelActive);
            if (isMessagePanelActive)
                buttonMgr.BackButton.Select();

            if (panelState == PanelState.result)
            {
                result.canScroll = !isMessagePanelActive;
                if (!isMessagePanelActive)
                    result.GetSrollbar.Select();
            }
            else if (panelState == PanelState.ranking)
            {
                ranking.canScroll = !isMessagePanelActive;
                if (!isMessagePanelActive)
                    ranking.GetSrollbar.Select();

            }
        }

        if (isMessagePanelActive)
            return;
        if (Player.InputSystem.GetKeyDown(KeyCode.R) || Player.InputSystem.GetKeyDown(KeyCode.RightArrow))
            ChangePanel(PanelState.ranking);
        if (Player.InputSystem.GetKeyDown(KeyCode.L) || Player.InputSystem.GetKeyDown(KeyCode.LeftArrow))
            ChangePanel(PanelState.result);

    }


    /// <summary>
    /// 釣ゲームから遷移した場合に成果を登録する
    /// 自動でresultPanel,rankingPanelのdebugをfalseにする
    /// </summary>
    /// <param name="fishList"></param>
    public void SetList(List<Fish.FishInfo> fishList)
    {
//        Debug.Log("SetList");
        result.debug = false;
        ranking.debug = false;
        //リザルトの更新
        result.UpdateResult(fishList);
        //ランキングの更新
        RankingSaveData.Record record;
        record.name = SaveManager.Instance.playerName;
        record.icon = SaveManager.Instance.playerIcon;
        record.fishCount = fishList.Count;
        int sum = 0;
        record.score = 0;
        foreach (var x in fishList)
            sum += x.sellingPrice;
        record.score = sum;
        ranking.UpdateRanking(record);
    }
    public void ActivateMessage()
    {
        messagePanel.gameObject.SetActive(true);

    }

    void ChangePanel(PanelState state)
    {
        isMessagePanelActive = false;
        //ランキングー＞リザルト
        if (state == PanelState.result && panelState == PanelState.ranking)
        {
            panelState = PanelState.result;
            //リザルトのアニメーション
            animator.SetTrigger("ToResult");

            result.canScroll = true;
            ranking.canScroll = false;
        }
        //リザルト->ランキング
        else if (state == PanelState.ranking && panelState == PanelState.result)
            {
            panelState = PanelState.ranking;

            //ランキングのアニメーション
            animator.SetTrigger("ToRanking");

            ranking.canScroll = true;
            result.canScroll = false;
        }
    }

    public void ToStageSelect()
    {
        SaveManager.Instance.Save();
        Destroy(FindObjectOfType<Player.CoolerBox>().gameObject);
        SceneManager.LoadScene("StageSelect");
        SaveManager.Instance.AddWeek();
    }

    public void ToTitle()
    {
        SaveManager.Instance.Save();
        Destroy(FindObjectOfType<Player.CoolerBox>().gameObject);
        StageSelectMgr.SetTitleActive();
        SceneManager.LoadScene("StageSelect");
        SaveManager.Instance.AddWeek();
    }

    public void SelectRankingData(RankingSaveData data)
    {
        ranking.rankingData = data;
    }
}
