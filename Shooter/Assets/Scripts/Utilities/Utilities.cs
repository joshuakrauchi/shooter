using Unity.Mathematics;
using UnityEngine;

public static class Utilities
{
    public static readonly Rect ScreenRect = GetScreenRect();

    public static Vector3 OffscreenPosition = new Vector3(1000.0f, 1000.0f, 1000.0f);

    private const float OffscreenOffset = 1.0f;
    public const uint MaxTimeData = 500;
    public static Camera MainCamera { get; set; } = Camera.main;

    
    public static bool IsOffscreen(Vector2 position, Vector2 spriteExtents, Rect screenRect) =>
        position.x < screenRect.xMin - spriteExtents.x - OffscreenOffset || position.x > screenRect.xMax + spriteExtents.x + OffscreenOffset ||
        position.y < screenRect.yMin - spriteExtents.y - OffscreenOffset || position.y > screenRect.yMax + spriteExtents.y + OffscreenOffset;

    public static bool IsOutsideRect(float3 position, float3 spriteExtents, Rect rect)
    {
        return position.x < rect.xMin - spriteExtents.x - OffscreenOffset || position.x > rect.xMax + spriteExtents.x + OffscreenOffset ||
               position.y < rect.yMin - spriteExtents.y - OffscreenOffset || position.y > rect.yMax + spriteExtents.y + OffscreenOffset;
    }

    private static Rect GetScreenRect()
    {
        Camera mainCamera = Camera.main;

        if (!mainCamera) return Rect.zero;

        Vector3 bottomLeft = mainCamera.ScreenToWorldPoint(Vector2.zero);
        Vector3 topRight = mainCamera.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));

        return Rect.MinMaxRect(
            bottomLeft.x < 0 ? bottomLeft.x : -bottomLeft.x,
            bottomLeft.y < 0 ? bottomLeft.y : -bottomLeft.y,
            topRight.x > 0 ? topRight.x : -topRight.x,
            topRight.y > 0 ? topRight.y : -topRight.y);
    }

    public static bool IsSameSign(float first, float second)
    {
        return (long) ((ulong) first ^ (ulong) second) >= 0;
    }

    /**
     * Faster Line Segment Intersection
     * Franklin Antonio
     */
    public static bool GetLineIntersection(Vector2 line1Point1, Vector2 line1Point2, Vector2 line2Point1, Vector2 line2Point2, ref Vector2 intersection)
    {
        var Ax = line1Point2.x - line1Point1.x;
        var Bx = line2Point1.x - line2Point2.x;

        float lesserX1;
        float greaterX1;

        if (Ax < 0)
        {
            lesserX1 = line1Point2.x;
            greaterX1 = line1Point1.x;
        }
        else
        {
            lesserX1 = line1Point1.x;
            greaterX1 = line1Point2.x;
        }

        if (Bx > 0)
        {
            if (greaterX1 < line2Point2.x || line2Point1.x < lesserX1) return false;
        }
        else
        {
            if (greaterX1 < line2Point1.x || line2Point2.x < lesserX1) return false;
        }

        var Ay = line1Point2.y - line1Point1.y;
        var By = line2Point1.y - line2Point2.y;

        float lesserY1;
        float greaterY1;

        if (Ay < 0)
        {
            lesserY1 = line1Point2.y;
            greaterY1 = line1Point1.y;
        }
        else
        {
            lesserY1 = line1Point1.y;
            greaterY1 = line1Point2.y;
        }

        if (By > 0)
        {
            if (greaterY1 < line2Point2.y || line2Point1.y < lesserY1) return false;
        }
        else
        {
            if (greaterY1 < line2Point1.y || line2Point2.y < lesserY1) return false;
        }

        var Cx = line1Point1.x - line2Point1.x;
        var Cy = line1Point1.y - line2Point1.y;

        var f = Ay * Bx - Ax * By;

        if (f == 0) return false;

        var d = By * Cx - Bx * Cy;
        if (f > 0)
        {
            if (d < 0 || d > f) return false;
        }
        else
        {
            if (d > 0 || d < f) return false;
        }

        var e = Ax * Cy - Ay * Cx;
        if (f > 0)
        {
            if (e < 0 || e > f) return false;
        }
        else
        {
            if (e > 0 || e < f) return false;
        }

        var numerator = d * Ax;
        var offset = IsSameSign(numerator, f) ? f / 2 : -f / 2;
        intersection.x = line1Point1.x + (numerator + offset) / f;

        numerator = d * Ay;
        offset = IsSameSign(numerator, f) ? f / 2 : -f / 2;
        intersection.y = line1Point1.y + (numerator + offset) / f; /* intersection y */

        return true;
    }
}