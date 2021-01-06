using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


public enum Quality { Common, Rare, Epic, Legendary }

public static class QualityColor
{
    private static Dictionary<Quality, string> colors = new Dictionary<Quality, string>()
    {
        {Quality.Common, "#E7E7E7"},
        {Quality.Rare, "#0669B5"},
        {Quality.Epic, "#C132B4"},
        {Quality.Legendary, "#B48006"},
    };

    public static Dictionary<Quality, string> MyColors { get => colors; }
}

