using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Element : MonoBehaviour {

    public bool isMine;

    // Different Textures
    public Sprite[] emptyTextures;
    public Sprite mineTexture;

    // Use this for initialization
    void Start()
    {
        //Randomly decide if it's a mine or not
        isMine = Random.value < 0.15;
        int x = (int)transform.position.x;
        int y = (int)transform.position.y;
        Griddy.elements[x, y] = this;
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
        return GetComponent<SpriteRenderer>().sprite.texture.name == "default";
    }

    void OnMouseUpAsButton()
    {
        if (isMine)
        {
            Griddy.uncoverMines();
            // game over
            print("you lose");
        }
        else
        {
            // show adjacent mine number
            int x = (int)transform.position.x;
            int y = (int)transform.position.y;
            loadTexture(Griddy.adjacentMines(x, y));

            // uncover area without mines
            Griddy.FFuncover(x, y, new bool[Griddy.w, Griddy.h]);
            if (Griddy.isFinished())
                print("Winner Winner Chicken Dinner!");
        }
    }


}
