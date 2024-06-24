using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public bool IsPlayerTile;
    public bool IsTargetTile;
    public bool IsSelectableTile;
    public bool IsWalkable;

    Material _mat;

    List<Tile> _adjacent = new();

    void Start() {
      _mat = GetComponent<Renderer>().material;  
    }

    void Update() {
        if (IsPlayerTile)           _mat.color = Color.green;
        else if (IsTargetTile)      _mat.color = Color.yellow;
        else if (IsSelectableTile)  _mat.color = Color.blue;
        else if (IsWalkable)        _mat.color = Color.green;
        else                        _mat.color = Color.white;
    }    

    public void FindNeighbors() {
        _adjacent.Clear();

        Vector3[] dirs = {
            Vector3.left,
            Vector3.right,
            Vector3.forward,
            Vector3.back,
        };

        Vector3 half = Vector3.one * 0.25f;

        foreach (var dir in dirs) {
            Collider[] cols = Physics.OverlapBox(transform.position + dir, half);

            foreach (var tile in cols) {
                if (!tile.TryGetComponent(out Tile t)) continue;
                if (tile.transform.position.y > 1) continue; // band-aid fix. tiles must have 0 as y position

                if (t.IsWalkable) _adjacent.Add(t);
            }
        }
    }
}
