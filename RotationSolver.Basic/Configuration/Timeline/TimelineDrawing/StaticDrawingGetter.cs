﻿using ECommons.DalamudServices;
using RotationSolver.Basic.Configuration.Timeline.TimelineCondition;
using XIVPainter.Vfx;

namespace RotationSolver.Basic.Configuration.Timeline.TimelineDrawing;

[Description("Static Drawing")]
internal class StaticDrawingGetter : BaseDrawingGetter
{
    public string Path { get; set; } = GroundOmenHostile.Circle.Omen();
    public Vector3 Position { get; set; }
    public float Rotation { get; set; }
    public Vector3 Scale { get; set; } = Vector3.Zero;
    public bool PlaceOnObject { get; set; } = false;
    public ObjectGetter ObjectGetter { get; set; } = new();
    public TextDrawing Text { get; set; } = new();
    public override IDisposable[] GetDrawing()
    {
        if (string.IsNullOrEmpty(Path)) return [];

        if (PlaceOnObject)
        {
            List<IDisposable> drawables = [];
            foreach (var obj in Svc.Objects.Where(ObjectGetter.CanGet))
            {
                drawables.Add(new StaticVfx(Path, Position + obj.Position, Rotation + obj.Rotation, Scale));
                var text = Text.GetText(Position);
                if(text != null)
                {
                    drawables.Add(text);
                }
            }
            return [.. drawables];
        }
        else
        {
            var item = new StaticVfx(Path, Position, Rotation, Scale);
            var text = Text.GetText(Position);
            return text == null ? [item] : [item, text];
        }
    }
}