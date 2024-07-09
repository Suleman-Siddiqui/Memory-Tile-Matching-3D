using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MemoryManager : MonoBehaviour
{
    public MemoryTiles[] memoryTilesArr;
    public Sprite[] itemSpriteArr;
    public SpriteRenderer[] tileSpriteRenderarr;

    public static MemoryManager Instance;
    // use for save random number to check not repeat
    List<int> rTileNumber = new List<int>();

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        rTileNumber = new List<int>();
        SetItemIntoMemoryRandomly();

    }

    void SetItemIntoMemoryRandomly()    //a method used to set up the items in the memory game randomly.
    {
        for (int i = 0; i < itemSpriteArr.Length; i++)  //iterates through each unique item in itemSpriteArr.
        {
            int rItem = i;
            for (int k = 0; k < 2; k++)                 // creating pairs of tiles for each item.
            {
                int rTile = GetTileIndexNumber_Random();         //is called to get a random index for the memory tile array (rTile).
                                                                 //This method likely ensures that the same tile index is not repeated to avoid duplicates.


                tileSpriteRenderarr[rTile].sprite = itemSpriteArr[rItem];   //sets the sprite of the tile at the random index to the corresponding item sprite.



                memoryTilesArr[rTile].tileId = rItem;
                memoryTilesArr[rTile].name = rItem.ToString();

            }
        }
    }



    int GetTileIndexNumber_Random()
    {
        int counter = 0;
        int rTile = Random.Range(0, memoryTilesArr.Length);        //rTile is assigned a random index within the range of the memoryTilesArr.Length.

        while (rTileNumber.Contains(rTile) /*&& counter <30*/)     //The while loop checks if the generated rTile is already present in the rTileNumber list.
                                                                   //If it is, a new random index is generated until a unique index is found.
        {
            rTile = Random.Range(0, memoryTilesArr.Length);
            counter++;
        }
        rTileNumber.Add(rTile);      //Once a unique rTile is found, it is added to the rTileNumber list.

        return rTile;


    }

    public static int counterCheck;

    public void ResetTile()
    {
        if (counterCheck == itemSpriteArr.Length)        //condition checks if the number of tiles that have been successfully matched(counterCheck) is equal to
                                                         //the total number of distinct item sprites(itemSpriteArr.Length). All pairs have been successfully matched.
        {
            int counter = 0;
            while (counter < memoryTilesArr.Length)
            {
                memoryTilesArr[counter].FilpTileFaceDown(); //method likely flips the tile face down, hiding it from view.
                counter++;
            }
            Invoke("DelayToResetSceneLoad", 1.5f);
            counterCheck = 0;
        }

    }

    void DelayToResetSceneLoad()
    {
        rTileNumber = new List<int>();                      // new list
        counterCheck = 0;                                   //is set to 0, indicating that the memory tiles have been reset.
                                                            //  Invoke("SetItemIntoMemoryRandomly", 1f);
        SetItemIntoMemoryRandomly();
    }

}

