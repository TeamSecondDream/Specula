using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Specula.Graphics;

namespace Specula;

// ReSharper disable class MemberCanBePrivate.Global
public class STexture {
    public int Width { get; private set; }
    public int Height { get; private set; }
    public Vector2 Center { get; private set; }
    public readonly Texture2D Texture;
    public Rectangle ClipRect;
    public Vector2 DrawOffset;

    public STexture(Texture2D texture) {
        Texture = texture;
        DrawOffset = Vector2.Zero;
        ClipRect = new Rectangle(0, 0, Width, Height);
        Setup();
    }

    public STexture(int width, int height, params Color[] data) {
        Texture = new Texture2D(Engine.Graphics.GraphicsDevice, width, height);
        Texture.SetData(data);
        Setup();
    }

    private void Setup() {
        Width = Texture.Width;
        Height = Texture.Height;
        Center = new Vector2(Width / 2f, Height / 2f);
    }

    public void SetPixels(Color[] data) {
        Texture.SetData(data);
    }

    public Color[] GetPixels() {
        Color[] data = new Color[Width * Height];
        Texture.GetData(data);
        return data;
    }

    public Rectangle GetRelativeRect(Rectangle rect) {
        return GetRelativeRect(rect.X, rect.Y, rect.Width, rect.Height);
    }

    public Rectangle GetRelativeRect(int x, int y, int width, int height) {
        int atX = (int)(ClipRect.X - DrawOffset.X + x);
        int atY = (int)(ClipRect.Y - DrawOffset.Y + y);

        int rX = MathHelper.Clamp(atX, ClipRect.Left, ClipRect.Right);
        int rY = MathHelper.Clamp(atY, ClipRect.Top, ClipRect.Bottom);
        int rW = Math.Max(0, Math.Min(atX + width, ClipRect.Right) - rX);
        int rH = Math.Max(0, Math.Min(atY + height, ClipRect.Bottom) - rY);

        return new Rectangle(rX, rY, rW, rH);
    }

    #region Draw

    public void Draw(Vector2 position) {
#if DEBUG
        if (Texture.IsDisposed) throw new Exception("Texture is disposed!");
#endif
        Drawing.SpriteBatch.Draw(Texture, position, ClipRect, Color.White, 0, -DrawOffset, 1f, SpriteEffects.None, 0);
    }

    public void Draw(Vector2 position, Color color) {
#if DEBUG
        if (Texture.IsDisposed) throw new Exception("Texture is disposed!");
#endif
        Drawing.SpriteBatch.Draw(Texture, position, ClipRect, color, 0, -DrawOffset, 1f, SpriteEffects.None, 0);
    }

    public void Draw(Vector2 position, Vector2 origin) {
#if DEBUG
        if (Texture.IsDisposed) throw new Exception("Texture is disposed!");
#endif
        Drawing.SpriteBatch.Draw(Texture, position, ClipRect, Color.White, 0, origin - DrawOffset, 1f,
            SpriteEffects.None, 0);
    }

    public void Draw(Vector2 position, Vector2 origin, Color color) {
#if DEBUG
        if (Texture.IsDisposed) throw new Exception("Texture is disposed!");
#endif
        Drawing.SpriteBatch.Draw(Texture, position, ClipRect, color, 0, origin - DrawOffset, 1f, SpriteEffects.None, 0);
    }

    public void Draw(Vector2 position, Vector2 origin, Color color, float scale) {
#if DEBUG
        if (Texture.IsDisposed) throw new Exception("Texture is disposed!");
#endif
        Drawing.SpriteBatch.Draw(Texture, position, ClipRect, color, 0, origin - DrawOffset, scale, SpriteEffects.None,
            0);
    }

    public void Draw(Vector2 position, Vector2 origin, Color color, float scale, float rotation) {
#if DEBUG
        if (Texture.IsDisposed) throw new Exception("Texture is disposed!");
#endif
        Drawing.SpriteBatch.Draw(Texture, position, ClipRect, color, rotation, origin - DrawOffset, scale,
            SpriteEffects.None, 0);
    }

    public void Draw(Vector2 position, Vector2 origin, Color color, float scale, float rotation, SpriteEffects flip) {
#if DEBUG
        if (Texture.IsDisposed) throw new Exception("Texture is disposed!");
#endif
        Drawing.SpriteBatch.Draw(Texture, position, ClipRect, color, rotation, origin - DrawOffset, scale, flip, 0);
    }

    public void Draw(Vector2 position, Vector2 origin, Color color, Vector2 scale) {
#if DEBUG
        if (Texture.IsDisposed) throw new Exception("Texture is disposed!");
#endif
        Drawing.SpriteBatch.Draw(Texture, position, ClipRect, color, 0, origin - DrawOffset, scale, SpriteEffects.None,
            0);
    }

    public void Draw(Vector2 position, Vector2 origin, Color color, Vector2 scale, float rotation) {
#if DEBUG
        if (Texture.IsDisposed) throw new Exception("Texture is disposed!");
#endif
        Drawing.SpriteBatch.Draw(Texture, position, ClipRect, color, rotation, origin - DrawOffset, scale,
            SpriteEffects.None, 0);
    }

    public void Draw(Vector2 position, Vector2 origin, Color color, Vector2 scale, float rotation, SpriteEffects flip) {
#if DEBUG
        if (Texture.IsDisposed) throw new Exception("Texture is disposed!");
#endif
        Drawing.SpriteBatch.Draw(Texture, position, ClipRect, color, rotation, origin - DrawOffset, scale, flip, 0);
    }

    public void Draw(Vector2 position, Vector2 origin, Color color, Vector2 scale, float rotation, Rectangle clip) {
#if DEBUG
        if (Texture.IsDisposed) throw new Exception("Texture is disposed!");
#endif
        Drawing.SpriteBatch.Draw(Texture, position, GetRelativeRect(clip), color, rotation, origin - DrawOffset, scale,
            SpriteEffects.None, 0);
    }

    #endregion

    #region Draw Centered

    public void DrawCentered(Vector2 position) {
#if DEBUG
        if (Texture.IsDisposed) throw new Exception("Texture is disposed!");
#endif
        Drawing.SpriteBatch.Draw(Texture, position, ClipRect, Color.White, 0, Center - DrawOffset, 1f,
            SpriteEffects.None, 0);
    }

    public void DrawCentered(Vector2 position, Color color) {
#if DEBUG
        if (Texture.IsDisposed) throw new Exception("Texture is disposed!");
#endif
        Drawing.SpriteBatch.Draw(Texture, position, ClipRect, color, 0, Center - DrawOffset, 1f, SpriteEffects.None, 0);
    }

    public void DrawCentered(Vector2 position, Color color, float scale) {
#if DEBUG
        if (Texture.IsDisposed) throw new Exception("Texture is disposed!");
#endif
        Drawing.SpriteBatch.Draw(Texture, position, ClipRect, color, 0, Center - DrawOffset, scale, SpriteEffects.None,
            0);
    }

    public void DrawCentered(Vector2 position, Color color, float scale, float rotation) {
#if DEBUG
        if (Texture.IsDisposed) throw new Exception("Texture is disposed!");
#endif
        Drawing.SpriteBatch.Draw(Texture, position, ClipRect, color, rotation, Center - DrawOffset, scale,
            SpriteEffects.None, 0);
    }

    public void DrawCentered(Vector2 position, Color color, float scale, float rotation, SpriteEffects flip) {
#if DEBUG
        if (Texture.IsDisposed) throw new Exception("Texture is disposed!");
#endif
        Drawing.SpriteBatch.Draw(Texture, position, ClipRect, color, rotation, Center - DrawOffset, scale, flip, 0);
    }

    public void DrawCentered(Vector2 position, Color color, Vector2 scale) {
#if DEBUG
        if (Texture.IsDisposed) throw new Exception("Texture is disposed!");
#endif
        Drawing.SpriteBatch.Draw(Texture, position, ClipRect, color, 0, Center - DrawOffset, scale, SpriteEffects.None,
            0);
    }

    public void DrawCentered(Vector2 position, Color color, Vector2 scale, float rotation) {
#if DEBUG
        if (Texture.IsDisposed) throw new Exception("Texture is disposed!");
#endif
        Drawing.SpriteBatch.Draw(Texture, position, ClipRect, color, rotation, Center - DrawOffset, scale,
            SpriteEffects.None, 0);
    }

    public void DrawCentered(Vector2 position, Color color, Vector2 scale, float rotation, SpriteEffects flip) {
#if DEBUG
        if (Texture.IsDisposed) throw new Exception("Texture is disposed!");
#endif
        Drawing.SpriteBatch.Draw(Texture, position, ClipRect, color, rotation, Center - DrawOffset, scale, flip, 0);
    }

    #endregion

    #region Draw Justified

    public void DrawJustified(Vector2 position, Vector2 justify) {
#if DEBUG
        if (Texture.IsDisposed) throw new Exception("Texture is disposed!");
#endif
        Drawing.SpriteBatch.Draw(Texture, position, ClipRect, Color.White, 0,
            new Vector2(Width * justify.X, Height * justify.Y) - DrawOffset, 1f, SpriteEffects.None, 0);
    }

    public void DrawJustified(Vector2 position, Vector2 justify, Color color) {
#if DEBUG
        if (Texture.IsDisposed) throw new Exception("Texture is disposed!");
#endif
        Drawing.SpriteBatch.Draw(Texture, position, ClipRect, color, 0,
            new Vector2(Width * justify.X, Height * justify.Y) - DrawOffset, 1f, SpriteEffects.None, 0);
    }

    public void DrawJustified(Vector2 position, Vector2 justify, Color color, float scale) {
#if DEBUG
        if (Texture.IsDisposed) throw new Exception("Texture is disposed!");
#endif
        Drawing.SpriteBatch.Draw(Texture, position, ClipRect, color, 0,
            new Vector2(Width * justify.X, Height * justify.Y) - DrawOffset, scale, SpriteEffects.None, 0);
    }

    public void DrawJustified(Vector2 position, Vector2 justify, Color color, float scale, float rotation) {
#if DEBUG
        if (Texture.IsDisposed) throw new Exception("Texture is disposed!");
#endif
        Drawing.SpriteBatch.Draw(Texture, position, ClipRect, color, rotation,
            new Vector2(Width * justify.X, Height * justify.Y) - DrawOffset, scale, SpriteEffects.None, 0);
    }

    public void DrawJustified(Vector2 position, Vector2 justify, Color color, float scale, float rotation,
        SpriteEffects flip) {
#if DEBUG
        if (Texture.IsDisposed) throw new Exception("Texture is disposed!");
#endif
        Drawing.SpriteBatch.Draw(Texture, position, ClipRect, color, rotation,
            new Vector2(Width * justify.X, Height * justify.Y) - DrawOffset, scale, flip, 0);
    }

    public void DrawJustified(Vector2 position, Vector2 justify, Color color, Vector2 scale) {
#if DEBUG
        if (Texture.IsDisposed) throw new Exception("Texture is disposed!");
#endif
        Drawing.SpriteBatch.Draw(Texture, position, ClipRect, color, 0,
            new Vector2(Width * justify.X, Height * justify.Y) - DrawOffset, scale, SpriteEffects.None, 0);
    }

    public void DrawJustified(Vector2 position, Vector2 justify, Color color, Vector2 scale, float rotation) {
#if DEBUG
        if (Texture.IsDisposed) throw new Exception("Texture is disposed!");
#endif
        Drawing.SpriteBatch.Draw(Texture, position, ClipRect, color, rotation,
            new Vector2(Width * justify.X, Height * justify.Y) - DrawOffset, scale, SpriteEffects.None, 0);
    }

    public void DrawJustified(Vector2 position, Vector2 justify, Color color, Vector2 scale, float rotation,
        SpriteEffects flip) {
#if DEBUG
        if (Texture.IsDisposed) throw new Exception("Texture is disposed!");
#endif
        Drawing.SpriteBatch.Draw(Texture, position, ClipRect, color, rotation,
            new Vector2(Width * justify.X, Height * justify.Y) - DrawOffset, scale, flip, 0);
    }

    #endregion

    #region Draw Outline

    public void DrawOutline(Vector2 position) {
#if DEBUG
        if (Texture.IsDisposed) throw new Exception("Texture is disposed!");
#endif
        for (var i = -1; i <= 1; i++)
        for (var j = -1; j <= 1; j++)
            if (i != 0 || j != 0)
                Drawing.SpriteBatch.Draw(Texture, position + new Vector2(i, j), ClipRect, Color.Black, 0, -DrawOffset,
                    1f, SpriteEffects.None, 0);

        Drawing.SpriteBatch.Draw(Texture, position, ClipRect, Color.White, 0, -DrawOffset, 1f, SpriteEffects.None, 0);
    }

    public void DrawOutline(Vector2 position, Vector2 origin) {
#if DEBUG
        if (Texture.IsDisposed) throw new Exception("Texture is disposed!");
#endif
        for (var i = -1; i <= 1; i++)
        for (var j = -1; j <= 1; j++)
            if (i != 0 || j != 0)
                Drawing.SpriteBatch.Draw(Texture, position + new Vector2(i, j), ClipRect, Color.Black, 0,
                    origin - DrawOffset, 1f, SpriteEffects.None, 0);

        Drawing.SpriteBatch.Draw(Texture, position, ClipRect, Color.White, 0, origin - DrawOffset, 1f,
            SpriteEffects.None, 0);
    }

    public void DrawOutline(Vector2 position, Vector2 origin, Color color) {
#if DEBUG
        if (Texture.IsDisposed) throw new Exception("Texture is disposed!");
#endif
        for (var i = -1; i <= 1; i++)
        for (var j = -1; j <= 1; j++)
            if (i != 0 || j != 0)
                Drawing.SpriteBatch.Draw(Texture, position + new Vector2(i, j), ClipRect, Color.Black, 0,
                    origin - DrawOffset, 1f, SpriteEffects.None, 0);

        Drawing.SpriteBatch.Draw(Texture, position, ClipRect, color, 0, origin - DrawOffset, 1f, SpriteEffects.None, 0);
    }

    public void DrawOutline(Vector2 position, Vector2 origin, Color color, float scale) {
#if DEBUG
        if (Texture.IsDisposed) throw new Exception("Texture is disposed!");
#endif
        for (var i = -1; i <= 1; i++)
        for (var j = -1; j <= 1; j++)
            if (i != 0 || j != 0)
                Drawing.SpriteBatch.Draw(Texture, position + new Vector2(i, j), ClipRect, Color.Black, 0,
                    origin - DrawOffset, scale, SpriteEffects.None, 0);

        Drawing.SpriteBatch.Draw(Texture, position, ClipRect, color, 0, origin - DrawOffset, scale, SpriteEffects.None,
            0);
    }

    public void DrawOutline(Vector2 position, Vector2 origin, Color color, float scale, float rotation) {
#if DEBUG
        if (Texture.IsDisposed) throw new Exception("Texture is disposed!");
#endif
        for (var i = -1; i <= 1; i++)
        for (var j = -1; j <= 1; j++)
            if (i != 0 || j != 0)
                Drawing.SpriteBatch.Draw(Texture, position + new Vector2(i, j), ClipRect, Color.Black, rotation,
                    origin - DrawOffset, scale, SpriteEffects.None, 0);

        Drawing.SpriteBatch.Draw(Texture, position, ClipRect, color, rotation, origin - DrawOffset, scale,
            SpriteEffects.None, 0);
    }

    public void DrawOutline(Vector2 position, Vector2 origin, Color color, float scale, float rotation,
        SpriteEffects flip) {
#if DEBUG
        if (Texture.IsDisposed) throw new Exception("Texture is disposed!");
#endif
        for (var i = -1; i <= 1; i++)
        for (var j = -1; j <= 1; j++)
            if (i != 0 || j != 0)
                Drawing.SpriteBatch.Draw(Texture, position + new Vector2(i, j), ClipRect, Color.Black, rotation,
                    origin - DrawOffset, scale, flip, 0);

        Drawing.SpriteBatch.Draw(Texture, position, ClipRect, color, rotation, origin - DrawOffset, scale, flip, 0);
    }

    public void DrawOutline(Vector2 position, Vector2 origin, Color color, Vector2 scale) {
#if DEBUG
        if (Texture.IsDisposed) throw new Exception("Texture is disposed!");
#endif
        for (var i = -1; i <= 1; i++)
        for (var j = -1; j <= 1; j++)
            if (i != 0 || j != 0)
                Drawing.SpriteBatch.Draw(Texture, position + new Vector2(i, j), ClipRect, Color.Black, 0,
                    origin - DrawOffset, scale, SpriteEffects.None, 0);

        Drawing.SpriteBatch.Draw(Texture, position, ClipRect, color, 0, origin - DrawOffset, scale, SpriteEffects.None,
            0);
    }

    public void DrawOutline(Vector2 position, Vector2 origin, Color color, Vector2 scale, float rotation) {
#if DEBUG
        if (Texture.IsDisposed) throw new Exception("Texture is disposed!");
#endif
        for (var i = -1; i <= 1; i++)
        for (var j = -1; j <= 1; j++)
            if (i != 0 || j != 0)
                Drawing.SpriteBatch.Draw(Texture, position + new Vector2(i, j), ClipRect, Color.Black, rotation,
                    origin - DrawOffset, scale, SpriteEffects.None, 0);

        Drawing.SpriteBatch.Draw(Texture, position, ClipRect, color, rotation, origin - DrawOffset, scale,
            SpriteEffects.None, 0);
    }

    public void DrawOutline(Vector2 position, Vector2 origin, Color color, Vector2 scale, float rotation,
        SpriteEffects flip) {
#if DEBUG
        if (Texture.IsDisposed) throw new Exception("Texture is disposed!");
#endif
        for (var i = -1; i <= 1; i++)
        for (var j = -1; j <= 1; j++)
            if (i != 0 || j != 0)
                Drawing.SpriteBatch.Draw(Texture, position + new Vector2(i, j), ClipRect, Color.Black, rotation,
                    origin - DrawOffset, scale, flip, 0);

        Drawing.SpriteBatch.Draw(Texture, position, ClipRect, color, rotation, origin - DrawOffset, scale, flip, 0);
    }

    #endregion

    #region Draw Outline Centered

    public void DrawOutlineCentered(Vector2 position) {
#if DEBUG
        if (Texture.IsDisposed) throw new Exception("Texture is disposed!");
#endif
        for (var i = -1; i <= 1; i++)
        for (var j = -1; j <= 1; j++)
            if (i != 0 || j != 0)
                Drawing.SpriteBatch.Draw(Texture, position + new Vector2(i, j), ClipRect, Color.Black, 0,
                    Center - DrawOffset, 1f, SpriteEffects.None, 0);

        Drawing.SpriteBatch.Draw(Texture, position, ClipRect, Color.White, 0, Center - DrawOffset, 1f,
            SpriteEffects.None, 0);
    }

    public void DrawOutlineCentered(Vector2 position, Color color) {
#if DEBUG
        if (Texture.IsDisposed) throw new Exception("Texture is disposed!");
#endif
        for (var i = -1; i <= 1; i++)
        for (var j = -1; j <= 1; j++)
            if (i != 0 || j != 0)
                Drawing.SpriteBatch.Draw(Texture, position + new Vector2(i, j), ClipRect, Color.Black, 0,
                    Center - DrawOffset, 1f, SpriteEffects.None, 0);

        Drawing.SpriteBatch.Draw(Texture, position, ClipRect, color, 0, Center - DrawOffset, 1f, SpriteEffects.None, 0);
    }

    public void DrawOutlineCentered(Vector2 position, Color color, float scale) {
#if DEBUG
        if (Texture.IsDisposed) throw new Exception("Texture is disposed!");
#endif
        for (var i = -1; i <= 1; i++)
        for (var j = -1; j <= 1; j++)
            if (i != 0 || j != 0)
                Drawing.SpriteBatch.Draw(Texture, position + new Vector2(i, j), ClipRect, Color.Black, 0,
                    Center - DrawOffset, scale, SpriteEffects.None, 0);

        Drawing.SpriteBatch.Draw(Texture, position, ClipRect, color, 0, Center - DrawOffset, scale, SpriteEffects.None,
            0);
    }

    public void DrawOutlineCentered(Vector2 position, Color color, float scale, float rotation) {
#if DEBUG
        if (Texture.IsDisposed) throw new Exception("Texture is disposed!");
#endif
        for (var i = -1; i <= 1; i++)
        for (var j = -1; j <= 1; j++)
            if (i != 0 || j != 0)
                Drawing.SpriteBatch.Draw(Texture, position + new Vector2(i, j), ClipRect, Color.Black, rotation,
                    Center - DrawOffset, scale, SpriteEffects.None, 0);

        Drawing.SpriteBatch.Draw(Texture, position, ClipRect, color, rotation, Center - DrawOffset, scale,
            SpriteEffects.None, 0);
    }

    public void DrawOutlineCentered(Vector2 position, Color color, float scale, float rotation, SpriteEffects flip) {
#if DEBUG
        if (Texture.IsDisposed) throw new Exception("Texture is disposed!");
#endif
        for (var i = -1; i <= 1; i++)
        for (var j = -1; j <= 1; j++)
            if (i != 0 || j != 0)
                Drawing.SpriteBatch.Draw(Texture, position + new Vector2(i, j), ClipRect, Color.Black, rotation,
                    Center - DrawOffset, scale, flip, 0);

        Drawing.SpriteBatch.Draw(Texture, position, ClipRect, color, rotation, Center - DrawOffset, scale, flip, 0);
    }

    public void DrawOutlineCentered(Vector2 position, Color color, Vector2 scale) {
#if DEBUG
        if (Texture.IsDisposed) throw new Exception("Texture is disposed!");
#endif
        for (var i = -1; i <= 1; i++)
        for (var j = -1; j <= 1; j++)
            if (i != 0 || j != 0)
                Drawing.SpriteBatch.Draw(Texture, position + new Vector2(i, j), ClipRect, Color.Black, 0,
                    Center - DrawOffset, scale, SpriteEffects.None, 0);

        Drawing.SpriteBatch.Draw(Texture, position, ClipRect, color, 0, Center - DrawOffset, scale, SpriteEffects.None,
            0);
    }

    public void DrawOutlineCentered(Vector2 position, Color color, Vector2 scale, float rotation) {
#if DEBUG
        if (Texture.IsDisposed) throw new Exception("Texture is disposed!");
#endif
        for (var i = -1; i <= 1; i++)
        for (var j = -1; j <= 1; j++)
            if (i != 0 || j != 0)
                Drawing.SpriteBatch.Draw(Texture, position + new Vector2(i, j), ClipRect, Color.Black, rotation,
                    Center - DrawOffset, scale, SpriteEffects.None, 0);

        Drawing.SpriteBatch.Draw(Texture, position, ClipRect, color, rotation, Center - DrawOffset, scale,
            SpriteEffects.None, 0);
    }

    public void DrawOutlineCentered(Vector2 position, Color color, Vector2 scale, float rotation, SpriteEffects flip) {
#if DEBUG
        if (Texture.IsDisposed) throw new Exception("Texture is disposed!");
#endif
        for (var i = -1; i <= 1; i++)
        for (var j = -1; j <= 1; j++)
            if (i != 0 || j != 0)
                Drawing.SpriteBatch.Draw(Texture, position + new Vector2(i, j), ClipRect, Color.Black, rotation,
                    Center - DrawOffset, scale, flip, 0);

        Drawing.SpriteBatch.Draw(Texture, position, ClipRect, color, rotation, Center - DrawOffset, scale, flip, 0);
    }

    #endregion

    #region Draw Outline Justified

    public void DrawOutlineJustified(Vector2 position, Vector2 justify) {
#if DEBUG
        if (Texture.IsDisposed) throw new Exception("Texture is disposed!");
#endif
        for (var i = -1; i <= 1; i++)
        for (var j = -1; j <= 1; j++)
            if (i != 0 || j != 0)
                Drawing.SpriteBatch.Draw(Texture, position + new Vector2(i, j), ClipRect, Color.Black, 0,
                    new Vector2(Width * justify.X, Height * justify.Y) - DrawOffset, 1f, SpriteEffects.None, 0);

        Drawing.SpriteBatch.Draw(Texture, position, ClipRect, Color.White, 0,
            new Vector2(Width * justify.X, Height * justify.Y) - DrawOffset, 1f, SpriteEffects.None, 0);
    }

    public void DrawOutlineJustified(Vector2 position, Vector2 justify, Color color) {
#if DEBUG
        if (Texture.IsDisposed) throw new Exception("Texture is disposed!");
#endif
        for (var i = -1; i <= 1; i++)
        for (var j = -1; j <= 1; j++)
            if (i != 0 || j != 0)
                Drawing.SpriteBatch.Draw(Texture, position + new Vector2(i, j), ClipRect, Color.Black, 0,
                    new Vector2(Width * justify.X, Height * justify.Y) - DrawOffset, 1f, SpriteEffects.None, 0);

        Drawing.SpriteBatch.Draw(Texture, position, ClipRect, color, 0,
            new Vector2(Width * justify.X, Height * justify.Y) - DrawOffset, 1f, SpriteEffects.None, 0);
    }

    public void DrawOutlineJustified(Vector2 position, Vector2 justify, Color color, float scale) {
#if DEBUG
        if (Texture.IsDisposed) throw new Exception("Texture is disposed!");
#endif
        for (var i = -1; i <= 1; i++)
        for (var j = -1; j <= 1; j++)
            if (i != 0 || j != 0)
                Drawing.SpriteBatch.Draw(Texture, position + new Vector2(i, j), ClipRect, Color.Black, 0,
                    new Vector2(Width * justify.X, Height * justify.Y) - DrawOffset, scale, SpriteEffects.None, 0);

        Drawing.SpriteBatch.Draw(Texture, position, ClipRect, color, 0,
            new Vector2(Width * justify.X, Height * justify.Y) - DrawOffset, scale, SpriteEffects.None, 0);
    }

    public void DrawOutlineJustified(Vector2 position, Vector2 justify, Color color, float scale, float rotation) {
#if DEBUG
        if (Texture.IsDisposed) throw new Exception("Texture is disposed!");
#endif
        for (var i = -1; i <= 1; i++)
        for (var j = -1; j <= 1; j++)
            if (i != 0 || j != 0)
                Drawing.SpriteBatch.Draw(Texture, position + new Vector2(i, j), ClipRect, Color.Black, rotation,
                    new Vector2(Width * justify.X, Height * justify.Y) - DrawOffset, scale, SpriteEffects.None, 0);

        Drawing.SpriteBatch.Draw(Texture, position, ClipRect, color, rotation,
            new Vector2(Width * justify.X, Height * justify.Y) - DrawOffset, scale, SpriteEffects.None, 0);
    }

    public void DrawOutlineJustified(Vector2 position, Vector2 justify, Color color, float scale, float rotation,
        SpriteEffects flip) {
#if DEBUG
        if (Texture.IsDisposed) throw new Exception("Texture is disposed!");
#endif
        for (var i = -1; i <= 1; i++)
        for (var j = -1; j <= 1; j++)
            if (i != 0 || j != 0)
                Drawing.SpriteBatch.Draw(Texture, position + new Vector2(i, j), ClipRect, Color.Black, rotation,
                    new Vector2(Width * justify.X, Height * justify.Y) - DrawOffset, scale, flip, 0);

        Drawing.SpriteBatch.Draw(Texture, position, ClipRect, color, rotation,
            new Vector2(Width * justify.X, Height * justify.Y) - DrawOffset, scale, flip, 0);
    }

    public void DrawOutlineJustified(Vector2 position, Vector2 justify, Color color, Vector2 scale) {
#if DEBUG
        if (Texture.IsDisposed) throw new Exception("Texture is disposed!");
#endif
        for (var i = -1; i <= 1; i++)
        for (var j = -1; j <= 1; j++)
            if (i != 0 || j != 0)
                Drawing.SpriteBatch.Draw(Texture, position + new Vector2(i, j), ClipRect, Color.Black, 0,
                    new Vector2(Width * justify.X, Height * justify.Y) - DrawOffset, scale, SpriteEffects.None, 0);

        Drawing.SpriteBatch.Draw(Texture, position, ClipRect, color, 0,
            new Vector2(Width * justify.X, Height * justify.Y) - DrawOffset, scale, SpriteEffects.None, 0);
    }

    public void DrawOutlineJustified(Vector2 position, Vector2 justify, Color color, Vector2 scale, float rotation) {
#if DEBUG
        if (Texture.IsDisposed) throw new Exception("Texture is disposed!");
#endif
        for (var i = -1; i <= 1; i++)
        for (var j = -1; j <= 1; j++)
            if (i != 0 || j != 0)
                Drawing.SpriteBatch.Draw(Texture, position + new Vector2(i, j), ClipRect, Color.Black, rotation,
                    new Vector2(Width * justify.X, Height * justify.Y) - DrawOffset, scale, SpriteEffects.None, 0);

        Drawing.SpriteBatch.Draw(Texture, position, ClipRect, color, rotation,
            new Vector2(Width * justify.X, Height * justify.Y) - DrawOffset, scale, SpriteEffects.None, 0);
    }

    public void DrawOutlineJustified(Vector2 position, Vector2 justify, Color color, Vector2 scale, float rotation,
        SpriteEffects flip) {
#if DEBUG
        if (Texture.IsDisposed) throw new Exception("Texture is disposed!");
#endif
        for (var i = -1; i <= 1; i++)
        for (var j = -1; j <= 1; j++)
            if (i != 0 || j != 0)
                Drawing.SpriteBatch.Draw(Texture, position + new Vector2(i, j), ClipRect, Color.Black, rotation,
                    new Vector2(Width * justify.X, Height * justify.Y) - DrawOffset, scale, flip, 0);

        Drawing.SpriteBatch.Draw(Texture, position, ClipRect, color, rotation,
            new Vector2(Width * justify.X, Height * justify.Y) - DrawOffset, scale, flip, 0);
    }

    #endregion
}