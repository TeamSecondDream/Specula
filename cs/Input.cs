using InputStateManager;
using InputStateManager.Inputs;
using Microsoft.Xna.Framework;
using Specula.Graphics;

namespace Specula; 

public static class Input {
    public static readonly InputManager Manager = new();
    public static Key.IsSub Key { get; private set; }
    public static Mouse.IsSub Mouse { get; private set; }
    public static Touch.IsSub Touch { get; private set; }
    public static Pad.IsSub Controller(int index) => Manager.Pad(index).Is;

    public static float ScreenMouseX {get; private set;}
    public static float ScreenMouseY {get; private set;}
    public static float MouseX {get; private set;}
    public static float MouseY {get; private set;}
    public static Vector2 MousePos {get; private set;}
    public static Vector2 ScreenMousePos {get; private set;}
    
    public static bool MouseVisible {
        get => Engine.Instance.IsMouseVisible;
        set => Engine.Instance.IsMouseVisible = value;
    }
	
    internal static void Update() {
        Manager.Update();
        Key = Manager.Key.Is;
        Mouse = Manager.Mouse.Is;
        Touch = Manager.Touch.Is;
        
        Vector2 mousePos = (Mouse.Position.ToVector2() - new Vector2(Engine.Viewport.X, Engine.Viewport.Y)) / new Vector2(Engine.Viewport.Width / (float) Engine.Width, Engine.Viewport.Height/ (float) Engine.Height);

        MousePos = Engine.PrimaryRenderer.Camera.ScreenToCamera(mousePos);
        MouseX = MousePos.X;
        MouseY = MousePos.Y;
        
        ScreenMousePos = mousePos;
        ScreenMouseX = ScreenMousePos.X;
        ScreenMouseY = ScreenMousePos.Y;
    }
}