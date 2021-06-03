using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkinModule : MonoBehaviour
{
   public int id;
   public Renderer renderer;
   private void Update()
   {
      if (SkinChanger.mainId == id)
      {
         renderer.enabled = true;
      }
      else
      {
         renderer.enabled = false;
      }
   }
}
