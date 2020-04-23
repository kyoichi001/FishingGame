﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    public class Player : MonoBehaviour
    {
        public enum State
        {
             None,
             Normal,
             ThrowRod,

        }

        FishingGame.FishingToolMgr fishingToolMgr;

        public float speed=1;

        [SerializeField,ReadOnly]State m_state = State.None;
        /// <summary>
        /// 移動可能か
        /// </summary>
        [ReadOnly]bool canMove = true;

        // Start is called before the first frame update
        void Start()
        {
            m_state = State.Normal;
            fishingToolMgr = FishingGame.FishingGameMgr.fishingToolMgr;
        }

        // Update is called once per frame
        void Update()
        {

            InputUpdate();
        }

        void InputUpdate()
        {
            if (Input.GetKeyDown(KeyCode.A))
            {
                if (m_state == State.Normal)
                {
                    ThrowRod();

                }
                else if (m_state == State.ThrowRod)
                {
                    RetrieveRod();
                }
            }
            if (!canMove) return;

            if (Input.GetKey(KeyCode.LeftArrow))
            {
                transform.position += new Vector3(-speed, 0f, 0f) * Time.deltaTime;
            }
            if (Input.GetKey(KeyCode.RightArrow))
            {
                transform.position += new Vector3(speed, 0f, 0f) * Time.deltaTime;
            }

            
        }

        /// <summary>
        /// 釣り竿を振り海に糸を垂らす
        /// </summary>
        public void ThrowRod()
        {
            m_state = State.ThrowRod;
            fishingToolMgr.ExpandTools();
            canMove = false;
        }

        /// <summary>
        /// 釣り竿をもとに戻す
        /// </summary>
        void RetrieveRod()
        {
            m_state = State.Normal;
            fishingToolMgr.RetrieveTools();
            //遅延を持たせたい
            canMove = true;
        }

    }
}