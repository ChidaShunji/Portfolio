﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PORTFOLIO.Saving;

namespace PORTFOLIO.SceneManagement
{
    public class SavingWrapper : MonoBehaviour
    {
        const string defaultSaveFile = "save";


        void Update()
        {
            if(Input.GetKeyDown(KeyCode.L))
            {
                Load();
            }

            if (Input.GetKeyDown(KeyCode.S))
            {
                Save();
            }

        }

        private void Save()
        {
            GetComponent<SavingSystem>().Save(defaultSaveFile);
        }

        private void Load()
        {
            GetComponent<SavingSystem>().Load(defaultSaveFile);
        }
    }

}