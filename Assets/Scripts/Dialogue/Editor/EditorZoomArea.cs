using Game.Utils.Editor.Extensions;
using UnityEngine;

namespace Game.Utils.Editor
{
    public class EditorZoomArea
    {
        private const float KEditorWindowTabHeight = 21f;
        private static Matrix4x4 _previousGuiMatrix;

        public static Rect Begin(float zoomScale, Rect screenCoordsArea)
        {
            GUI.EndGroup();

            Rect clippedArea = screenCoordsArea.ScaleSizeBy(1f / zoomScale, screenCoordsArea.TopLeft());
            clippedArea.y += KEditorWindowTabHeight;
            GUI.BeginGroup(clippedArea);

            _previousGuiMatrix = GUI.matrix;
            Matrix4x4 translation = Matrix4x4.TRS(clippedArea.TopLeft(), Quaternion.identity, Vector3.one);
            Matrix4x4 scale = Matrix4x4.Scale(new Vector3(zoomScale, zoomScale, 1f));
            GUI.matrix = translation * scale * translation.inverse * GUI.matrix;

            return clippedArea;
        }

        public static void End()
        {
            GUI.matrix = _previousGuiMatrix;
            GUI.EndGroup();
            GUI.BeginGroup(new Rect(0f, KEditorWindowTabHeight, Screen.width, Screen.height));
        }
    }
}