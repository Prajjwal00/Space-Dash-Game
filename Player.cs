using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{   [SerializeField]
    private float _speed = 3.5f;
    private float _speedMultiplier =2;
    [SerializeField]
    private GameObject _laserPrefab;
    [SerializeField]
    private float _firerate=0.5f;
    private float _canfire=-1f;
    [SerializeField]
    private int _lives=3;
    private SpawnManager _spawnManager;
    [SerializeField]
    private GameObject _tripleShotPrefab;
    private bool _isTripleShotActive= false;
    private bool _isSpeedBoostActive= false;
    private bool _isShieldsActive =false;
    [SerializeField]
    private GameObject _shieldsVisualiser;
    [SerializeField]
    private GameObject _leftEngine, _rightEngine;
    [SerializeField]
    private int _score,_HighScore;
   
    private UIManager _uiManager;
    [SerializeField]
    private AudioClip _laserSoundClip;
    private AudioSource _audioSource;
    private GameManager _gameManager;
    public bool isPlayerOne = false;
    public bool isPlayerTwo = false;
    void Start()
    {
        _gameManager = GameObject.Find("Game_Manager").GetComponent<GameManager>();
       _spawnManager = GameObject.Find("Spawn_Manager").GetComponent<SpawnManager>();
       _uiManager= GameObject.Find("Canvas").GetComponent<UIManager>();
       if(_gameManager.isCoopMode == false)
       {
       
        transform.position=new Vector3(0,0,0);
       }
       
       _audioSource= GetComponent<AudioSource>();
       if(_spawnManager == null)

       {
          Debug.LogError("The Spawn Manager is NULL");
       }
       if(_uiManager == null)
       {
              Debug.LogError("The UIManager is NULL");
       }
       if(_audioSource == null)
       {
       }
       else
       {
              _audioSource.clip = _laserSoundClip;
       }
       
    }


    
    void Update()
    { 
       if(isPlayerOne == true)
       {
       calculatemovement();
       if((Input.GetKeyDown(KeyCode.Space) && Time.time > _canfire) && isPlayerOne == true)
          {
          FireLaser();
	      }
       
       }
       if(isPlayerTwo == true)
       {
          PlayerTwoMovement();
          if((Input.GetKeyDown(KeyCode.KeypadEnter) && Time.time > _canfire )&& isPlayerTwo == true)
          {
          FireLaserPlayerTwo();
	      }
        

       }
       
       
     }
    void calculatemovement()
        {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        Vector3 direction =new Vector3(horizontalInput,verticalInput,0);
      
        transform.Translate(direction*_speed*Time.deltaTime);
      
        transform.position= new Vector3(transform.position.x,Mathf.Clamp(transform.position.y,-3.8f,0),0);

       if(transform.position.x>=11.3f) 
        {
        transform.position=new Vector3(-11.3f,transform.position.y,0);
		}
        else if(transform.position.x<=-11.3f)
        {
         transform.position= new Vector3(11.3f,transform.position.y,0);  
		}
        
        }
         void PlayerTwoMovement()
        {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
      
         if(Input.GetKey(KeyCode.Keypad8))
        {
            transform.Translate(Vector3.up*_speed*Time.deltaTime);
        }
         if(Input.GetKey(KeyCode.Keypad2))
        {
            transform.Translate(Vector3.down*_speed*Time.deltaTime);
        }
         if(Input.GetKey(KeyCode.Keypad4))
        {
            transform.Translate(Vector3.left*_speed*Time.deltaTime);
        }
         if(Input.GetKey(KeyCode.Keypad6))
        {
            transform.Translate(Vector3.right*_speed*Time.deltaTime);
        }

      
        transform.position= new Vector3(transform.position.x,Mathf.Clamp(transform.position.y,-3.8f,0),0);

       if(transform.position.x>=11.3f) 
        {
        transform.position=new Vector3(-11.3f,transform.position.y,0);
		}
        else if(transform.position.x<=-11.3f)
        {
         transform.position= new Vector3(11.3f,transform.position.y,0);  
		}
        
        }
        void FireLaser()
       {
        _canfire= Time.time +_firerate;
        
        if(_isTripleShotActive == true)
        {
            Instantiate(_tripleShotPrefab,transform.position,Quaternion.identity);

        }
        else
        {
        Instantiate(_laserPrefab,transform.position+ new Vector3(0,1.05f,0),Quaternion.identity);
       
        }
        _audioSource.Play();
        
	   }
       void FireLaserPlayerTwo()
       {
        _canfire= Time.time +_firerate;
        
        if(_isTripleShotActive == true)
        {
            Instantiate(_tripleShotPrefab,transform.position,Quaternion.identity);

        }
        else
        {
        Instantiate(_laserPrefab,transform.position+ new Vector3(0,1.05f,0),Quaternion.identity);
       
        }
        _audioSource.Play();
        
	   }
	
        public void Damage()
        {
        if(_isShieldsActive== true)
        {    
             _isShieldsActive=false;
             _shieldsVisualiser.SetActive(false);
            return;
        }
        
         _lives--;
         if(_lives == 2)
         {
              _leftEngine.SetActive(true);

         }
         else if(_lives == 1)
         {
              _rightEngine.SetActive(true);
         }

         _uiManager.UpdateLives(_lives);

         if(_lives<1)
         { 
           _spawnManager.OnPlayerDeath();
            _uiManager.UpdateHighScore();
          Destroy(this.gameObject);
         
     
         
		 }
       
		}
        public void TripleShotActive()
        {
            _isTripleShotActive= true;
            StartCoroutine(TripleShotPowerDownRoutine());

        }
        IEnumerator TripleShotPowerDownRoutine()
        {
            yield return new WaitForSeconds(5.0f);
            _isTripleShotActive = false;
        }
        public void SpeedBoostActive()
        {
            _isSpeedBoostActive =true;
            StartCoroutine(SpeedBoostPowerDownRoutine());
            _speed *=_speedMultiplier;
        }
        IEnumerator SpeedBoostPowerDownRoutine()
        {
            yield return new WaitForSeconds(5.0f);
            _isSpeedBoostActive= false;
            _speed /=_speedMultiplier;                                                     
        }
        public void ShieldsActive()
        {
            _isShieldsActive= true;
            _shieldsVisualiser.SetActive(true);
        }
       
         public void AddScore(int points)
         {
            _score+=points;
            _uiManager.UpdateScore(_score);
         }
         
}

