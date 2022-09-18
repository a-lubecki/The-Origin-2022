using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;


public class PostProcessingManager : MonoBehaviour {


    AutoExposure autoExposure;


    void Awake() {

        GetComponent<PostProcessVolume>().profile.TryGetSettings(out autoExposure);
    }

    public void SetBlackScreen() {

        autoExposure.keyValue.value = 0f;
    }

    public void SetWhiteScreen() {

        autoExposure.keyValue.value = 100f;
    }

}
