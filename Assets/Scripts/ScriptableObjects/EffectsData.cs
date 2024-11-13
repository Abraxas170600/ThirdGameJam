using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EffectsData", menuName = "ScriptableObjects/New EffectData", order = 1)]
public class EffectsData : ScriptableObject
{
    public EffectData[] effectsObj;
}
public enum EnumEffect
{
    CFXR_Explo1,
    CFXR_Explo2,
    CFXR_Explo3
}

[System.Serializable]
public class EffectData
{
    public GameObject effect;
    public EnumEffect effectKey;
    
}