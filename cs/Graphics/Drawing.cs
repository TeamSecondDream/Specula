using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Specula.Content;
using Specula.Util;

namespace Specula.Graphics; 

public static class Drawing {
    public static SpriteBatch SpriteBatch { get; private set; }
    private static Texture2D Pixel { get; set; }
    public static Renderer Renderer { get; set; }

    public static Font Font { get; set; }
    
    internal static void Initialize() {
        SpriteBatch = new SpriteBatch(Engine.Graphics.GraphicsDevice);
        Resources.Load("assets/textures/pixel.png");
        Pixel = Resources.Textures["assets/textures/pixel.png"];
        Resources.Load("assets/fonts/default.json");
        Font = Resources.Fonts["assets/fonts/default.json"];
    }

    public static void DrawPoint(Vector2 pos, Color color) {
        SpriteBatch.Draw(Pixel, pos, color);
    }

    public static void DrawRectangleFilled(Rectangle rect, Color color) {
        SpriteBatch.Draw(Pixel, rect, null, color);
    }   
    public static void DrawRectangleFilled(SpriteBatch batch, Rectangle rect, Color color) {
        batch.Draw(Pixel, rect, null, color);
    }   
    
    public static void DrawRectangleFilled(Vector2 pos, Vector2 scale, Color color) {
        SpriteBatch.Draw(Pixel, new Rectangle((int)pos.X, (int)pos.Y, (int)scale.X, (int)scale.Y), null, color);
    }

    public static void DrawRectangle(Vector2 topLeft, Vector2 scale, Color color) {
        Vector2 bottomLeft = topLeft + new Vector2(0, scale.Y);
        Vector2 topRight = topLeft + new Vector2(scale.X, 0);
        Vector2 bottomRight = bottomLeft + new Vector2(scale.X, 0);
        
        DrawLine(topLeft, topRight, color);
        DrawLine(bottomLeft, bottomRight, color);
        DrawLine(topLeft, bottomLeft, color);
        DrawLine(topRight, bottomRight, color);
    }
    
    public static void DrawLine(Vector2 from, Vector2 to, Color color) {
        float angle = (float)Math.Atan2(to.Y - from.Y, to.X - from.X);
        int length = (int)Vector2.Distance(from, to);
        
        Console.WriteLine(length);
        DrawLine(from, length, angle, color);
    }

    public static void DrawLine(Vector2 from, int length, float angle, Color color) {
        SpriteBatch.Draw(Pixel, from, null, color, angle, Vector2.Zero, new Vector2(length, 1), SpriteEffects.None, 0);
        //SpriteBatch.Draw(Pixel, from, null, color, angle, from, SpriteEffects.None, 0);
    }

    public static void DrawString(string str, Vector2 pos, Color color) {
        int x = 0;
        int y = 0;
        foreach (char c in str) {
            if ("\n".Contains(c)) {
                x = 0;
                y += Font.LineHeight;
                continue;
            }

            if (c != ' ') {
                SpriteBatch.Draw(Font.Texture, pos + new Vector2(x, y), Font.GetRectangle(c), color);
            }

            x += Font.GetWidth(c) + Font.CharSpacing;
        }
    }
}