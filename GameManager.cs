using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{  
   public bool isCoopMode= false;
   [SerializeField]
   private bool _isGameOver;
   [SerializeField]
   private GameObject _pauseMenuPanel;
   private void Update()
   { 
      if(isCoopMode == false)
     {
		  if(Input.GetKeyDown(KeyCode.R) && _isGameOver == true)
		  {
				SceneManager.LoadScene(1);
		  }
		  
     }
	 else
	 {
		  if(Input.GetKeyDown(KeyCode.R) && _isGameOver == true)
		  {
				SceneManager.LoadScene(2);
		  }
		  
	 }
	 if(Input.GetKeyDown(KeyCode.Escape))
		  {
				Application.Quit();
				SceneManager.LoadScene(3);
		  }
      if(Input.GetKeyDown(KeyCode.P))
     {
		  _pauseMenuPanel.SetActive(true);
		  Time.timeScale=0;
      }

   }
   public void ResumeGame()
   {
		  _pauseMenuPanel.SetActive(false);
		  Time.timeScale =1;
   }

   public void GameOver()
   {
		  _isGameOver = true;
   }
  
}
