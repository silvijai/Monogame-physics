using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System;

namespace Physics_Game;

public class Entity
{
    public readonly int Id;
    public string Name;
    public bool Active = true;
    public bool Debug = false;

    // Components
    private List<Component> _components = new List<Component>();

    public Entity(int id, string name = "Entity")
    {
        Id = id;
        Name = name;
    }

    // Add a component and return it (fluent style)
    public T AddComponent<T>(T component) where T : Component
    {
        component.Entity = this;
        _components.Add(component);
        return component;
    }

    public void RemoveComponent<T>(T component) where T: Component
    {
        _components.Remove(component);
    }

    // Get a component by type
    public T GetComponent<T>() where T : Component
    {
        foreach (var c in _components)
            if (c is T match) return match;
        return null;
    }

    public void Update(double deltaTime)
    {
        if (!Active) return;
        foreach (var c in _components)
            if (c.Enabled) c.Update(deltaTime);
    } 

    public void Draw(SpriteBatch spriteBatch)
    {
        if (!Active) return;
        foreach (var c in _components)
            if (c.Enabled) c.Draw(spriteBatch);
    }

    public void DebugDraw(SpriteBatch spriteBatch)
    {
        if (!Active) return; 
        foreach (var c in _components)
            if (c.Enabled && c.DrawDebug) c.DebugDraw(spriteBatch);
    }

    public void DebugPrint()
    {
        if (!Active) return;
        Console.WriteLine($"=== {Name} (id:{Id}) active:{Active} ===");
        foreach (var c in _components)
            if (c.Enabled && c.PrintDebug) c.DebugPrint();
    }
}
