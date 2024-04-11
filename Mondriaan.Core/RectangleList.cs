namespace Mondriaan.Core;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class RectangleList : List<Rectangle>
{
    public int Width { get; set; }
    public int Height { get; set; }

    public void Colorize()
    {
        if (this.Count() > 3)
        {

            List<string> colors = new List<string> { "#ff0000", "#ffff00", "#0000ff", "#000000" };
            Random random = RandomProvider.GetThreadRandom();

            int randomColorCount = random.Next(2, (int)Math.Floor(((double)(this.Count() / 2))));

            List<string> randomColors = new List<string>();
            for (int i = 0; i < randomColorCount; i++)
            {
                int randomIndex = random.Next(0, colors.Count());
                randomColors.Add(colors[randomIndex]);
            }

            foreach (string color in randomColors)
            {
                this.GetRandomRectangleWithoutColor().Color = color;
            }

        }
    }

    private Rectangle GetRandomRectangleWithoutColor()
    {
        List<Rectangle> rectanglesWithoutColor = this.Where(x => string.IsNullOrWhiteSpace(x.Color)).ToList();
        int count = rectanglesWithoutColor.Count();
        Random random = RandomProvider.GetThreadRandom();
        int randomIndex = random.Next(0, count);

        return rectanglesWithoutColor[randomIndex];
    }

    public string ToHtml()
    {
        StringBuilder contents = new StringBuilder();

        foreach (Rectangle rect in this)
        {
            contents.AppendLine(rect.ToHtml());
        }

        string result = string.Format("<div style='position:relative;width:{0}px;height:{1}px;'>{2}</div>", Width, Height, contents);
        return result;
    }
}
