using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Highlighter : MonoBehaviour
{
    public GameObject game_object;

    GameObject reference = null;

    public bool attack = false;

    int matrixX;
    int matrixY;


    private void Start()
    {
        if (attack)
        {
            //Set to red
            gameObject.GetComponent<SpriteRenderer>().color = new Color(1.0f, 0.0f, 0.0f, 1.0f);
        }
    }


    public void OnMouseUp()
    {
        game_object = GameObject.FindGameObjectWithTag("GameManager");

        if (attack)
        {
            GameObject _attack = game_object.GetComponent<GameManager>().GetPosition(matrixX, matrixY);

            Destroy(_attack);
        }

        game_object.GetComponent<GameManager>().SetPositionEmpty(reference.GetComponent<ChessPieces>().GetXBoard(),
            reference.GetComponent<ChessPieces>().GetYBoard());

        //Move reference chess piece to this position
        reference.GetComponent<ChessPieces>().SetXBoard(matrixX);
        reference.GetComponent<ChessPieces>().SetYBoard(matrixY);

        //Update the matrix
        game_object.GetComponent<GameManager>().SetPosition(reference);

        //Switch Current Player
        game_object.GetComponent<GameManager>().NextTurn();

        //Destroy the move plates including self
        
        //reference.GetComponent<ChessPieces>().DestroyMovePlates();

    }


    public void SetCoords(int x, int y)
    {
        matrixX = x;
        matrixY = y;
    }


    public void SetReference(GameObject obj)
    {
        reference = obj;
    }


    public GameObject GetReference()
    {
        return reference;
    }

}