namespace Mondriaan.Core;

using System;

public class Rectangle
{
    public int Top { get; set; } = 0;
    public int Left { get; set; } = 0;
    public int Width { get; set; } = 0;
    public int Height { get; set; } = 0;
    public string Color { get; set; } = string.Empty;
    public int SurfaceArea { get { return this.Width * this.Height; } }

    public Rectangle()
    {
    }

    public override string ToString()
    {
        return string.Format("{0}*{1} at [{2},{3}]", this.Width, this.Height, this.Top, this.Left);
    }

    public string ToHtml()
    {
        string htmlColor = this.Color;
        if (string.IsNullOrWhiteSpace(htmlColor))
        {
            htmlColor = "none";
        }

        string result = string.Format("<div style='position:absolute;top:{0}px;left:{1}px;width:{2}px;height:{3}px;background-color:{4};border:1px solid #000000;'>&nbsp;</div>", this.Top, this.Left, this.Width, this.Height, htmlColor);
        return result;
    }

    public RectangleList SplitHorizontally(double splitPercentage)
    {
        RectangleList result = new RectangleList();

        int widthLeft = (int)Math.Floor(this.Width * splitPercentage);

        Rectangle rectLeft = new Rectangle
        {
            Top = this.Top,
            Left = this.Left,
            Width = widthLeft,
            Height = this.Height,
            Color = this.Color
        };

        Rectangle rectRight = new Rectangle
        {
            Top = this.Top,
            Left = this.Left + widthLeft,
            Width = this.Width - widthLeft,
            Height = this.Height,
            Color = this.Color
        };

        result.Add(rectLeft);
        result.Add(rectRight);

        return result;
    }

    public RectangleList SplitVertically(double splitPercentage)
    {
        RectangleList result = new RectangleList();

        int heightTop = (int)Math.Floor(this.Height * splitPercentage);

        Rectangle rectTop = new Rectangle
        {
            Top = this.Top,
            Left = this.Left,
            Width = this.Width,
            Height = heightTop,
            Color = this.Color
        };

        Rectangle rectBottom = new Rectangle
        {
            Top = this.Top + heightTop,
            Left = this.Left,
            Width = this.Width,
            Height = this.Height - heightTop,
            Color = this.Color
        };

        result.Add(rectTop);
        result.Add(rectBottom);

        return result;
    }

    public RectangleList MakeMondrian()
    {
        int minSurfaceArea = (int)Math.Floor((double)(SurfaceArea / 32));
        int maxSurfaceAreaForSkip = (int)Math.Floor((double)(SurfaceArea / 4));
        var actual = MakeMondrian(minSurfaceArea, maxSurfaceAreaForSkip);
        actual.Colorize();
        return actual;
    }

    private RectangleList MakeMondrian(int minSurfaceArea, int maxSurfaceForSkip)
    {
        RectangleList result = new RectangleList();
        result.Width = this.Width;
        result.Height = this.Height;

        if (this.SurfaceArea < minSurfaceArea || this.Width < 5 || this.Height < 5)
        {
            //abort if to small rectangle
            result.Add(this);
        }
        else if (this.SurfaceArea <= maxSurfaceForSkip && GetRandomBoolNormal(0.1))
        {
            //skip 1 in 10 rectangles
            //skip randomly to allow bigger rectangles
            result.Add(this);
        }
        else
        {
            //fifty-fifty chance for horizontal vs vertical split
            bool randomBool = GetRandomBoolNormal(0.5);

            RectangleList split;
            if (randomBool)
            {
                //todo: random splitpercentage between 0.2 and 0.8
                split = this.SplitHorizontally(GetRandomSplitPercentage());
            }
            else
            {
                //todo: random splitpercentage between 0.2 and 0.8
                split = this.SplitVertically(GetRandomSplitPercentage());
            }
            result.AddRange(split[0].MakeMondrian(minSurfaceArea, maxSurfaceForSkip));
            result.AddRange(split[1].MakeMondrian(minSurfaceArea, maxSurfaceForSkip));
        }
        return result;
    }

    private static double GetRandomSplitPercentage()
    {
        Random random = RandomProvider.GetThreadRandom();
        int randomNumber = random.Next(20, 81);
        double randomSplitPercentage = randomNumber / 100.0;
        return randomSplitPercentage;
    }

    //chanceTrue between 0..1. 0.9 means 90% chance at true
    private static bool GetRandomBoolNormal(double chanceTrue)
    {
        Random random = RandomProvider.GetThreadRandom();
        double randomNumber = random.NextDouble();

        bool randomBool;
        if (randomNumber < (1 - chanceTrue))
        {
            randomBool = false;
        }
        else
        {
            randomBool = true;
        }

        return randomBool;
    }
}
