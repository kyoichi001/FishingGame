﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace FishingGame
{

    public class FishingGameMgr : SingletonMonoBehaviour<FishingGameMgr>
    {
        static private FishingGame.Tools.Hook m_fishingHook;
        static public FishingGame.Tools.Hook Hook
        {
            get
            {
                //釣り針の取得
                if (m_fishingHook == null)
                    m_fishingHook = GameObject.FindGameObjectWithTag("Hook").GetComponent<FishingGame.Tools.Hook>();
                return m_fishingHook;
            }
        }

        static private FishingToolMgr m_fishingToolMgr;
        static public FishingToolMgr fishingToolMgr
        {
            get
            {
                //釣り針の取得
                if (m_fishingToolMgr == null)
                    m_fishingToolMgr = Transform.FindObjectOfType<FishingToolMgr>();
                return m_fishingToolMgr;
            }
        }

        private Player.Player player { get { return fishingToolMgr.player; } }//釣りゲームが開始したときのみ他の操作の禁止をする

        [Header("References")]
        [SerializeField] private Fish.FishGenerator fishGenerator;
        [SerializeField] private CommandGenerator commandGenerator;
        [SerializeField] private Player.InputSystem input;

        [SerializeField, Tooltip("コマンドが生成されるまでの最低時間(tick)")] int attackTimeMin=100;

        [Tooltip("今狙っている魚"),ReadOnly]
        [SerializeField]
        private Fish.Behavior.CommonFish m_targetFish;
        public Fish.Behavior.CommonFish TargetFish
        {
            get { return m_targetFish; }
        }
        [ReadOnly]
        public bool canAttack = false;
        


        [Header("When Fishing starts"), SerializeField, Tooltip("釣りゲームが始まったときに呼び出す関数")]
        UnityEvent WhenFishingStart;


        [Header("When Fishing is successful"), SerializeField,Tooltip("釣りが成功したときに呼び出す関数")]
        UnityEvent WhenFishingSucceeded;

        [Header("When Fishing fails "), SerializeField,Tooltip("釣りが失敗したときに呼び出す関数")]
        UnityEvent WhenFishingFailed;

        
        /// <summary>
        /// 釣りゲーム中(コマンドバトル)しているか
        /// </summary>
        public bool isFishing { get { return m_isFishing; } }
        bool m_isFishing = false;

        /// <summary>
        /// 攻撃の間隔をあけるためのタイマー
        /// </summary>
        int attackTimer=0;

        /// <summary>
        /// 魚が針を狙い始めた
        /// </summary>
        public void StartApproaching()
        {
            
        }


        /// <summary>
        /// 釣りゲームが始まったときに呼び出す関数
        /// </summary>
        public void StartFishing(Fish.Behavior.CommonFish target)
        {
            if (isFishing)
            {
                Debug.LogError("Fishing is already started");

                return;
            }
            Hook.OnBiteHook();
            WhenFishingStart.Invoke();
            m_targetFish = target;

            m_isFishing = true;
            canAttack = true;
            attackTimer = attackTimeMin *3 /4;

            player.StartFishing();
        }

        /// <summary>
        /// 釣りが成功したときに呼び出す
        /// </summary>
        public void FishingSucceeded()
        {
            WhenFishingSucceeded.Invoke();
            m_isFishing = false;
            fishingToolMgr.CatchFish(TargetFish);
            Hook.FinishBite();
        }

        /// <summary>
        /// 釣りが失敗したときに呼び出す
        /// </summary>
        public void FishingFailed()
        {
            WhenFishingFailed.Invoke();
            m_isFishing = false;
            fishingToolMgr.RetrieveTools();
            Hook.FinishBite();
            m_targetFish.SetEscaping();
        }

        new private void Awake()
        {
            base.Awake();
            if (WhenFishingFailed == null)
                WhenFishingFailed = new UnityEvent();
            if (WhenFishingSucceeded == null)
                WhenFishingSucceeded = new UnityEvent();
            if (WhenFishingStart == null)
                WhenFishingStart = new UnityEvent();
        }

        // Update is called once per frame
        void Update()
        {
            if (isFishing)
            {
                Fishing();
            }





            ////釣り具の動作
            //if (input.RightClicked() )
            //{
            //   // fishingToolMgr.PullToRight();
            //}
            //else if (input.LeftClicked())
            //{
            //   // fishingToolMgr.PullToLeft();
            //}
        

        }

        /// <summary>
        /// 釣りゲーム中の処理
        /// </summary>
        void Fishing()
        {
            if(++attackTimer > attackTimeMin && canAttack)
            {
                canAttack = false;
                attackTimer = 0;
                commandGenerator.Generate();
            }
        }

        

    }
}