using System;
using UnityEngine;
using System.Diagnostics.CodeAnalysis;
using UnityEngine.Profiling;

[SuppressMessage("ReSharper", "MemberCanBePrivate.Global")]
public sealed class ChessBoardPlacementHandler : MonoBehaviour {
    
    [SerializeField] private GameObject[] _rowsArray;
    [SerializeField] private GameObject _highlightPrefab;
    public GameObject[,] positions = new GameObject[8, 8];
    private GameObject[,] _chessBoard;
    
    private bool gameOver = false;

    public bool choosePlayer_Black;
    bool currentPlayer_blackturn;

    internal static ChessBoardPlacementHandler Instance;

    private void Awake() {
        Instance = this;
        GenerateArray();
        if (choosePlayer_Black)
            currentPlayer_blackturn = true;
        else
            currentPlayer_blackturn = false;
    }

    private void GenerateArray() {
        _chessBoard = new GameObject[8, 8];
        for (var i = 0; i < 8; i++) {
            for (var j = 0; j < 8; j++) {
                _chessBoard[i, j] = _rowsArray[i].transform.GetChild(j).gameObject;
            }
        }
    }

    internal GameObject GetTile(int i, int j) {
        
        try {
            return _chessBoard[i, j];
        } catch (Exception) {
            Debug.LogError("Invalid row or column.");
            return null;
        }
    }

    internal void Highlight(int row, int col, GameObject piece) 
    {
        var tile = GetTile(row, col).transform;
        if (tile == null) {
            Debug.LogError("Invalid row or column.");
            return;
        }
        GameObject hl = Instantiate(_highlightPrefab, tile.transform.position, Quaternion.identity, tile.transform);
        Highlighter hlScript = hl.GetComponent<Highlighter>();
        hlScript.attack = false;
        hlScript.SetCoords(row, col);
        hlScript.SetReference(piece);
    }

    internal void HighRedlight(int row, int col, GameObject piece)
    {
        var tile = GetTile(row, col).transform;
        if (tile == null)
        {
            Debug.LogError("Invalid row or column.");
            return;
        }

        GameObject hl = Instantiate(_highlightPrefab, tile.transform.position, Quaternion.identity, tile.transform);
        Highlighter hlScript = hl.GetComponent<Highlighter>();
        hlScript.attack = true;
        hlScript.SetCoords(row, col);
        hlScript.SetReference(piece);
    }

    internal void ClearHighlights() {
        for (var i = 0; i < 8; i++) {
            for (var j = 0; j < 8; j++) {
                var tile = GetTile(i, j);
                if (tile.transform.childCount <= 0) continue;
                foreach (Transform childTransform in tile.transform) {
                    Destroy(childTransform.gameObject);
                }
            }
        }
        
    }


    #region Highlight Testing

    // private void Start() {
    //     StartCoroutine(Testing());
    // }

    // private IEnumerator Testing() {
    //     Highlight(2, 7);
    //     yield return new WaitForSeconds(1f);
    //
    //     ClearHighlights();
    //     Highlight(2, 7);
    //     Highlight(2, 6);
    //     Highlight(2, 5);
    //     Highlight(2, 4);
    //     yield return new WaitForSeconds(1f);
    //
    //     ClearHighlights();
    //     Highlight(7, 7);
    //     Highlight(2, 7);
    //     yield return new WaitForSeconds(1f);
    // }

    #endregion


    public bool PositionOnBoard(int x, int y)
    {
        if (x < 0 || y < 0 || x >= 8 || y >= 8) return false;
        return true;
    }

    public GameObject GetPosition(int x, int y)
    {
        return positions[x, y];
    }

    public void SetPositionEmpty(int x, int y)
    {
        positions[x, y] = null;
    }

    
    public void NextTurn()
    {
        if (currentPlayer_blackturn == false)
        {
            currentPlayer_blackturn = true;
        }
        else
        {
            currentPlayer_blackturn = false;
        }
    }

    public bool IsBlackPlayer_Turn()
    {
        return currentPlayer_blackturn;
    }


    public bool IsGameOver()
    {
        return gameOver;
    }
    
}