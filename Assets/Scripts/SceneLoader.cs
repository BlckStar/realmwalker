using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
   public void Load(string Scene)
   {
      Debug.Log(Scene);
      SceneManager.LoadScene(Scene);
   }
}
