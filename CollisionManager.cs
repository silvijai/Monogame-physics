using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace Physics_Game;

public class CollisionManager
{
    private List<Collider> _colliders = new List<Collider>();

    public void Register(Collider collider) => _colliders.Add(collider);
    public void Unregister(Collider collider) => _colliders.Remove(collider);

    public void Update()
    {
        for (int i = 0; i < _colliders.Count; i++)
        {
            for (int j = i + 1; j < _colliders.Count; j++)
            {
                var a = _colliders[i];
                var b = _colliders[j];
                bool hit = a.Intersects(b);

                if (hit)
                {
                    // If one is solid, resolve the non-solid one
                    if (b.Tag == "solid") a.Resolve(b);
                    else if (a.Tag == "solid") b.Resolve(a);
                }

                a.ReportCollision(b, hit);
                b.ReportCollision(a, hit);
            }
        }
    }

    // Query helpers — useful for gameplay logic
    public List<Collider> GetOverlapping(Collider target)
    {
        var results = new List<Collider>();
        foreach (var c in _colliders)
            if (c != target && c.Intersects(target))
                results.Add(c);
        return results;
    }

    public List<Collider> GetByTag(string tag)
    {
        return _colliders.FindAll(c => c.Tag == tag);
    }
}
