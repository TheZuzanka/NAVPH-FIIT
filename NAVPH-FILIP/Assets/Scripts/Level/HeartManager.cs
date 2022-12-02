using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Level
{
    public class HeartManager : MonoBehaviour
    {
        [SerializeField] private List<FullHeart> heartObjects = new();
        private Player _player;

        private void Start()
        {
            StartCoroutine(AssignPlayer());
        }

        private IEnumerator AssignPlayer()
        {
            yield return new WaitForEndOfFrame();
            _player = GameObject.FindWithTag("Player").GetComponent<Player>();
            _player.heartDelegate += NieUpdate;
            
            _player.UpdateState();
        }

        private void NieUpdate(int hearts)
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