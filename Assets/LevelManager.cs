using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public MazeGenerator mazeGenerator;
    public Transform player;
    public Transform startMarker;
    public Transform exitMarker;

    public LevelUI levelUI;
    public TimerUI timerUI;


    private int currentLevel = 1;

    void Start()
    {
        LoadLevel(currentLevel);
    }

    public void LoadLevel(int level)
    {
        Debug.Log("LoadLevel() CHIAMATO! Genero livello: " + level);

        currentLevel = level;
        mazeGenerator.GenerateLevel(level);

        player.position = startMarker.position;
        //aggiorna numero liv e timer
        timerUI.StartTimer();
        levelUI.SetLevel(level);
    }

    public void NextLevel()
    {
        Debug.Log("NextLevel() CHIAMATO! Livello attuale: " + currentLevel);
        currentLevel++;
        LoadLevel(currentLevel);
    }
}
