using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MemoryTiles : MonoBehaviour
{

    public int tileId;

    public static MemoryTiles lastClick_Memorytile;
    private static bool isFlipping = false;  // Added variable to track whether tiles are currently flipping


    private void OnMouseDown()
    {
        if (!isFlipping)                                              //new
        {
            if (MemoryTiles.lastClick_Memorytile == null)
            {
                // If no tile is currently clicked, make this tile the current clicked tile
                MemoryTiles.lastClick_Memorytile = this;
                FilpTileFaceUp();
            }
            else
            {
                // If another tile is already clicked
                FilpTileFaceUp(() =>
                {
                    if (lastClick_Memorytile != this) // The callback checks if the previously clicked tile (lastClick_Memorytile) is not the same as the current tile (this).
                    {
                        // If the previously clicked tile is not the same as the current one
                        if (lastClick_Memorytile.tileId == tileId)
                        {
                            // If the tiles match
                            SoundManagerr.Instance.PlayAudio("Tile_Solve");
                            lastClick_Memorytile.GetComponent<Collider>().enabled = false;
                            GetComponent<Collider>().enabled = false;
                            lastClick_Memorytile = null;
                            MemoryManager.counterCheck++;
                            Invoke("ResetAllMermorytileDithDelay", 1);
                        }
                        else
                        {
                            // If the tiles don't match
                            FilpTileFaceDown();
                            lastClick_Memorytile.FilpTileFaceDown();
                            lastClick_Memorytile = null;
                        }
                    }
                });
            }
        }
    }

    public void FilpTileFaceUp(System.Action action = null)
    {
        SoundManagerr.Instance.PlayAudio("Tile_Click");

        transform.DOLocalRotate(new Vector3(-270f, 34f, 0f), 0.5f).OnComplete(() =>
        {
            action?.Invoke();
        });

        StartCoroutine(WaitForFlipAnimation());
    }

    private IEnumerator WaitForFlipAnimation()                       //new
    {
        isFlipping = true;
        yield return new WaitForSeconds(0.5f);  // Adjust the delay based on your flip animation duration
        isFlipping = false;
    }

    public void FilpTileFaceDown()
    {
        SoundManagerr.Instance.PlayAudio("Swish");
        transform.DOLocalRotate(new Vector3(-90, 34f, 0), 0.5f).OnComplete(() =>
        {
            GetComponent<BoxCollider>().enabled = true;
        });
    }

    void ResetAllMermorytileDithDelay()
    {
        MemoryManager.Instance.ResetTile();
    }


}

