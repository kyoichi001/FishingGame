﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Environment
{
    [CreateAssetMenu(menuName = "Data/PlaceData", order = 1)]
    public class PlaceData : ScriptableObject
    {
        [System.Serializable]
        public class FishGenerateData
        {
            public Fish.FishInfo fishInfo;
            public Weather weather;
            //釣れる時刻
            [Tooltip("unused")]
            public int beginTime;
            [Tooltip("unused")]
            public int endTime;
        }

        public enum Weather
        {
            Sunny,
            Cloudy,
            Rainy,
            Snowy,
            Windy,
        }

        [Tooltip("場所の名前"), TextArea]
        public string placeName;
        [Tooltip("場所の説明（地図での）"), TextArea]
        public string description;
        [Tooltip("場所の詳細な説明（図鑑とか）")]
        public string description_detail;
        [Tooltip("何円以上持ってれば移動可能か")]
        public int moneyCondition;
        [Tooltip("何週目から移動可能か")]
        public int weekCondition;
        [Tooltip("場所のアイコン")]
        public Sprite icon;
        [Tooltip("開始時刻(分)")]
        public int startTime;
        [Tooltip("終了時刻(分)")]
        public int endTime;
        [Tooltip("時間ごとの環境光")]
        public Gradient globalColor = new Gradient();

        [Header("Weather")]
        //天候に関するデータ
        //とりあえず降水確率だけ
        [Range(0,1.0f)]
        [Tooltip("降水確率")]
        public float rainyPercent;

        [Tooltip("雨が何分続くか(分)")]
        public int rainDuration=120;
        [Tooltip("その場所の背景")]
        public GameObject backGroundPrefab;
        [Tooltip("ランキングデータ")]
        public RankingSaveData rankingSaveData;

        [Header("Available fish list(Experimental)")]
        //その場所で釣れる魚たち
        //時間ごと、天候ごとに変更したい
        [Tooltip("その場所で釣れる魚たち")]
        public List<FishGenerateData> availableFishList;

    }
}