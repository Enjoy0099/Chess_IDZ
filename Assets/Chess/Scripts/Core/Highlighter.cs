using Chess.Scripts.Core;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Highlighter : MonoBehaviour
{
    ChessPlayerPlacementHandler chessPieces_attachScript;
    ChessBoardPlacementHandler chessBoard_attachScript;
    GameObject referenceObject  = null;

    public bool attack = false;

    int Highlighter_X, Highlighter_Y;

    private void Start()
    {
        chessBoard_attachScript = ChessBoardPlacementHandler.Instance;

        if (attack)
            gameObject.GetComponent<SpriteRenderer>().color = new Color(1.0f, 0.0f, 0.0f, 1.0f);
    }

    public void OnMouseUp()
    {
        if (attack)
        {
            GameObject _attack = chessBoard_attachScript.GetPosition(Highlighter_X, Highlighter_Y);

            Destroy(_attack);
        }

        chessBoard_attachScript.SetPositionEmpty(chessPieces_attachScript.GetXBoard(),                                                  chessPieces_attachScript.GetYBoard());

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