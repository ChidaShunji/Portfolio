﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.AI;

namespace PORTFOLIO.SceneManagement

{
public class Portal : MonoBehaviour
    {
        enum DestinationIdentifier
        {
            A, B, C, D, E
        }
   
       [SerializeField] int sceneToLoad = -1;
       [SerializeField] Transform spawnPoint;
       [SerializeField] DestinationIdentifier destination;
       [SerializeField] float fadeOutTime = 1f;
       [SerializeField] float fadeInTime = 2f;
       [SerializeField] float fadeWaitTime = 0.5f;

        private void OnTriggerEnter(Collider other) {
           if (other.tag == "Player")
           {
             StartCoroutine(Transition());
           }
       }
       
       private IEnumerator Transition()
       {
           if (sceneToLoad < 0)
           {
               Debug.LogError("Scene to load not set");
               yield break;
           }
            DontDestroyOnLoad(gameObject);

            Fader fader = FindObjectOfType<Fader>();

            yield return fader.FadeOut(fadeOutTime);
            yield return SceneManager.LoadSceneAsync(sceneToLoad);

            Portal otherPortal = GetOtherPortal();
            UpdatePlayer(otherPortal);

            yield return new WaitForSeconds(fadeWaitTime);
            yield return fader.FadeIn(fadeInTime);
            
            Destroy(gameObject);
       }

        private void UpdatePlayer(Portal otherPortal)
        {
            GameObject player = GameObject.FindWithTag("Player");
            player.GetComponent<NavMeshAgent>().enabled = false;
            player.transform.position = otherPortal.spawnPoint.position;
            player.transform.rotation = otherPortal.spawnPoint.rotation;
            player.GetComponent<NavMeshAgent>().enabled = true;


        }

        private Portal GetOtherPortal()
        {

            //7-72 意味わかっていない
            foreach (Portal portal in FindObjectsOfType<Portal>())
            {
                if (portal == this) continue;
                if (portal.destination != destination) continue;

                return portal; //portal保持
            }
            return null;


        }
    }    

}