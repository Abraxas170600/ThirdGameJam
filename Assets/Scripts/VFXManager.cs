using UnityEngine;
using UnityEngine.Audio;
using CartoonFX;
using System;

public class VFXManager : MonoBehaviour
{
    public static VFXManager Instance;
    [SerializeField] EffectsData effectsData;

    [Header("Cartoon Effects Reference")]
    [SerializeField] CFXR_Effect cfxr_Effect;

    [Header("Effects Array")]
    [SerializeField] GameObject[] effects;
    private void Awake()
    {
        
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    /// <summary>
    /// Method when its called start the VFX system.
    /// </summary>
    public void PlayVFX(EnumEffects effectName, Transform positionEffect)
    {
        foreach (EffectData effectData in effectsData.effectsObj)
        {
            if (effectData.effectKey == effectName)
            {
                Instantiate(effectData.effect, positionEffect.position, Quaternion.identity);
            }
        }
        //cfxr_Effect.ResetState();
        
    }
}
