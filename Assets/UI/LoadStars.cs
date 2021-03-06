﻿using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class LoadStars : MonoBehaviour
{
    public GameObject[] level_preview;

    private int[] stars = new int[2];

    // Start is called before the first frame update
    void Start()
    {
        //stars1 = int.Parse(LoadConfigs("stars.cfg").Split(',')[0]);
        //stars2 = int.Parse(LoadConfigs("stars.cfg").Split(',')[1]);
        
        stars[0] = PlayerPrefs.GetInt("Stars1", 0);   
        stars[1] = PlayerPrefs.GetInt("Stars2", 0);

        for(int i = 0; i < level_preview.Length; i++)
            for (int j = 3; j > stars[i]; j--)
                level_preview[i].GetComponentsInChildren<Transform>()[1].gameObject.SetActive(false);

        // Выборка по всем детям
        //foreach (Transform child in level_preview_2.GetComponentsInChildren<Transform>())
        //{
        //    Debug.Log(child.name);
        //}
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //public string LoadConfigs(string file)
    //{
    //    // Check File Exists
    //    if (!File.Exists(Application.persistentDataPath+"/"+file))
    //    { // No File
    //        SaveConfigs("stars.cfg", "0,0");
    //        return "0,0";
    //    }

    //    // Load File
    //    string _data = File.ReadAllText(Application.persistentDataPath + "/" + file); // Read All Text
    //    return _data;
    //}

    //public void SaveConfigs(string file, string data)
    //{
    //    // Save File
    //    File.WriteAllText(Application.persistentDataPath + "/" + file, data); // Save datas to file
    //}
}
