using UnityEngine;

namespace TaigaGames.SineysArkanoid.Utils
{
    public static class TextureScaler
    {
        public static Texture2D Scale(Texture2D source, int width, int height, FilterMode filterMode = FilterMode.Trilinear)
        {
            Rect textureRect = new(0, 0, width, height);
            
            source.filterMode = filterMode;
            source.Apply(true);

            RenderTexture rtt = new(width, height, 32);

            Graphics.SetRenderTarget(rtt);

            GL.LoadPixelMatrix(0, 1, 1, 0);
            GL.Clear(true, true, Color.clear);
            Graphics.DrawTexture(new Rect(0, 0, 1, 1), source);

            Texture2D result = new(width, height, TextureFormat.ARGB32, false);
            result.Reinitialize(width, height);
            result.ReadPixels(textureRect, 0, 0, true);
            return result;
        }
    }
}