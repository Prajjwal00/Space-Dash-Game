using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class UIManager : MonoBehaviour
  
{    
    
    [SerializeField]
    private Text _scoreText;
    [SerializeField]
    private Text _HighScoreText;
    [SerializeField]
    private Sprite[] _livesSprites;
    [SerializeField]
    private Image _LivesImg;
    [SerializeField]
    private Text _gameOverText;
    [SerializeField]
    private Text _restartText;
    private GameManager _gameManager;
    private Player _player;
    public int _score,_HighScore;
    
     private void Start()
    {    
          _HighScore= PlayerPrefs.GetInt("HighScore",0);
            _HighScoreText.text = "Best :" + _HighScore;
         _scoreText.text = "Score:"+0; 
        
        _gameOverText.gameObject.SetActive(false);
        _gameManager= GameObject.Find("Game_Manager").GetComponent<GameManager>();
        if(_gameManager == null)
        {
            Debug.LogError("GameManager is Null");
        }
         
    }
    

     public void UpdateScore(int playerScore)
      {
         
         _score +=10;
        _scoreText.text = "Score:"+ playerScore.ToString();
       
      }
      public void UpdateHighScore()
      {        
       if(_score> _HighScore)
       {
              _HighScore = _score;
              PlayerPrefs.SetInt("HighScore",_HighScore);
             
              _HighScoreText.text = "Best :" + _HighScore;
       }
      }
      
     
     public void UpdateLives(int currentLives)
     {
        _LivesImg.sprite = _livesSprites[currentLives];
        if(currentLives == 0)
        {
           GameOverSequence();
        }
     }
      
    void GameOverSequence()
    {    
           _gameManager.GameOver();
           _gameOverText.gameObject.SetActive(true);
           _restartText.gameObject.SetActive(true);
           StartCoroutine(GameOverFlickerRoutine());
    }
    

    IEnumerator GameOverFlickerRoutine()
    {
        while(true)
        {
            _gameOverText.text = "GAME OVER";
            
            yield return new WaitForSeconds(0.5f);
            _gameOverText.text = "" ;
            yield return new WaitForSeconds(0.5f);
            
        }
    }
    public void ResumePlay()
    {
       _gameManager.ResumeGame();
    }
    public void BackToMainMenu()
    {
        SceneManager.LoadScene(0);
    }
}
