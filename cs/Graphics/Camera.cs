using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Specula.Graphics;

public class Camera {
    private Matrix _matrix = Matrix.Identity;
    private Matrix _inverse = Matrix.Identity;
    private Rectangle _bounds;
    private bool _dirty = true;

    public Matrix Matrix {
        get {
            if (_dirty)
                UpdateMatrices();
            return _matrix;
        }
    }

    public Matrix Inverse {
        get {
            if (_dirty)
                UpdateMatrices();
            return _inverse;
        }
    }

    public Rectangle Bounds {
        get {
            if (_dirty) 
                UpdateMatrices();
            return _bounds;
        }
    }


    private Vector2 _pos = Vector2.Zero;
    private Vector2 _origin = Vector2.Zero;
    private Vector2 _zoom = Vector2.One;
    private float _angle = 0;

    public Vector2 Position {
        get => _pos;
        set {
            _dirty = true;
            _pos = value;
        }
    }

    public Vector2 Origin {
        get => _origin;
        set {
            _dirty = true;
            _origin = value;
        }
    }

    public float Zoom {
        get => _zoom.X;
        set {
            _dirty = true;
            _zoom.X = value;
            _zoom.Y = value;
        }
    }

    public float X {
        get => _pos.X;
        set {
            _dirty = true;
            _pos.X = value;
        }
    }

    public float Y {
        get => _pos.Y;
        set {
            _dirty = true;
            _pos.Y = value;
        }
    }

    public float Angle {
        get => _angle;
        set {
            _dirty = true;
            _angle = value;
        }
    }

    public Viewport Viewport;

    public Camera() {
        Viewport = new Viewport {
            Width = Engine.Width,
            Height = Engine.Height
        };
        UpdateMatrices();
    }

    public void SetOriginToCenter() {
        Origin = new Vector2(Viewport.Width / 2f, Viewport.Height / 2f);
    }

    public void RoundPosition() {
        X = MathF.Round(X);
        Y = MathF.Round(Y);
    }

    public Vector2 ScreenToCamera(Vector2 screenSpacePos) {
        return Vector2.Transform(screenSpacePos, Inverse);
    }

    public Vector2 CameraToScreen(Vector2 cameraSpacePos) {
        return Vector2.Transform(cameraSpacePos, Matrix);
    }

    public void Approach(Vector2 position, float ease) {
        Position += (position - Position) * ease;
    }

    public void Approach(Vector2 position, float ease, float maxDistance) {
        Vector2 move = (position - Position) * ease;
        Position += move.Length() > maxDistance ? Vector2.Normalize(move) * maxDistance : move;
    }

    public void UpdateMatrices() {
        _matrix = Matrix.Identity *
                  Matrix.CreateTranslation(new Vector3(-new Vector2((_pos.X), (_pos.Y)), 0)) *
                  Matrix.CreateRotationZ(_angle) *
                  Matrix.CreateScale(new Vector3(_zoom, 1)) *
                  Matrix.CreateTranslation(new Vector3(new Vector2((_origin.X), (_origin.Y)), 0));

        _inverse = Matrix.Invert(_matrix);

        
        _bounds = new Rectangle((int)X, (int)Y, Viewport.Width, Viewport.Height);
        _bounds.Offset(-_origin);
        _dirty = false;
    }
}