using UnityEngine;



public class GameManager : MonoBehaviour
{

    public GameObject chesspiece;

    //Matrices needed, positions of each of the GameObjects
    //Also separate arrays for the players in order to easily keep track of them all
    //Keep in mind that the same objects are going to be in "positions" and "playerBlack"/"playerWhite"
    private GameObject[,] positions = new GameObject[8, 8];

    //current turn
    private string currentPlayer = "black";

    //Game Ending
    private bool gameOver = false;


    public void SetPositionEmpty(int x, int y)
    {
        positions[x, y] = null;
    }


    public void SetPosition(GameObject obj)
    {
        ChessPieces cp = obj.GetComponent<ChessPieces>();

        //Overwrites either empty space or whatever was there
        positions[cp.GetXBoard(), cp.GetYBoard()] = obj;
    }



    public GameObject GetPosition(int x, int y)
    {
        return positions[x, y];
    }



    public bool PositionOnBoard(int x, int y)
    {
        if (x < 0 || y < 0 || x >= positions.GetLength(0) || y >= positions.GetLength(1)) return false;
        return true;
    }



    public string GetCurrentPlayer()
    {
        return currentPlayer;
    }



    public bool IsGameOver()
    {
        return gameOver;
    }



    public void NextTurn()
    {
        if (currentPlayer == "white")
        {
            currentPlayer = "black";
        }
        else
        {
            currentPlayer = "white";
        }
    }

}







/*int matrixX;
int matrixY;

//private bool gameOver = false;

public bool attack = false;


private void OnMouseDown()
{
    ChessBoardPlacementHandler.Instance.ClearHighlights();

    InitiateMovePlates();

    Debug.Log("hello");
}

public void InitiateMovePlates()
{
    switch (this.name)
    {

        case "B_King":
        case "W_King":
            //SurroundMovePlate();
            break;


        case "B_Queen":
        case "W_Queen":
            ChessBoardPlacementHandler.Instance.Highlight(1, 0);
            ChessBoardPlacementHandler.Instance.Highlight(0, 1);
            ChessBoardPlacementHandler.Instance.Highlight(-1, 0);
            ChessBoardPlacementHandler.Instance.Highlight(0, -1);
            ChessBoardPlacementHandler.Instance.Highlight(1, 1);
            ChessBoardPlacementHandler.Instance.Highlight(-1, -1);
            ChessBoardPlacementHandler.Instance.Highlight(1, -1);
            ChessBoardPlacementHandler.Instance.Highlight(-1, 1);
            break;


        case "B_Knight":
        case "W_Knight":
            //LMovePlate();
            break;


        case "B_Bishop":
        case "W_Bishop":
            ChessBoardPlacementHandler.Instance.Highlight(1, 1);
            ChessBoardPlacementHandler.Instance.Highlight(1, -1);
            ChessBoardPlacementHandler.Instance.Highlight(-1, 1);
            ChessBoardPlacementHandler.Instance.Highlight(-1, -1);
            break;


        case "B_Rook":
        case "W_Rook":
            ChessBoardPlacementHandler.Instance.Highlight(1, 0);
            ChessBoardPlacementHandler.Instance.Highlight(0, 1);
            ChessBoardPlacementHandler.Instance.Highlight(-1, 0);
            ChessBoardPlacementHandler.Instance.Highlight(0, -1);
            break;

        case "B_Pawn":
        case "W_Pawn":
            //PawnMovePlate()
            break;
    }
}*/