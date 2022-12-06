using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectionRod : MonoBehaviour
{
    public int Row;
    public int Column;
    private int Floor = 0;

    public GameObject BallRed;
    public GameObject BallBlue;



    private void OnMouseEnter()
    {
        if (Floor < 4)
        {
            this.gameObject.GetComponent<MeshRenderer>().enabled = true;
        }
    }

    private void OnMouseExit()
    {
        this.gameObject.GetComponent<MeshRenderer>().enabled = false;
    }

    private void OnMouseDown()
    {
        InstantiateBall();
    }


    public void InstantiateBall(){
        // va demander plus tard au game mangaer quelle boule poser et si il a le droit de en poser
        if (GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>().isPlayerTurn)
        {
            Instantiate(BallBlue, this.transform.position + Vector3.up, Quaternion.identity);
        }
        else
        {
            Instantiate(BallRed, this.transform.position + Vector3.up, Quaternion.identity);
        }
        //   GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>().SetBallInArray();

        GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>().SetBallInArray(Row, Column, Floor);
        Floor++;
    }

    public void IAPlay()
    {
        Invoke("InstantiateBall", 1);
    }
}
