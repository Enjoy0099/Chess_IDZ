using System;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

namespace Chess.Scripts.Core
{
    public class ChessPlayerPlacementHandler : MonoBehaviour
    {
        ChessBoardPlacementHandler chessBoard_attachScript;

        [SerializeField] public int row, column;
        [SerializeField] public string chessPiece_name;


        public bool player_black;

        
        private void Start()
        {
            chessBoard_attachScript = ChessBoardPlacementHandler.Instance;
            
            chessPiece_name = gameObject.name;
            
            transform.position = chessBoard_attachScript.GetTile(row, column).transform.position;
            
            chessBoard_attachScript.positions[row, column] = this.gameObject;
        }

        private void OnMouseDown()
        {
            if (!chessBoard_attachScript.IsGameOver() && 
                chessBoard_attachScript.IsBlackPlayer_Turn() == this.player_black)
            {
                chessBoard_attachScript.ClearHighlights();
                InitiateMovePlates();
            }
        }


        public void InitiateMovePlates()
        {
            switch (this.chessPiece_name)
            {
                case "B_King":
                case "W_King":
                    SurroundMovePlate();
                    break;

                case "B_Queen":
                case "W_Queen":
                    LineMovePlate(1, 0);
                    LineMovePlate(0, 1);
                    LineMovePlate(1, 1);
                    LineMovePlate(-1, 0);
                    LineMovePlate(0, -1);
                    LineMovePlate(-1, -1);
                    LineMovePlate(-1, 1);
                    LineMovePlate(1, -1);
                    break;

                case "B_Knight":
                case "W_Knight":
                    LMovePlate();
                    break;

                case "B_Bishop":
                case "W_Bishop":
                    LineMovePlate(1, 1);
                    LineMovePlate(1, -1);
                    LineMovePlate(-1, 1);
                    LineMovePlate(-1, -1);
                    break;

                case "B_Rook":
                case "W_Rook":
                    LineMovePlate(1, 0);
                    LineMovePlate(0, 1);
                    LineMovePlate(-1, 0);
                    LineMovePlate(0, -1);
                    break;

                case "B_Pawn":
                    PawnMovePlate(this.row + 1, this.column);
                    break;
                case "W_Pawn":
                    PawnMovePlate(this.row - 1, this.column);
                    break;
            }
        }

        public void SurroundMovePlate()
        {
            for(int i = -1; i <= 1; i++)
            {
                for(int j = -1; j <= 1; j++)
                {
                    if (i == 0 && j == 0)
                        continue;
                    PointMovePlate(this.row + i, this.column + j);
                }
            }
        }

        public void LMovePlate()
        {
            int[] offsets = { 1, -1, 2, -2 };

            foreach (int i in offsets)
            {
                foreach (int j in offsets)
                {
                    if (Math.Abs(i) + Math.Abs(j) == 3)
                    {
                        PointMovePlate(this.row + i, this.column + j);
                    }
                }
            }
        }


        public void PointMovePlate(int x, int y)    
        {
            if (chessBoard_attachScript.PositionOnBoard(x, y))
            {
                GameObject moveablePosition_Object = chessBoard_attachScript.GetPosition(x, y);

                if (moveablePosition_Object == null)
                {
                    chessBoard_attachScript.Highlight(x, y, this.gameObject);
                }
                else 
                {
                    var targetPlayerPlacementHandler = moveablePosition_Object.GetComponent<ChessPlayerPlacementHandler>();

                    if (chessBoard_attachScript.IsBlackPlayer_Turn() != targetPlayerPlacementHandler.player_black)
                        chessBoard_attachScript.HighRedlight(x, y, this.gameObject);
                }
            }
        }


        public void LineMovePlate(int xIncrement, int yIncrement)
        {

            int x = this.row + xIncrement;
            int y = this.column + yIncrement;

            while (chessBoard_attachScript.PositionOnBoard(x, y) && chessBoard_attachScript.GetPosition(x, y) == null)
            {
                chessBoard_attachScript.Highlight(x, y, this.gameObject);
                x += xIncrement;
                y += yIncrement;
            }

            if (chessBoard_attachScript.PositionOnBoard(x, y))
            {
                var targetPlayerPlacementHandler = chessBoard_attachScript.GetPosition(x, y).GetComponent<ChessPlayerPlacementHandler>();

                if(targetPlayerPlacementHandler.player_black != chessBoard_attachScript.IsBlackPlayer_Turn())
                {
                    chessBoard_attachScript.HighRedlight(x, y, this.gameObject);
                }
            }
        }

        public void PawnMovePlate(int x, int y)
        {
            if (!chessBoard_attachScript.PositionOnBoard(x, y))
                return;

            var currentPosition_chessPiece = chessBoard_attachScript.GetPosition(x, y);

            if (currentPosition_chessPiece == null)
            {
                chessBoard_attachScript.Highlight(x, y, this.gameObject);
            }

            int[] yOffsetOptions = { 1, -1 };

            foreach (int yOffset in yOffsetOptions)
            {
                int newY = y + yOffset;

                if (chessBoard_attachScript.PositionOnBoard(x, newY))
                {
                    var newPosition = chessBoard_attachScript.GetPosition(x, newY);

                    if (newPosition != null)
                    {
                        var playerPlacementHandler = newPosition.GetComponent<ChessPlayerPlacementHandler>();

                        if (playerPlacementHandler != null && playerPlacementHandler.player_black != chessBoard_attachScript.IsBlackPlayer_Turn())
                        {
                            chessBoard_attachScript.HighRedlight(x, newY, this.gameObject);
                        }
                    }
                }
            }

            if (chessBoard_attachScript.PositionOnBoard(x, y + 1) && chessBoard_attachScript.GetPosition(x, y + 1) != null && chessBoard_attachScript.GetPosition(x, y + 1).GetComponent<ChessPlayerPlacementHandler>().player_black != chessBoard_attachScript.IsBlackPlayer_Turn())
                {
                    chessBoard_attachScript.HighRedlight(x, y + 1, this.gameObject);
                }

                if (chessBoard_attachScript.PositionOnBoard(x, y - 1) && chessBoard_attachScript.GetPosition(x, y - 1) != null && chessBoard_attachScript.GetPosition(x, y - 1).GetComponent<ChessPlayerPlacementHandler>().player_black != chessBoard_attachScript.IsBlackPlayer_Turn())
                {
                    chessBoard_attachScript.HighRedlight(x, y - 1, this.gameObject);
                }
        }


        public void SetPosition(GameObject obj)
        {

            //Overwrites either empty space or whatever was there
            chessBoard_attachScript.positions[GetXBoard(), GetYBoard()] = obj;

            transform.position = chessBoard_attachScript.GetTile(GetXBoard(), GetYBoard()).transform.position;
        }

        public int GetXBoard()
        {
            return this.row;
        }

        public int GetYBoard()
        {
            return this.column;
        }

        public void SetXYBoard(int x, int y)
        {
            this.row = x;
            this.column = y;
        }

    }
}
