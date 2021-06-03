using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Score : MonoBehaviour
{
   public GameManager _gameManager;
   public List<GameObject> stars;
   private int starsCount = -1;

   private void Start()
   {
      for (int i = 0; i < transform.childCount; i++)
      {
         stars[i].SetActive(false);
      }
   }

   public void ShowStars()
   {
      if (_gameManager.activePeople == 0)
      {
         starsCount = -1;
      }
      if (_gameManager.activePeople <= 2 && _gameManager.activePeople > 0)
      {
         starsCount = 0;
      }
      if (_gameManager.activePeople <= 4 && _gameManager.activePeople >= 2)
      {
         starsCount = 1;
      }
      if (_gameManager.activePeople <= 6 && _gameManager.activePeople >= 4)
      {
         starsCount = 2;
      }
   }

   private void Update()
   {
      for (int i = 0; i < starsCount; i++)
      {
         stars[i].SetActive(true);
      }
   }
}
