using System;

public class StaticScene : GLScene
{
    public override void EndScene()
    {
        SwapBuffers();
    }

    public override void OnWindowResized(object sender, int width, int height)
    {
        DrawScene();
    }
}

