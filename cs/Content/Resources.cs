using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;
using Specula.Content.Loaders;
using Specula.Util;

namespace Specula.Content; 

public static class Resources {
    private static readonly string[] ResourceLocations;

    public static readonly Storage<Texture2D> Textures = new();
    public static readonly Storage<Effect> Effects = new();
    public static readonly Storage<SoundEffect> Sounds = new();
    public static readonly Storage<Song> Songs = new();
    public static readonly Storage<Font> Fonts = new();

    private static readonly List<ResourceLoader> ResourceLoaders = new();

    public static void RegisterLoader(ResourceLoader loader) {
        ResourceLoaders.Add(loader);
    }
    
    static Resources() {
        Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies();
        List<string> resourceLocations = new();
        
        foreach (Assembly assembly in assemblies) {
            foreach (Type t in assembly.GetTypes()) {
                if (t.GetCustomAttributes(typeof(ResourceLoaderAttribute), true).Length > 0) {
                    ResourceLoaders.Add((ResourceLoader) Activator.CreateInstance(t));
                }
            }
            
            string path = Path.GetDirectoryName(assembly.Location);
            if (path == null || !Directory.Exists(path = Path.Combine(path, "resources"))) continue;
            resourceLocations.Add(path);
            //Console.WriteLine(path);
        }

        ResourceLocations = resourceLocations.ToArray();
    }
    
    public static FileStream GetFileStream(string path) {
        foreach (string resourceLocation in ResourceLocations) {
            string filePath = Path.Combine(resourceLocation, path);
            if (File.Exists(filePath)) {
                return File.OpenRead(filePath);
            }
        }

        throw new FileNotFoundException($"Resource not found: {path}");
    }

    public static StreamReader GetTextFileStream(string path) {
        foreach (string resourceLocation in ResourceLocations) {
            string filePath = Path.Combine(resourceLocation, path);
            if (File.Exists(filePath)) {
                return File.OpenText(filePath);
            }
        }

        throw new FileNotFoundException($"Resource not found: {path}");
    }

    public static bool Load(string resourcePath) {
        foreach (string resourceLocation in ResourceLocations) {
            string filePath = Path.Combine(resourceLocation, resourcePath);
            if (!File.Exists(filePath)) continue;
            foreach (ResourceLoader resourceLoader in ResourceLoaders) {
                if (!resourceLoader.ShouldLoadFile(resourcePath)) continue;
                resourceLoader.Load(resourcePath, filePath);
                
                return true;
            }
            
            return false;
        }

        return false;
    }
    
    public sealed class Storage<T> {
        private readonly Dictionary<string, T> _entries = new();
        
        internal Storage() {
            
        }

        public void Add(string path, T entry) {
            _entries.Add(path, entry);
        }

        public bool Contains(string path) => _entries.ContainsKey(path);
        
        public T this[string resourcePath] {
            get {
                if (Contains(resourcePath)) return _entries[resourcePath];
                Load(resourcePath);
                return _entries[resourcePath];

            }
        }

        ~Storage() {
            _entries.Clear();
        }

        
       
    }
}