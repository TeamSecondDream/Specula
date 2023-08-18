// Code from https://github.com/JamesMcMahon/monocle-engine/blob/master/Monocle/Engine.cs

using System;
using System.IO;
using System.Reflection;
using System.Runtime;
using InputStateManager;
using InputStateManager.Inputs;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Specula.Graphics;
using Specula.Scenes;
using Mouse = Microsoft.Xna.Framework.Input.Mouse;

namespace Specula;

// ReSharper disable file MemberCanBePrivate.Global
// ReSharper disable file UnusedAutoPropertyAccessor.Local
// ReSharper disable file UnusedAutoPropertyAccessor.Global

public class Engine : Game {
    public static Renderer PrimaryRenderer;

    public string WindowTitle;
    public Version GameVersion;
    
    public static Engine Instance { get; private set; }
    public static GraphicsDeviceManager Graphics { get; private set; }
    
    public static int Width { get; private set; }
    public static int Height { get; private set; }
    public static int ViewWidth { get; private set; }
    public static int ViewHeight { get; private set; }
    public static int ViewPadding { get; private set; }
    
    private static bool _busyResizing = false;
    
    public static Viewport Viewport { get; private set; }
    public static Matrix ScreenMatrix;
    public static Color ClearColor;

    public static double RawDeltaTime;
    public static double DeltaTime;
    public static float TimeRate = 1;
    public static int Fps { get; private set; }
    private int _fpsCounter = 0;
    private TimeSpan _counterElapsed = TimeSpan.Zero;
    
    public static Scene Scene { get; private set; }
    private Scene _nextScene;
    
    public static string AssemblyDirectory => Path.GetDirectoryName(Assembly.GetEntryAssembly()!.Location);

    public Engine(int width, int height, int windowWidth, int windowHeight, string windowTitle, bool fullscreen) {
        Instance = this;

        WindowTitle = Window.Title = windowTitle;
        Width = width;
        Height = height;
        ClearColor = Color.Black;
        
        Graphics = new GraphicsDeviceManager(this);
        Graphics.DeviceReset += OnGraphicsReset;
        Graphics.DeviceCreated += OnGraphicsCreate;
        Graphics.SynchronizeWithVerticalRetrace = true;
        Graphics.PreferMultiSampling = false;
        Graphics.GraphicsProfile = GraphicsProfile.HiDef;
        Graphics.PreferredBackBufferFormat = SurfaceFormat.Color;
        Graphics.PreferredDepthStencilFormat = DepthFormat.Depth24Stencil8;
        Graphics.ApplyChanges();        
        IsMouseVisible = true;
        
        Window.AllowUserResizing = true;
        Window.ClientSizeChanged += OnWindowSizeChanged;

        
        if (fullscreen) {
            Graphics.PreferredBackBufferWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
            Graphics.PreferredBackBufferHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;
            Graphics.IsFullScreen = true;
        } else {
            Graphics.PreferredBackBufferWidth = windowWidth;
            Graphics.PreferredBackBufferHeight = windowHeight;
            Graphics.IsFullScreen = false;
        }
        
        IsMouseVisible = false;
        IsFixedTimeStep = false;

        GCSettings.LatencyMode = GCLatencyMode.SustainedLowLatency;
        
        UpdateView();
        
    }

    private static void OnGraphicsReset(object sender, EventArgs e) {
        Scene?.OnGraphicsReset();
    }

    private static void OnGraphicsCreate(object sender, EventArgs e) {
        Scene?.OnGraphicsCreate();
    }

    private void OnWindowSizeChanged(object sender, EventArgs e) {
        if (_busyResizing || Window.ClientBounds is not { Width: > 0, Height: > 0 }) return;
        _busyResizing = true;

        Graphics.PreferredBackBufferWidth = Window.ClientBounds.Width;
        Graphics.PreferredBackBufferHeight = Window.ClientBounds.Height;
        UpdateView();
        
        _busyResizing = false;
    }
    
    public static void ChangeScene(Scene newScene) {
        Instance._nextScene = newScene;
    }
    
    protected override void Initialize() {
        base.Initialize();
    }

    protected override void LoadContent() {
        Drawing.Initialize();
    }

    protected override void Update(GameTime gameTime) {
        Input.Update();

        
        RawDeltaTime = gameTime.ElapsedGameTime.TotalSeconds;
        DeltaTime = RawDeltaTime * TimeRate;
        
        // input update

        if (Scene != null) {
            Scene.PreUpdate();
            Scene.Update();
            Scene.PostUpdate();
        }

        if (Scene != _nextScene) {
            Scene lastScene = Scene;

            Scene?.End();
            Scene = _nextScene;
            OnSceneChanged(lastScene, _nextScene);
            Scene?.Start();
        }
        
        base.Update(gameTime);
    }

    protected virtual void OnSceneChanged(Scene lastScene, Scene nextScene) {
        GC.Collect();
        GC.WaitForPendingFinalizers();

        TimeRate = 1f;
    }

    protected override void Draw(GameTime gameTime) {
        RenderImplementation();
        
        base.Draw(gameTime);
        _fpsCounter++;
        _counterElapsed += gameTime.ElapsedGameTime;
        if (_counterElapsed < TimeSpan.FromSeconds(1)) return;
#if DEBUG
        Window.Title = WindowTitle + " " + _fpsCounter + " fps - " + (GC.GetTotalMemory(false) / 1048576f).ToString("F") + " MB";
#endif
        Fps = _fpsCounter;
        _fpsCounter = 0;
        _counterElapsed -= TimeSpan.FromSeconds(1);

    }

    protected virtual void RenderImplementation() {
        Scene?.PreDraw();
        
        GraphicsDevice.SetRenderTarget(null);
        GraphicsDevice.Viewport = Viewport;
        GraphicsDevice.Clear(ClearColor);
        
        Scene?.Draw();
        Scene?.PostDraw();
    }

    public static void SetWindowed(int width, int height) {
        if (width <= 0 || height <= 0) return;
        _busyResizing = true;
        Graphics.PreferredBackBufferWidth = width;
        Graphics.PreferredBackBufferHeight = height;
        Graphics.IsFullScreen = false;
        Graphics.ApplyChanges();
        Instance.UpdateView();
        _busyResizing = false;
    }
    
    public static void SetFullscreen() {
        _busyResizing = true;
        Graphics.PreferredBackBufferWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
        Graphics.PreferredBackBufferHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;
        Graphics.IsFullScreen = true;         
        Graphics.ApplyChanges();
        Instance.UpdateView();
        _busyResizing = false;
    }
    
    private void UpdateView() {
        float screenWidth = GraphicsDevice.PresentationParameters.BackBufferWidth;
        float screenHeight = GraphicsDevice.PresentationParameters.BackBufferHeight;

        //Console.WriteLine($"Screen: {screenWidth}, {screenHeight}");
        
        // get View Size
        if (screenWidth / Width > screenHeight / Height) {
            ViewWidth = (int)(screenHeight / Height * Width);
            ViewHeight = (int)screenHeight;
        }
        else {
            ViewWidth = (int)screenWidth;
            ViewHeight = (int)(screenWidth / Width * Height);
        }

        //Console.WriteLine($"View: {ViewWidth}, {ViewHeight}");

        
        // apply View Padding
        float aspect = ViewHeight / (float)ViewWidth;
        ViewWidth -= ViewPadding * 2;
        ViewHeight -= (int)(aspect * ViewPadding * 2);

        //Console.WriteLine($"Aspect: {aspect}, ViewPadding: {ViewPadding}");
        //Console.WriteLine($"Adjusted View: {ViewWidth}, {ViewHeight}");

        
        // update screen matrix
        ScreenMatrix = Matrix.CreateScale(ViewWidth / (float)Width);
        //Console.WriteLine($"Screen Matrix Scale: {ViewWidth / (float)Width}");

        // update viewport
        Viewport = new Viewport {
            X = (int)(screenWidth / 2 - ViewWidth / 2),
            Y = (int)(screenHeight / 2 - ViewHeight / 2),
            Width = ViewWidth,
            Height = ViewHeight,
            MinDepth = 0,
            MaxDepth = 1
        };
    }
}