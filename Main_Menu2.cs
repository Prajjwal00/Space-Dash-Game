using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Main_Menu2 : MonoBehaviour
{

  public void LoadSinglePlayerGame()

    {
      Debug.Log("Single Player Game Loading....");
     SceneManager.LoadScene(1);

    }
    public void LoadCoOpMode()
    {
        Debug.Log(" Co-op Game Loading....");
        SceneManager.LoadScene(2);
    }
}