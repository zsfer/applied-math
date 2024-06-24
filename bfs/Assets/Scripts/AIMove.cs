using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AIMove : MonoBehaviour {
    public int TileMoveRange = 5;
    public float MoveSpeed;

    List<Tile> _tiles = new();

    float _halfHeight;

    protected void Initialize() {
        _tiles = GameObject.FindGameObjectsWithTag("Tile")
                .Select(g => g.GetComponent<Tile>())
                .ToList();

        _halfHeight = GetComponent<Collider>().bounds.extents.y;
    }

    void Reset() {
        _tiles.Clear();
    }
}
