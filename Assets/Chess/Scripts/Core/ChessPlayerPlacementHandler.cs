using System;
using UnityEngine;

namespace Chess.Scripts.Core
{
    public class ChessPlayerPlacementHandler : MonoBehaviour
    {
        [SerializeField] public int row, column;
        [SerializeField] public string gameobject_name;


        public bool player_black;

        
        private void Start()
        {
            gameobject_name = gameObject.name;
            transform.position = ChessBoardPlacementHandler.Instance.GetTile(row, column).transform.position;
        }

        private void OnMouseDown()
        {
            if(!ChessBoardPlacementHandler.Instance.IsGameOver() &&
                ChessBoardPlacementHandler.Instance.currentPlayer_black == player_black)
            {
                ChessBoardPlacementHandler.Instance.DestroyMovePlates();
                ChessBoardPlacementHandler.Instance.InitiateMovePlates();
                Debug.Log("black..");
            }
            else if (!ChessBoardPlacementHandler.Instance.IsGameOver() &&
                ChessBoardPlacementHandler.Instance.currentPlayer_black == !player_black)
            {
                ChessBoardPlacementHandler.Instance.DestroyMovePlates();
                ChessBoardPlacementHandler.Instance.InitiateMovePlates();
                Debug.Log("white..");
            }
            
            //Debug.Log(this.name);
            /*if (!GameManager_Script.GetComponent<GameManager>().IsGameOver() && GameManager_Script.GetComponent<GameManager>().GetCurrentPlayer() == player)
            {

                Debug.Log(this.name);
            }*/
        }
    }
}
