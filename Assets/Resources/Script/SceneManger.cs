﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneManger : MonoBehaviour
{
    public void ChangeScene(string Scenename)
    {
        SceneManager.LoadScene(Scenename);
    }
}
