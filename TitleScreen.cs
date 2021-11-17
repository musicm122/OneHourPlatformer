using Godot;
using System;

public class TitleScreen : Node
{
    [Export(PropertyHint.File, "*.tscn")]
    public string StartScene;

    public override void _Ready()
    {
        ((Button)FindNode("Start")).GrabFocus();
    }

    void OnStartPressed()
    {
        GetTree().ChangeScene(StartScene);
    }
    void OnQuitPressed()
    {
        GetTree().Quit();
    }
}