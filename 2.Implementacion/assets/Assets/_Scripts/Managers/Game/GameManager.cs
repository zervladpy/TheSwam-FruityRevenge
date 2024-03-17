using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    [SerializeField] private Text scoreText;
    [SerializeField] private Text livesText;
    [SerializeField] private GameObject SpawnPoint;
    [SerializeField] private GameObject DefaultPlayer;

    private const int DEFAULT_LIVES = 3;
    private const string AWAIT_AND_CHANGE_SCENE = "ChangeSceneAwait";
    private const int SCENE_SECONDS_AWAIT = 2;
    private const int LAST_LEVEL = Scenes.LEVEL_2;
    private List<int> PLAYABLE_SCENES = new List<int> { Scenes.LEVEL_1, Scenes.LEVEL_2 };

    private int currentScene;

    static private int score = 0;
    static private int levelScore = 0;
    static private int lives = DEFAULT_LIVES;
    static private GameObject player;

    public static GameManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        } else
        {
            Destroy(gameObject);
        }
    }

    public void Start()
    {
        currentScene = SceneManager.GetActiveScene().buildIndex;

        UpdateLiveText();
        UpdateScoreText();

        SpawnPlayer();
    }

    /// <summary>
    /// Spawns a Player Prefab on a designated point
    /// </summary>
    private void SpawnPlayer()
    {
        if (SpawnPoint != null)
        {
            GameObject spawnable = null;

            if (player == null)
            {
                spawnable = DefaultPlayer;
            } else
            {
                spawnable = player;
            }

            Vector3 spawnPos = new Vector2(SpawnPoint.transform.position.x, SpawnPoint.transform.position.y);

            Instantiate(spawnable, spawnPos, Quaternion.identity);
        }
    }

    /// <summary>
    /// On Start Button Pressed
    /// </summary>
    public void StartNewGame()
    {
        lives = DEFAULT_LIVES;
        score = 0;
        levelScore = 0;

        LoadNewScene(Scenes.LEVEL_1);
    }

    /// <summary>
    /// On Select Character Button Pressed
    /// </summary>
    public void ToSelectCharacter() 
    {
        LoadNewScene(Scenes.SELECT_CHARACTER);
    }

    /// <summary>
    /// On Exit Button Pressed
    /// </summary>
    public void Exit()
    {
        Application.Quit();
    }

    /// <summary>
    /// On Return To Main Menu
    /// </summary>
    public void ReturnToMainMenu()
    {
        lives = DEFAULT_LIVES;
        levelScore = 0;
        score = 0;

        LoadNewScene(Scenes.MENU);
    }

    /// <summary>
    /// Loads a new Scene
    /// </summary>
    /// <param name="Scene">Scene Index</param>
    private void LoadNewScene(int Scene)
    {
        levelScore = 0;
        SceneManager.LoadScene(Scene);
    }

    /// <summary>
    /// Resets The current Scene
    /// </summary>
    public void ResetScene()
    {
        int CurrentScene = SceneManager.GetActiveScene().buildIndex;

        if (PLAYABLE_SCENES.Contains(CurrentScene))
        {
            currentScene = CurrentScene;
            ResetLevelScore();
        }

        if (lives == 0)
        {
            currentScene = Scenes.LOSE_SCENE;
        }

        StartCoroutine(AWAIT_AND_CHANGE_SCENE);
    }

    /// <summary>
    /// Next Level
    /// </summary>
    public void NextLevel()
    {

        int CurrentScene = SceneManager.GetActiveScene().buildIndex;
        Debug.Log(CurrentScene);
        if (CurrentScene == LAST_LEVEL)
        {
            currentScene = Scenes.WIN_SCENE;
        } else
        {
            currentScene += 1;
        }

        StartCoroutine(AWAIT_AND_CHANGE_SCENE);

    }

    /// <summary>
    /// Loads A New Scene with Delay <br/>
    /// 
    /// Scene Loaded is currentScene
    /// </summary>
    /// <returns></returns>
    private IEnumerator ChangeSceneAwait()
    {
        yield return new WaitForSeconds(SCENE_SECONDS_AWAIT);

        LoadNewScene(currentScene);

    }

    /// <summary>
    /// Sums the given Param to the Score
    /// </summary>
    /// <param name="Score"></param>
    public void AddScore(int Score)
    {
        levelScore += Score;
        score += Score;

        UpdateScoreText();
    }

    /// <summary>
    /// Resets The Score as Te begining of the Level
    /// </summary>
    /// <param name="Score"></param>
    public void ResetLevelScore()
    {
        score -= levelScore;
        levelScore = 0;

        UpdateScoreText();
    }

    /// <summary>
    /// Updates The Score Text
    /// </summary>
    private void UpdateScoreText()
    {
        if (scoreText != null)
        {
            scoreText.text = score.ToString();
        }
    }

    /// <summary>
    /// Updates The Lives Text
    /// </summary>
    private void UpdateLiveText()
    {
        if (scoreText != null)
        {
            livesText.text = lives.ToString();
        }
    }

    public GameObject GetPlayer()
    {
        return player;
    }

    public void SetPlayer(GameObject Player)
    {
        player = Player;
        ReturnToMainMenu();
    }

    public void RestLive()
    {
        lives--;
    }

    public void AddLives(int Lives)
    {
        lives += Lives;
    }

    public void AddLive()
    {
        lives++;
    }

    public int GetLives()
    {
        return lives;
    }

}
