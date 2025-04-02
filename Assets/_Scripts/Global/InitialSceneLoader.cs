using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System;
public class InitialSceneLoader : MonoBehaviour
{
    public String CasinoSceneOnLoad;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if(CasinoSceneOnLoad == null || CasinoSceneOnLoad == "") {
            return;
        }
        SceneManager.LoadSceneAsync(CasinoSceneOnLoad, LoadSceneMode.Additive);
    }
}
