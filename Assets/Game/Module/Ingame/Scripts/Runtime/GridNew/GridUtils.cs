using UnityEngine;

public static class GridUtils
{
    public const int WIDTH = 3;
    public const int HEIGHT = 4;
    public const float CELL_WIDTH = 3f;
    public const float CELL_HEIGHT = 2.5f;
    public const float SPACING_X = 0.35f;
    public const float SPACING_Y = 1f;

    public static readonly Vector3[] SLOT_POSITIONS =
    {
        new Vector3( -1f, 0f, 0f),
        new Vector3( 0.0f, 0f, 0f),
        new Vector3(1f, 0f, 0f),
    };

    public static readonly Vector3[] SLOT_PLATES =
    {
        new Vector3( -0.65f, -0.1f, 0f),
        new Vector3( -0.1f, -0.1f, 0f),
        new Vector3(0.45f, -0.1f, 0f),
    };

    public static readonly Vector3[] SLOT_PLATES_CLEAR =
   {
        new Vector3(-0.5f, -0.1f, 0f),
        new Vector3(0f, -0.1f, 0f),
        new Vector3(0.5f, -0.1f, 0f),
    };

    public static Vector2Int WorldToGrid(Vector3 worldPos)
    {
        //var gc = GridController.Instance;
        //if (gc == null) return new Vector2Int(-1, -1);
        //float w = CELL_WIDTH;
        //float h = CELL_HEIGHT;
        //Vector3 offset = worldPos - gc.Origin;
        //int x = Mathf.FloorToInt(offset.x / (w + SPACING_X));
        //int y = Mathf.FloorToInt(offset.y / (h + SPACING_Y));
        //return new Vector2Int(x, y);

        var gc = GridController.Instance;
        if (gc == null) return new Vector2Int(-1, -1);
        float w = CELL_WIDTH;
        float h = CELL_HEIGHT;
        float sx = SPACING_X;
        float sy = SPACING_Y;
        Vector3 offset = worldPos - gc.Origin;
        int gx = Mathf.FloorToInt(offset.x / (w + sx));
        int gy = Mathf.FloorToInt(offset.y / (h + sy));
        if (gx < 0 || gy < 0 || gx >= WIDTH || gy >= HEIGHT)
            return new Vector2Int(-1, -1);
        float lx = offset.x - gx * (w + sx);
        float ly = offset.y - gy * (h + sy);
        if (lx < 0f || lx > w || ly < 0f || ly > h)
            return new Vector2Int(-1, -1);
        return new Vector2Int(gx, gy);

    }

    public static Vector2Int WorldToGridNearest(Vector3 worldPos)
    {
        var gc = GridController.Instance;
        if (gc == null) return new Vector2Int(-1, -1);
        float w = CELL_WIDTH;
        float h = CELL_HEIGHT;
        float sx = SPACING_X;
        float sy = SPACING_Y;
        Vector3 offset = worldPos - gc.Origin;
        int gx = Mathf.RoundToInt((offset.x - w * 0.5f) / (w + sx));
        int gy = Mathf.RoundToInt((offset.y - h * 0.5f) / (h + sy));
        gx = Mathf.Clamp(gx, 0, WIDTH - 1);
        gy = Mathf.Clamp(gy, 0, HEIGHT - 1);
        return new Vector2Int(gx, gy);
    }



    public static Vector3 GridToWorld(int x, int y)
    {
        var gc = GridController.Instance;
        if (gc == null) return Vector3.zero;
        float w = CELL_WIDTH;
        float h = CELL_HEIGHT;
        return gc.Origin + new Vector3(
            x * (w + SPACING_X) + w / 2f,
            y * (h + SPACING_Y) + h / 2f,
            0f
        );
    }

    public static Vector3 GridToWorld(Vector2Int gridPos) => GridToWorld(gridPos.x, gridPos.y);
}

