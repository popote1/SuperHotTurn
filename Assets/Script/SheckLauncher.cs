using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SheckLauncher : MonoBehaviour
{
   public ScreenShake ScreenShake;

   public void StratShake()
   {
      StartCoroutine("ScreenShake.Shake");
   }
}
