using UnityEngine;
using System.Collections;

public class Done_GameController : MonoBehaviour
{
	public GameObject[] hazards;
	public Vector3 spawnValues;
    //
	public int hazardCount=1;
	//public float spawnWait=1;
	public float startWait=1;
    //
	public float waveWait;
	
	public GUIText scoreText;
	public GUIText restartText;
	public GUIText gameOverText;
	
	private bool gameOver;
	private bool restart;
    private int score = 0;
    private int Total = 0;
    private int Miss =0;
	//
	private bool gamestartModel1=false;
    private bool gamestartModel2=false;
    private bool gameselect=false;

	void Start ()
	{
        //游戏在这里开始；
        //
        gameOver = false;
		restart = false;
		restartText.text = "1:简单  2.中等  3.困难  4.练习模式";
		gameOverText.text = "";
        scoreText.text = "";
        score = 0;
        Total = 0;
        Miss = 0;

		gameselect = true;
	//	UpdateScore ();
	//	StartCoroutine (SpawnWaves ());
	//	Client.GetInstance();
	}

    void Update ()
	{
		if (gameselect) {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                Done_Mover_asteroid.asteroidSpeed = -1;
                Done_Mover_enemy.enemySpeed = -2 ;
                //hazardCount = 3;
                waveWait = 4;
                gamestartModel1 = true;
            }
            else if (Input.GetKeyDown(KeyCode.Alpha2)|| Input.GetKeyDown(KeyCode.R))
            {
                Done_Mover_asteroid.asteroidSpeed = -3;
                Done_Mover_enemy.enemySpeed = -4;
                //hazardCount = 7;
                waveWait = 2;
                gamestartModel1 = true;
            }
            else if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                Done_Mover_asteroid.asteroidSpeed = -5;
                Done_Mover_enemy.enemySpeed = -6;
                //hazardCount = 10;
                waveWait = 1;
                gamestartModel1 = true;
            }
            else if (Input.GetKeyDown(KeyCode.Alpha4))
            {
                Done_Mover_asteroid.asteroidSpeed = -3;
                Done_Mover_enemy.enemySpeed = -4;
                waveWait = 2;
                gamestartModel2 = true;
            }

            if (gamestartModel1) {
                gamestartModel1 = false;
                restartText.text = "";
                UpdateScore();
                StartCoroutine(SpawnWaves());
                Client.GetInstance();         
            }
            if (gamestartModel2)
            {
                gamestartModel2 = false;
                restartText.text = "";
                UpdateScore();
                StartCoroutine(SpawnPractice());
                Client.GetInstance();             
            }
        }

		if (restart)
		{
			if (Input.GetKeyDown (KeyCode.R))
 			{
                Application.LoadLevel (Application.loadedLevel);
                Client.GetInstance().ReStart();
			}
			//
			if (Input.GetKeyDown(KeyCode.E))
			{
                Client.GetInstance().Destroy();
				Application.Quit();

			}
			//
		}
	}

    IEnumerator SpawnPractice() {
        yield return new WaitForSeconds(startWait);
        while (true) {
            GameObject hazard1 = hazards[0];
            Vector3 spawnPosition1 = new Vector3(6, spawnValues.y, spawnValues.z);
            Quaternion spawnRotation1 = Quaternion.identity;
            Instantiate(hazard1, spawnPosition1, spawnRotation1);
            yield return new WaitForSeconds(waveWait);
            GameObject hazard2 = hazards[0];
            Vector3 spawnPosition2 = new Vector3(0, spawnValues.y, spawnValues.z);
            Quaternion spawnRotation2 = Quaternion.identity;
            Instantiate(hazard2, spawnPosition2, spawnRotation2);
            yield return new WaitForSeconds(waveWait);
            GameObject hazard3 = hazards[0];
            Vector3 spawnPosition3 = new Vector3(-6, spawnValues.y, spawnValues.z);
            Quaternion spawnRotation3 = Quaternion.identity;
            Instantiate(hazard3, spawnPosition3, spawnRotation3);
            yield return new WaitForSeconds(waveWait);
            GameObject hazard4 = hazards[0];
            Vector3 spawnPosition4 = new Vector3(0, spawnValues.y, spawnValues.z);
            Quaternion spawnRotation4 = Quaternion.identity;
            Instantiate(hazard4, spawnPosition4, spawnRotation4);
            yield return new WaitForSeconds(waveWait);

            if (gameOver)
            {
                restartText.text = "按R重新开始 " + " 按E退出游戏";
                restart = true;
                break;
            }
        }
    }



    IEnumerator SpawnWaves()
	{
		yield return new WaitForSeconds (startWait);
		while (true)
		{
//			for (int i = 0; i < hazardCount; i++)
//			{
				GameObject hazard = hazards [Random.Range (0, hazards.Length)];
				Vector3 spawnPosition = new Vector3 (Random.Range (-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
				Quaternion spawnRotation = Quaternion.identity;
				Instantiate (hazard, spawnPosition, spawnRotation);
//				yield return new WaitForSeconds (spawnWait);
//			}
			yield return new WaitForSeconds (waveWait);
			
			if (gameOver)
			{
				restartText.text = "按R重新开始 " + " 按E退出游戏";
				restart = true;
				break;
			}
		}
	}

    public void AddScore (int newScoreValue){
		score += newScoreValue;
		UpdateScore ();
	}

    public void AddTotal(int newTotalValue) {
        Total += newTotalValue;
        UpdateScore();
    }

    public void AddMiss(int newMissValue) {
        Miss += newMissValue;
        UpdateScore();
    }

	void UpdateScore ()
	{
		scoreText.text =  "总数 :" +Total + "    失误:" +Miss +  "        分数: " + score;
    }
	
	public void GameOver ()
	{
		gameOverText.text = "游戏结束!";
		gameOver = true;
	}
}