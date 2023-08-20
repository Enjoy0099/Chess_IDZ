using Chess.Scripts.Core;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Highlighter : MonoBehaviour
{
    ChessPlayerPlacementHandler chessPieces_attachScript;
    ChessBoardPlacementHandler chessBoard_attachScript;
    GameObject referenceObject  = null;

    public bool attack;

    int Highlighter_X, Highlighter_Y;

    private void Start()
    {
        chessBoard_attachScript = ChessBoardPlacementHandler.Instance;

        if (attack)
            gameObject.GetComponent<SpriteRenderer>().color = new Color(1.0f, 0.0f, 0.0f, 1f);
    }

    public void OnMouseDown()
    {
        Debug.Log("Press..");
        if (attack)
        {
            GameObject _attack = chessBoard_attachScript.GetPosition(Highlighter_X, Highlighter_Y);

            if (_attack.name == "B_King") chessBoard_attachScript.Winner("White");
            if (_attack.name == "W_King") chessBoard_attachScript.Winner("Black");

            Destroy(_attack);
        }

        chessBoard_attachScript.SetPositionEmpty(chessPieces_attachScript.GetXBoardPos(),                                                  chessPieces_attachScript.GetYBoardPos());

        chessPieces_attachScript.SetXYBoard(Highlighter_X, Highlighter_Y);

        chessPieces_attachScript.SetPosition(referenceObject);

        chessBoard_attachScript.NextTurn();

        chessBoard_attachScript.ClearHighlights();
    }


    public void SetCoords(int x, int y)
    {
        Highlighter_X = x;
        Highlighter_Y = y;
    }

    public void SetReference(GameObject obj)
    {
        referenceObject = obj;
        chessPieces_attachScript = obj.GetComponent<ChessPlayerPlacementHandler>();
    }

}