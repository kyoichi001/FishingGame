﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BookDescriptionUI : MonoBehaviour
{
    [Header("References")]
    [SerializeField] Image image;
    [SerializeField] TextMeshProUGUI description;
    [SerializeField] new TextMeshProUGUI name;
    [SerializeField] TextMeshProUGUI count;

    public void Set(Sprite icon,string name_,string description_,int count_)
    {
        image.sprite = icon;
        description.text = description_;
        name.text = name_;
        count.text = $"釣った回数：{count_}";
    }
}
