using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridGenerator : MonoBehaviour
{
    [SerializeField] Vector2 _size;
    [SerializeField] GameObject _tile;

    readonly List<GameObject> _tiles = new();

    public void GenerateTiles() {
        ClearTiles();
        for (int x = 0; x < _size.x; x++) {
            for (int y = 0; y < _size.y; y++) {
                _tiles.Add(Instantiate(_tile, new Vector3(x, 0, y), Quaternion.identity));
            }
        }
    }
    
    void ClearTiles() {
        if (_tiles.Count == 0) return;

        foreach (var tile in _tiles) {
            DestroyImmediate(tile);
        }

        _tiles.Clear();
    }
}
