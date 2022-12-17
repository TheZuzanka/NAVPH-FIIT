using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Level
{
    public class HeartManager : MonoBehaviour
    {
        [SerializeField] private List<GameObject> heartObjects = new();
        private Player _player;

        private void Start()
        {
            StartCoroutine(AssignPlayer());
        }

        private IEnumerator AssignPlayer()
        {
            // as player is spawned in scene to obtain reference this method has to be delayed 
            
            yield return new WaitForEndOfFrame();
            _player = GameObject.FindWithTag("Player").GetComponent<Player>();
            
            // this is an observer of health system
            _player.heartDelegate += DisplayHearts;
            
            _player.UpdateState();
        }

        private void DisplayHearts(int hearts)
        {
            for (var i = 0; i < heartObjects.Count; i++)
            {
                if (i < hearts)
                {
                    heartObjects[i].gameObject.SetActive(true);
                }
                else
                {
                    heartObjects[i].gameObject.SetActive(false);
                }
            }
        }
    }
}