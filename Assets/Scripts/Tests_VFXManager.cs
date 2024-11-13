using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Tests_VFXManager : MonoBehaviour
{
    float speed = 1.5f, x, y, z = 0;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        x = (float)Random.Range(-6, 5);
       
        transform.Translate(x * speed * Time.deltaTime, x * speed * Time.deltaTime, 0);


        if (Input.GetKeyDown(KeyCode.E))
        {
            VFXManager.Instance.PlayVFX(EnumEffects.CFXR_Explo1, gameObject.transform);
        }
        else if(Input.GetKeyDown(KeyCode.R))
        {
            VFXManager.Instance.PlayVFX(EnumEffects.CFXR_Explo2, gameObject.transform);
        }
        else if (Input.GetKeyDown(KeyCode.T))
        {
            VFXManager.Instance.PlayVFX(EnumEffects.CFXR_Explo3, gameObject.transform);
        }
    }
}
