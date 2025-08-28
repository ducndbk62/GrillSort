using UnityEngine;

public static class GridUtils
{
    public const float CELL_WIDTH = 1.5f;
    public const float CELL_HEIGHT = 1f;
    public const float SPACING_X = 0.2f;
    public const float SPACING_Y = 0.5f;

    public static readonly Vector3[] SLOT_POSITIONS =
{
        new Vector3( -0.45f, 0f, 0f),
        new Vector3( 0.0f, 0f, 0f),
        new Vector3(0.45f, 0f, 0f),
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
        if (gc == null || gc.Model == null) return new Vector2Int(-1, -1);
        float w = CELL_WIDTH;
        float h = CELL_HEIGHT;
        float sx = SPACING_X;
        float sy = SPACING_Y;
        Vector3 offset = worldPos - gc.Origin;
        int gx = Mathf.FloorToInt(offset.x / (w + sx));
        int gy = Mathf.FloorToInt(offset.y / (h + sy));
        if (gx < 0 || gy < 0 || gx >= gc.Model.Width || gy >= gc.Model.Height)
            return new Vector2Int(-1, -1);
        float lx = offset.x - gx * (w + sx);
        float ly = offset.y - gy * (h + sy);
        if (lx < 0f || lx > w || ly < 0f || ly > h)
            return new Vector2Int(-1, -1);
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

