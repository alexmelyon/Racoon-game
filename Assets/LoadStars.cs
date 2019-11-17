using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class LoadStars : MonoBehaviour
{
    public GameObject[] level_preview;

    private int stars1, stars2;

    // Start is called before the first frame update
    void Start()
    {
        //stars1 = int.Parse(LoadConfigs("stars.cfg").Split(',')[0]);
        //stars2 = int.Parse(LoadConfigs("stars.cfg").Split(',')[1]);

        stars1 = PlayerPrefs.GetInt("Stars1", 0);
        stars2 = PlayerPrefs.GetInt("Stars2", 0);

        foreach(GameObject preview in level_preview)
            for (int i = 3; i > stars1; i--)
                preview.GetComponentsInChildren<Transform>()[1].gameObject.SetActive(false);

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
