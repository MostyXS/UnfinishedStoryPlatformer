using UnityEngine;

namespace Game.Utils.Editor
{
    public class EditorZoomArea
    {
        private const float kEditorWindowTabHeight = 21f;
        private static Matrix4x4 previousGUIMatrix;

        public static Rect Begin(float zoomScale, Rect screenCoordsArea)
        {
            GUI.EndGroup();

            Rect clippedArea = screenCoordsArea.ScaleSizeBy(1f / zoomScale, screenCoordsArea.TopLeft());
            clippedArea.y += kEditorWindowTabHeight;
            GUI.BeginGroup(clippedArea);

            previousGUIMatrix = GUI.matrix;
            Matrix4x4 translation = Matrix4x4.TRS(clippedArea.TopLeft(), Quaternion.identity, Vector3.one);
            Matrix4x4 scale = Matrix4x4.Scale(new Vector3(zoomScale, zoomScale, 1f));
            GUI.matrix = translation * scale * translation.inverse * GUI.matrix;

            return clippedArea;
        }

        public static void  End()
        {
            GUI.matrix = previousGUIMatrix;
            GUI.EndGroup();
            GUI.BeginGroup(new Rect(0f, kEditorWindowTabHeight, Screen.width, Screen.height));
        }

    }
}
