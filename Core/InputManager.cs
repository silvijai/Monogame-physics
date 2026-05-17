using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Box2D.NET.Bindings;
using System.Collections.Generic;

namespace Physics_Game;

public class InputManager
{
    // TODO Implement a system for managing inputs, and their corresponding bindings
    // Be aware of stuff like axis needing to use a method that can interperate that
    // Also touch screen and mouse needs to function with screenspace clicks, and maybe on-screen controls
    
    // keyboard button implementation:
    // private Dictionary<string, List<Binding>> _bindings = new ();
    

    public Vector2 PointerPosition = new Vector2.Zero;

    private KeyboardState _prevKb;
    private MouseState    _prevMouse;
    private GamePadState  _prevGp;

    public void Update(double deltaTime)
    {
        var kb    = Keyboard.GetState();
        var mouse = Mouse.GetState();
        var gp    = GamePad.GetState(PlayerIndex.One, GamePadDeadZone.None);

        foreach (Keys key in kb.GetPressedKeys())
            if (!_prevKb.IsKeyDown(key))
                // find action from binding

        if (mouse.LeftButton == ButtonState.Pressed && _prevMouse.LeftButton == ButtonState.Released)
            // left click 
        if (mouse.RightButton == ButtonState.Pressed && _prevMouse.RightButton == ButtonState.Released) 
            // right click
        if (mouse.MiddleButton == ButtonState.Pressed && _prevMouse.MiddleButton == ButtonState.Released) 
            // middle click

        if (mouse.X != _prevMouse.X || mouse.Y != _prevMouse.Y)
            PointerPosition = new Vector2(mouse.X, mouse.Y);

        if (mouse.ScrollWheelValue != _prevMouse.ScrollWheelValue)
            // scrolling

        if (gp.IsConnected)
        {
            foreach (Buttons btn in Enum.GetValues<Buttons>())
                if (gp.IsButtonDown(btn) && !_prevGp.IsButtonDown(btn))
                    // find action from binding

            void Axis(string name, float val, float prev)
            {
                if (MathF.Abs(val) > 0.1f && MathF.Abs(val - prev) > 0.01f)
                    // find binding from name, and pass to action
            }

            Axis("LeftStick.X",  gp.ThumbSticks.Left.X,  _prevGp.ThumbSticks.Left.X);
            Axis("LeftStick.Y",  gp.ThumbSticks.Left.Y,  _prevGp.ThumbSticks.Left.Y);
            Axis("RightStick.X", gp.ThumbSticks.Right.X, _prevGp.ThumbSticks.Right.X);
            Axis("RightStick.Y", gp.ThumbSticks.Right.Y, _prevGp.ThumbSticks.Right.Y);
            Axis("LeftTrigger",  gp.Triggers.Left,        _prevGp.Triggers.Left);
            Axis("RightTrigger", gp.Triggers.Right,       _prevGp.Triggers.Right);

            // CheckPS5Touchpad();
        }

        _prevKb    = kb;
        _prevMouse = mouse;
        _prevGp    = gp;
    }
 
}
