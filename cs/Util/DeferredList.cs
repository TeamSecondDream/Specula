using System;
using System.Collections.Generic;

namespace Specula.Util; 

public abstract class DeferredList<T> {
    protected readonly List<T> Contents = new();
    internal readonly List<T> ToAdd = new();
    private readonly List<T> _toRemove = new();

    public int Count => Contents.Count;

    public T this[int index] => Contents[index];

    public void Add(T entry) {
        ToAdd.Add(entry);
    }

    public void Remove(T entry) {
        _toRemove.Add(entry);
    }

    public void Clear() {
        _toRemove.AddRange(Contents);
    }

    public void UpdateChanges() {
        if (_toRemove.Count > 0) {
            foreach (T entry in _toRemove) {
                Contents.Remove(entry);
            }

            _toRemove.Clear();
        }

        if (ToAdd.Count > 0) {
            foreach (T entry in ToAdd) {
                Contents.Add(entry);
            }
            ToAdd.Clear();
        }
    }

    protected static void UpdateAll<TEntry>(List<TEntry> entries) where TEntry : IUpdatable {
        entries.Sort();
        foreach (TEntry entry in entries) {
            entry.PreUpdate();
            entry.Update();
            entry.PostUpdate();
        }
    }

    protected static void DrawAll<TEntry>(List<TEntry> entries) where TEntry : IRenderable {
        foreach (TEntry entry in entries) {
            entry.PreDraw();
            entry.Draw();
            entry.PostDraw();
        }
    }
}