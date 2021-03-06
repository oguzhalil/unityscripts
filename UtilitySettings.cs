﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "EboxGamesSettings")]
public class UtilitySettings : ScriptableObject
{
    public bool googlePlayGameServices;
    public bool playfabServices;
    public bool admobServices;
    public bool unityAdsServices;
    public bool loggerEnabled;
    public bool inAppPurcases;
    public bool leanTween;
}
