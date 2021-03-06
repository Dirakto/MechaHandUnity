﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MessageShower
{
    private static MessageShower _me = null;
    private GameObject warningObject;
    private Text warningText;

    public string text
    { 
        set{
            warningText.text = value;
        }
    }

    private MessageShower(GameObject gm){
        warningObject = gm;
        warningText = gm.GetComponent<Text>();
    }

    public static MessageShower instanceOf(GameObject gm){
        if(_me == null)
            _me = new MessageShower(gm);
        else{
            _me.warningObject = gm;
            _me.warningText = gm.GetComponent<Text>();
        }
        return _me;
    }
}
