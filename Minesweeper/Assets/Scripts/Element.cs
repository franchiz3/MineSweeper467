using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Element : MonoBehaviour {

    public bool isMine, covered;
    TimeController timeScore;

    // Different Textures
    public Sprite[] emptyTextures;
    public Sprite mineTexture;
   // private GameObject loseScreen, winScreen;
    //private ShowPanels showPanels;

    // Use this for initialization
    void Start()
    {
        //Randomly decide if it's a mine or not
        isMine = Random.value < 0.15;
        if(!isMine)
        covered = true;
        int x = (int)transform.position.x;
        int y = (int)transform.position.y;
        Griddy.elements[x, y] = this;
        timeScore = GameObject.FindGameObjectWithTag("GameController").GetComponent<TimeController>();
        //Griddy.uncoverMines();
    }
    public void loadTexture(int adjacentCount)
    {
        if (isMine)
            GetComponent<SpriteRenderer>().sprite = mineTexture;
        else
            GetComponent<SpriteRenderer>().sprite = emptyTextures[adjacentCount];
    }

    public bool isCovered()
    {
        Debug.Log(GetComponent<SpriteRenderer>().sprite.texture.name);
        covered = GetComponent<SpriteRenderer>().sprite.texture.name == "Default";
        Debug.Log(covered);
        return covered;
    }

    void OnMouseUpAsButton()
    {
        if (isMine)
        {
            //showPanels = GetComponent<ShowPanels>();
            Griddy.uncoverMines();
            // game over
            Invoke("loadLose", 1);
            
        }
        else
        {
            // show adjacent mine number
            timeScore.endTime = Time.time;
            int x = (int)transform.position.x;
            int y = (int)transform.position.y;
            loadTexture(Griddy.adjacentMines(x, y));

            // uncover area without mines
            Griddy.FFuncover(x, y, new bool[Griddy.w, Griddy.h]);
            if (Griddy.isFinished())
            {
                ScoreData score = new ScoreData();
                float newScore = timeScore.getScore(timeScore.endTime);
                if (newScore > score.highScore)
                    score.setHighScore(newScore);
                else
                    score.setLastScore(newScore);
                Invoke("loadWin", 1);       
                //Debug.Log("Win Triggered");
            }
                
        }
    }

    void loadWin()
    {
        // StartOptions.showPanels.ShowMenu();
        //Destroy(mainMenu);
        PlayMusic.stopMusic();
        SceneManager.LoadSceneAsync("winMenu");
        //Debug.Log(StartOptions.inMainMenu);
    }

    void loadLose()
    {
        // StartOptions.showPanels.ShowMenu();
        //Destroy(mainMenu);
        PlayMusic.stopMusic();
        SceneManager.LoadSceneAsync("loseMenu");
        //Debug.Log(StartOptions.inMainMenu);
    }


}
