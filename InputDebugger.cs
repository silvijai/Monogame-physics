using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;

namespace Physics_Game;

public class InputDebugger
{
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
                Console.WriteLine($"[KB] Pressed: {key}");

        if (mouse.LeftButton   == ButtonState.Pressed && _prevMouse.LeftButton   == ButtonState.Released) Console.WriteLine("[MOUSE] Left pressed");
        if (mouse.RightButton  == ButtonState.Pressed && _prevMouse.RightButton  == ButtonState.Released) Console.WriteLine("[MOUSE] Right pressed");
        if (mouse.MiddleButton == ButtonState.Pressed && _prevMouse.MiddleButton == ButtonState.Released) Console.WriteLine("[MOUSE] Middle pressed");

        if (mouse.X != _prevMouse.X || mouse.Y != _prevMouse.Y)
            Console.WriteLine($"[MOUSE] Position: ({mouse.X}, {mouse.Y})");

        if (mouse.ScrollWheelValue != _prevMouse.ScrollWheelValue)
            Console.WriteLine($"[MOUSE] Scroll: {mouse.ScrollWheelValue - _prevMouse.ScrollWheelValue}");

        if (gp.IsConnected)
        {
            foreach (Buttons btn in Enum.GetValues<Buttons>())
                if (gp.IsButtonDown(btn) && !_prevGp.IsButtonDown(btn))
                    Console.WriteLine($"[GP] Button: {btn}");

            void Axis(string name, float val, float prev)
            {
                if (MathF.Abs(val) > 0.1f && MathF.Abs(val - prev) > 0.01f)
                    Console.WriteLine($"[GP] {name}: {val:F3}");
            }

            Axis("LeftStick.X",  gp.ThumbSticks.Left.X,  _prevGp.ThumbSticks.Left.X);
            Axis("LeftStick.Y",  gp.ThumbSticks.Left.Y,  _prevGp.ThumbSticks.Left.Y);
            Axis("RightStick.X", gp.ThumbSticks.Right.X, _prevGp.ThumbSticks.Right.X);
            Axis("RightStick.Y", gp.ThumbSticks.Right.Y, _prevGp.ThumbSticks.Right.Y);
            Axis("LeftTrigger",  gp.Triggers.Left,        _prevGp.Triggers.Left);
            Axis("RightTrigger", gp.Triggers.Right,       _prevGp.Triggers.Right);

            CheckPS5Touchpad();
        }

        _prevKb    = kb;
        _prevMouse = mouse;
        _prevGp    = gp;
    }

    private void CheckPS5Touchpad()
    {
        try
        {
            const string lib = "/opt/homebrew/lib/libSDL2.dylib";

            [System.Runtime.InteropServices.DllImport(lib)] static extern int    SDL_NumJoysticks();
            [System.Runtime.InteropServices.DllImport(lib)] static extern bool   SDL_IsGameController(int i);
            [System.Runtime.InteropServices.DllImport(lib)] static extern IntPtr SDL_GameControllerOpen(int i);
            [System.Runtime.InteropServices.DllImport(lib)] static extern void   SDL_GameControllerClose(IntPtr c);
            [System.Runtime.InteropServices.DllImport(lib)] static extern int    SDL_GameControllerGetNumTouchpads(IntPtr c);
            [System.Runtime.InteropServices.DllImport(lib)] static extern int    SDL_GameControllerGetNumTouchpadFingers(IntPtr c, int tp);
            [System.Runtime.InteropServices.DllImport(lib)] static extern int    SDL_GameControllerGetTouchpadFinger(IntPtr c, int tp, int f, out byte state, out float x, out float y, out float pressure);

            int n = SDL_NumJoysticks();
            for (int i = 0; i < n; i++)
            {
                if (!SDL_IsGameController(i)) continue;
                var ctrl = SDL_GameControllerOpen(i);
                if (ctrl == IntPtr.Zero) continue;

                int tps = SDL_GameControllerGetNumTouchpads(ctrl);
                for (int tp = 0; tp < tps; tp++)
                {
                    int fingers = SDL_GameControllerGetNumTouchpadFingers(ctrl, tp);
                    for (int f = 0; f < fingers; f++)
                    {
                        SDL_GameControllerGetTouchpadFinger(ctrl, tp, f,
                            out byte state, out float x, out float y, out float pressure);
                        if (state != 0)
                            Console.WriteLine($"[PS5 Touchpad {tp}] Finger {f}: ({x:F3}, {y:F3}) pressure={pressure:F3}");
                    }
                }
                SDL_GameControllerClose(ctrl);
            }
        }
        catch (Exception e)
        {
            Console.WriteLine($"[Touchpad] {e.Message}");
        }
    }
}
