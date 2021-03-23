using UnityEngine;

namespace Game.Utils.Editor.Extensions
{
    public static class RectExtensions
    {
        public static void SetWidth(this RectTransform rect, float width)
        {
            rect.anchoredPosition = new Vector2(width, rect.anchoredPosition.y);
        }

        public static void SetHeight(this RectTransform rect, float height)
        {
            rect.anchoredPosition = new Vector2(rect.anchoredPosition.x, height);
        }

        public static void SetAnchorAndSize(this RectTransform rect, Vector2 anchorMin, Vector2 anchorMax, Vector2 size)
        {
            rect.SetAnchor(anchorMin, anchorMax);
            rect.SetSize(size);
        }

        public static void SetMinAnchor(this RectTransform rect, float xMin, float yMin)
        {
            rect.anchorMin = new Vector2(xMin.ToAnchor(), yMin.ToAnchor());
        }

        public static void SetMaxAnchor(this RectTransform rect, float xMax, float yMax)
        {
            rect.anchorMax = new Vector2(xMax.ToAnchor(), yMax.ToAnchor());
        }

        public static void SetAnchor(this RectTransform rect, Vector2 anchorMin, Vector2 anchorMax)
        {
            rect.anchorMin = anchorMin.ToAnchor();
            rect.anchorMax = anchorMax.ToAnchor();
        }

        public static void SetAnchor(this RectTransform rect, Vector2 anchorBoth)
        {
            rect.anchorMin = anchorBoth;
            rect.anchorMax = anchorBoth;
        }

        public static void SetSize(this RectTransform rect, Vector2 size)
        {
            rect.sizeDelta = size;
        }

        public static void ResetSize(this RectTransform rect)
        {
            rect.sizeDelta = new Vector2(0, 0);
        }

        public static void ResetAnchor(this RectTransform rect)
        {
            rect.anchorMin = new Vector2(0, 0);
            rect.anchorMax = new Vector2(0, 0);
        }

        public static float ToAnchor(this float value)
        {
            return Mathf.Clamp(value, 0, 1f);
        }

        public static Vector2 ToAnchor(this Vector2 vector)
        {
            return new Vector2(vector.x.ToAnchor(), vector.y.ToAnchor());
        }

        public static Vector2 TopLeft(this Rect rect)
        {
            return new Vector2(rect.xMin, rect.yMin);
        }

        public static Rect ScaleSizeBy(this Rect rect, float scale)
        {
            return rect.ScaleSizeBy(scale, rect.center);
        }

        public static Rect ScaleSizeBy(this Rect rect, float scale, Vector2 pivotPoint)
        {
            Rect result = rect;
            result.x -= pivotPoint.x;
            result.y -= pivotPoint.y;
            result.xMin *= scale;
            result.xMax *= scale;
            result.yMin *= scale;
            result.yMax *= scale;
            result.x += pivotPoint.x;
            result.y += pivotPoint.y;
            return result;
        }

        public static Rect ScaleSizeBy(this Rect rect, Vector2 scale)
        {
            return rect.ScaleSizeBy(scale, rect.center);
        }

        public static Rect ScaleSizeBy(this Rect rect, Vector2 scale, Vector2 pivotPoint)
        {
            Rect result = rect;
            result.x -= pivotPoint.x;
            result.y -= pivotPoint.y;
            result.xMin *= scale.x;
            result.xMax *= scale.x;
            result.yMin *= scale.y;
            result.yMax *= scale.y;
            result.x += pivotPoint.x;
            result.y += pivotPoint.y;
            return result;
        }
    }
}