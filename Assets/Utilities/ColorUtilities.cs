using UnityEngine;

public static class ColorUtilities
{
    //====================================================================================================
    //====================================================================================================

    /// <summary>
    /// NOTE: Color names come from the CSS3 specification, Section 4.3 Extended Color Keywords
    /// http://www.w3.org/TR/css3-color/#svg-color
    /// </summary>
    public static Color AliceBlue { get { return new Color32(240, 248, 255, 255); } }
    public static Color AntiqueWhite { get { return new Color32(250, 235, 215, 255); } }
    public static Color Aqua { get { return new Color32(0, 255, 255, 255); } }
    public static Color Aquamarine { get { return new Color32(127, 255, 212, 255); } }
    public static Color Azure { get { return new Color32(240, 255, 255, 255); } }
    public static Color Beige { get { return new Color32(245, 245, 220, 255); } }
    public static Color Bisque { get { return new Color32(255, 228, 196, 255); } }
    public static Color Black { get { return new Color32(0, 0, 0, 255); } }
    public static Color BlanchedAlmond { get { return new Color32(255, 235, 205, 255); } }
    public static Color Blue { get { return new Color32(0, 0, 255, 255); } }
    public static Color BlueViolet { get { return new Color32(138, 43, 226, 255); } }
    public static Color Brown { get { return new Color32(165, 42, 42, 255); } }
    public static Color Burlywood { get { return new Color32(222, 184, 135, 255); } }
    public static Color CadetBlue { get { return new Color32(95, 158, 160, 255); } }
    public static Color Chartreuse { get { return new Color32(127, 255, 0, 255); } }
    public static Color Chocolate { get { return new Color32(210, 105, 30, 255); } }
    public static Color Coral { get { return new Color32(255, 127, 80, 255); } }
    public static Color CornflowerBlue { get { return new Color32(100, 149, 237, 255); } }
    public static Color Cornsilk { get { return new Color32(255, 248, 220, 255); } }
    public static Color Crimson { get { return new Color32(220, 20, 60, 255); } }
    public static Color Cyan { get { return new Color32(0, 255, 255, 255); } }
    public static Color DarkBlue { get { return new Color32(0, 0, 139, 255); } }
    public static Color DarkCyan { get { return new Color32(0, 139, 139, 255); } }
    public static Color DarkGoldenrod { get { return new Color32(184, 134, 11, 255); } }
    public static Color DarkGray { get { return new Color32(169, 169, 169, 255); } }
    public static Color DarkGreen { get { return new Color32(0, 100, 0, 255); } }
    public static Color DarkKhaki { get { return new Color32(189, 183, 107, 255); } }
    public static Color DarkMagenta { get { return new Color32(139, 0, 139, 255); } }
    public static Color DarkOliveGreen { get { return new Color32(85, 107, 47, 255); } }
    public static Color DarkOrange { get { return new Color32(255, 140, 0, 255); } }
    public static Color DarkOrchid { get { return new Color32(153, 50, 204, 255); } }
    public static Color DarkRed { get { return new Color32(139, 0, 0, 255); } }
    public static Color DarkSalmon { get { return new Color32(233, 150, 122, 255); } }
    public static Color DarkSeaGreen { get { return new Color32(143, 188, 143, 255); } }
    public static Color DarkSlateBlue { get { return new Color32(72, 61, 139, 255); } }
    public static Color DarkSlateGray { get { return new Color32(47, 79, 79, 255); } }
    public static Color DarkTurquoise { get { return new Color32(0, 206, 209, 255); } }
    public static Color DarkViolet { get { return new Color32(148, 0, 211, 255); } }
    public static Color DeepPink { get { return new Color32(255, 20, 147, 255); } }
    public static Color DeepSkyBlue { get { return new Color32(0, 191, 255, 255); } }
    public static Color DimGray { get { return new Color32(105, 105, 105, 255); } }
    public static Color DodgerBlue { get { return new Color32(30, 144, 255, 255); } }
    public static Color FireBrick { get { return new Color32(178, 34, 34, 255); } }
    public static Color FloralWhite { get { return new Color32(255, 250, 240, 255); } }
    public static Color ForestGreen { get { return new Color32(34, 139, 34, 255); } }
    public static Color Fuchsia { get { return new Color32(255, 0, 255, 255); } }
    public static Color Gainsboro { get { return new Color32(220, 220, 220, 255); } }
    public static Color GhostWhite { get { return new Color32(248, 248, 255, 255); } }
    public static Color Gold { get { return new Color32(255, 215, 0, 255); } }
    public static Color Goldenrod { get { return new Color32(218, 165, 32, 255); } }
    public static Color Gray { get { return new Color32(128, 128, 128, 255); } }
    public static Color Green { get { return new Color32(0, 128, 0, 255); } }
    public static Color GreenYellow { get { return new Color32(173, 255, 47, 255); } }
    public static Color Honeydew { get { return new Color32(240, 255, 240, 255); } }
    public static Color HotPink { get { return new Color32(255, 105, 180, 255); } }
    public static Color IndianRed { get { return new Color32(205, 92, 92, 255); } }
    public static Color Indigo { get { return new Color32(75, 0, 130, 255); } }
    public static Color Ivory { get { return new Color32(255, 255, 240, 255); } }
    public static Color Khaki { get { return new Color32(240, 230, 140, 255); } }
    public static Color Lavender { get { return new Color32(230, 230, 250, 255); } }
    public static Color Lavenderblush { get { return new Color32(255, 240, 245, 255); } }
    public static Color LawnGreen { get { return new Color32(124, 252, 0, 255); } }
    public static Color LemonChiffon { get { return new Color32(255, 250, 205, 255); } }
    public static Color LightBlue { get { return new Color32(173, 216, 230, 255); } }
    public static Color LightCoral { get { return new Color32(240, 128, 128, 255); } }
    public static Color LightCyan { get { return new Color32(224, 255, 255, 255); } }
    public static Color LightGoldenrodYellow { get { return new Color32(250, 250, 210, 255); } }
    public static Color LightGray { get { return new Color32(211, 211, 211, 255); } }
    public static Color LightGreen { get { return new Color32(144, 238, 144, 255); } }
    public static Color LightPink { get { return new Color32(255, 182, 193, 255); } }
    public static Color LightSalmon { get { return new Color32(255, 160, 122, 255); } }
    public static Color LightSeaGreen { get { return new Color32(32, 178, 170, 255); } }
    public static Color LightSkyBlue { get { return new Color32(135, 206, 250, 255); } }
    public static Color LightSlateGray { get { return new Color32(119, 136, 153, 255); } }
    public static Color LightSteelBlue { get { return new Color32(176, 196, 222, 255); } }
    public static Color LightYellow { get { return new Color32(255, 255, 224, 255); } }
    public static Color Lime { get { return new Color32(0, 255, 0, 255); } }
    public static Color LimeGreen { get { return new Color32(50, 205, 50, 255); } }
    public static Color Linen { get { return new Color32(250, 240, 230, 255); } }
    public static Color Magenta { get { return new Color32(255, 0, 255, 255); } }
    public static Color Maroon { get { return new Color32(128, 0, 0, 255); } }
    public static Color MediumAquamarine { get { return new Color32(102, 205, 170, 255); } }
    public static Color MediumBlue { get { return new Color32(0, 0, 205, 255); } }
    public static Color MediumOrchid { get { return new Color32(186, 85, 211, 255); } }
    public static Color MediumPurple { get { return new Color32(147, 112, 219, 255); } }
    public static Color MediumSeaGreen { get { return new Color32(60, 179, 113, 255); } }
    public static Color MediumSlateBlue { get { return new Color32(123, 104, 238, 255); } }
    public static Color MediumSpringGreen { get { return new Color32(0, 250, 154, 255); } }
    public static Color MediumTurquoise { get { return new Color32(72, 209, 204, 255); } }
    public static Color MediumVioletRed { get { return new Color32(199, 21, 133, 255); } }
    public static Color MidnightBlue { get { return new Color32(25, 25, 112, 255); } }
    public static Color Mintcream { get { return new Color32(245, 255, 250, 255); } }
    public static Color MistyRose { get { return new Color32(255, 228, 225, 255); } }
    public static Color Moccasin { get { return new Color32(255, 228, 181, 255); } }
    public static Color NavajoWhite { get { return new Color32(255, 222, 173, 255); } }
    public static Color Navy { get { return new Color32(0, 0, 128, 255); } }
    public static Color OldLace { get { return new Color32(253, 245, 230, 255); } }
    public static Color Olive { get { return new Color32(128, 128, 0, 255); } }
    public static Color OliveDrab { get { return new Color32(107, 142, 35, 255); } }
    public static Color Orange { get { return new Color32(255, 165, 0, 255); } }
    public static Color OrangeRed { get { return new Color32(255, 69, 0, 255); } }
    public static Color Orchid { get { return new Color32(218, 112, 214, 255); } }
    public static Color PaleGoldenrod { get { return new Color32(238, 232, 170, 255); } }
    public static Color PaleGreen { get { return new Color32(152, 251, 152, 255); } }
    public static Color PaleTurquoise { get { return new Color32(175, 238, 238, 255); } }
    public static Color PaleVioletRed { get { return new Color32(219, 112, 147, 255); } }
    public static Color PapayaWhip { get { return new Color32(255, 239, 213, 255); } }
    public static Color PeachPuff { get { return new Color32(255, 218, 185, 255); } }
    public static Color Peru { get { return new Color32(205, 133, 63, 255); } }
    public static Color Pink { get { return new Color32(255, 192, 203, 255); } }
    public static Color Plum { get { return new Color32(221, 160, 221, 255); } }
    public static Color PowderBlue { get { return new Color32(176, 224, 230, 255); } }
    public static Color Purple { get { return new Color32(128, 0, 128, 255); } }
    public static Color Red { get { return new Color32(255, 0, 0, 255); } }
    public static Color RosyBrown { get { return new Color32(188, 143, 143, 255); } }
    public static Color RoyalBlue { get { return new Color32(65, 105, 225, 255); } }
    public static Color SaddleBrown { get { return new Color32(139, 69, 19, 255); } }
    public static Color Salmon { get { return new Color32(250, 128, 114, 255); } }
    public static Color SandyBrown { get { return new Color32(244, 164, 96, 255); } }
    public static Color SeaGreen { get { return new Color32(46, 139, 87, 255); } }
    public static Color Seashell { get { return new Color32(255, 245, 238, 255); } }
    public static Color Sienna { get { return new Color32(160, 82, 45, 255); } }
    public static Color Silver { get { return new Color32(192, 192, 192, 255); } }
    public static Color SkyBlue { get { return new Color32(135, 206, 235, 255); } }
    public static Color SlateBlue { get { return new Color32(106, 90, 205, 255); } }
    public static Color SlateGray { get { return new Color32(112, 128, 144, 255); } }
    public static Color Snow { get { return new Color32(255, 250, 250, 255); } }
    public static Color SpringGreen { get { return new Color32(0, 255, 127, 255); } }
    public static Color SteelBlue { get { return new Color32(70, 130, 180, 255); } }
    public static Color Tan { get { return new Color32(210, 180, 140, 255); } }
    public static Color Teal { get { return new Color32(0, 128, 128, 255); } }
    public static Color Thistle { get { return new Color32(216, 191, 216, 255); } }
    public static Color Tomato { get { return new Color32(255, 99, 71, 255); } }
    public static Color Turquoise { get { return new Color32(64, 224, 208, 255); } }
    public static Color Violet { get { return new Color32(238, 130, 238, 255); } }
    public static Color Wheat { get { return new Color32(245, 222, 179, 255); } }
    public static Color White { get { return new Color32(255, 255, 255, 255); } }
    public static Color WhiteSmoke { get { return new Color32(245, 245, 245, 255); } }
    public static Color Yellow { get { return new Color32(255, 255, 0, 255); } }
    public static Color YellowGreen { get { return new Color32(154, 205, 50, 255); } }

    //====================================================================================================
    //====================================================================================================

    public enum ColorEnum
    {
        AliceBlue,
        AntiqueWhite,
        Aqua,
        Aquamarine,
        Azure,
        Beige,
        Bisque,
        Black,
        BlanchedAlmond,
        Blue,
        BlueViolet,
        Brown,
        Burlywood,
        CadetBlue,
        Chartreuse,
        Chocolate,
        Coral,
        CornflowerBlue,
        Cornsilk,
        Crimson,
        Cyan,
        DarkBlue,
        DarkCyan,
        DarkGoldenrod,
        DarkGray,
        DarkGreen,
        DarkKhaki,
        DarkMagenta,
        DarkOliveGreen,
        DarkOrange,
        DarkOrchid,
        DarkRed,
        DarkSalmon,
        DarkSeaGreen,
        DarkSlateBlue,
        DarkSlateGray,
        DarkTurquoise,
        DarkViolet,
        DeepPink,
        DeepSkyBlue,
        DimGray,
        DodgerBlue,
        FireBrick,
        FloralWhite,
        ForestGreen,
        Fuchsia,
        Gainsboro,
        GhostWhite,
        Gold,
        Goldenrod,
        Gray,
        Green,
        GreenYellow,
        Honeydew,
        HotPink,
        IndianRed,
        Indigo,
        Ivory,
        Khaki,
        Lavender,
        Lavenderblush,
        LawnGreen,
        LemonChiffon,
        LightBlue,
        LightCoral,
        LightCyan,
        LightGoldenrodYellow,
        LightGray,
        LightGreen,
        LightPink,
        LightSalmon,
        LightSeaGreen,
        LightSkyBlue,
        LightSlateGray,
        LightSteelBlue,
        LightYellow,
        Lime,
        LimeGreen,
        Linen,
        Magenta,
        Maroon,
        MediumAquamarine,
        MediumBlue,
        MediumOrchid,
        MediumPurple,
        MediumSeaGreen,
        MediumSlateBlue,
        MediumSpringGreen,
        MediumTurquoise,
        MediumVioletRed,
        MidnightBlue,
        Mintcream,
        MistyRose,
        Moccasin,
        NavajoWhite,
        Navy,
        OldLace,
        Olive,
        Olivedrab,
        Orange,
        OrangeRed,
        Orchid,
        PaleGoldenrod,
        PaleGreen,
        PaleTurquoise,
        PaleVioletRed,
        PapayaWhip,
        PeachPuff,
        Peru,
        Pink,
        Plum,
        PowderBlue,
        Purple,
        Red,
        RosyBrown,
        RoyalBlue,
        SaddleBrown,
        Salmon,
        SandyBrown,
        SeaGreen,
        Seashell,
        Sienna,
        Silver,
        SkyBlue,
        SlateBlue,
        SlateGray,
        Snow,
        SpringGreen,
        SteelBlue,
        Tan,
        Teal,
        Thistle,
        Tomato,
        Turquoise,
        Violet,
        Wheat,
        White,
        WhiteSmoke,
        Yellow,
        YellowGreen
    }

    public static Color GetColor(ColorEnum color)
    {
        switch (color)
        {
            case ColorEnum.AliceBlue: return AliceBlue;
            case ColorEnum.AntiqueWhite: return AntiqueWhite;
            case ColorEnum.Aqua: return Aqua;
            case ColorEnum.Aquamarine: return Aquamarine;
            case ColorEnum.Azure: return Azure;
            case ColorEnum.Beige: return Beige;
            case ColorEnum.Bisque: return Bisque;
            case ColorEnum.Black: return Black;
            case ColorEnum.BlanchedAlmond: return BlanchedAlmond;
            case ColorEnum.Blue: return Blue;
            case ColorEnum.BlueViolet: return BlueViolet;
            case ColorEnum.Brown: return Brown;
            case ColorEnum.Burlywood: return Burlywood;
            case ColorEnum.CadetBlue: return CadetBlue;
            case ColorEnum.Chartreuse: return Chartreuse;
            case ColorEnum.Chocolate: return Chocolate;
            case ColorEnum.Coral: return Coral;
            case ColorEnum.CornflowerBlue: return CornflowerBlue;
            case ColorEnum.Cornsilk: return Cornsilk;
            case ColorEnum.Crimson: return Crimson;
            case ColorEnum.Cyan: return Cyan;
            case ColorEnum.DarkBlue: return DarkBlue;
            case ColorEnum.DarkCyan: return DarkCyan;
            case ColorEnum.DarkGoldenrod: return DarkGoldenrod;
            case ColorEnum.DarkGray: return DarkGray;
            case ColorEnum.DarkGreen: return DarkGreen;
            case ColorEnum.DarkKhaki: return DarkKhaki;
            case ColorEnum.DarkMagenta: return DarkMagenta;
            case ColorEnum.DarkOliveGreen: return DarkOliveGreen;
            case ColorEnum.DarkOrange: return DarkOrange;
            case ColorEnum.DarkOrchid: return DarkOrchid;
            case ColorEnum.DarkRed: return DarkRed;
            case ColorEnum.DarkSalmon: return DarkSalmon;
            case ColorEnum.DarkSeaGreen: return DarkSeaGreen;
            case ColorEnum.DarkSlateBlue: return DarkSlateBlue;
            case ColorEnum.DarkSlateGray: return DarkSlateGray;
            case ColorEnum.DarkTurquoise: return DarkTurquoise;
            case ColorEnum.DarkViolet: return DarkViolet;
            case ColorEnum.DeepPink: return DeepPink;
            case ColorEnum.DeepSkyBlue: return DeepSkyBlue;
            case ColorEnum.DimGray: return DimGray;
            case ColorEnum.DodgerBlue: return DodgerBlue;
            case ColorEnum.FireBrick: return FireBrick;
            case ColorEnum.FloralWhite: return FloralWhite;
            case ColorEnum.ForestGreen: return ForestGreen;
            case ColorEnum.Fuchsia: return Fuchsia;
            case ColorEnum.Gainsboro: return Gainsboro;
            case ColorEnum.GhostWhite: return GhostWhite;
            case ColorEnum.Gold: return Gold;
            case ColorEnum.Goldenrod: return Goldenrod;
            case ColorEnum.Gray: return Gray;
            case ColorEnum.Green: return Green;
            case ColorEnum.GreenYellow: return GreenYellow;
            case ColorEnum.Honeydew: return Honeydew;
            case ColorEnum.HotPink: return HotPink;
            case ColorEnum.IndianRed: return IndianRed;
            case ColorEnum.Indigo: return Indigo;
            case ColorEnum.Ivory: return Ivory;
            case ColorEnum.Khaki: return Khaki;
            case ColorEnum.Lavender: return Lavender;
            case ColorEnum.Lavenderblush: return Lavenderblush;
            case ColorEnum.LawnGreen: return LawnGreen;
            case ColorEnum.LemonChiffon: return LemonChiffon;
            case ColorEnum.LightBlue: return LightBlue;
            case ColorEnum.LightCoral: return LightCoral;
            case ColorEnum.LightCyan: return LightCyan;
            case ColorEnum.LightGoldenrodYellow: return LightGoldenrodYellow;
            case ColorEnum.LightGray: return LightGray;
            case ColorEnum.LightGreen: return LightGreen;
            case ColorEnum.LightPink: return LightPink;
            case ColorEnum.LightSalmon: return LightSalmon;
            case ColorEnum.LightSeaGreen: return LightSeaGreen;
            case ColorEnum.LightSkyBlue: return LightSkyBlue;
            case ColorEnum.LightSlateGray: return LightSlateGray;
            case ColorEnum.LightSteelBlue: return LightSteelBlue;
            case ColorEnum.LightYellow: return LightYellow;
            case ColorEnum.Lime: return Lime;
            case ColorEnum.LimeGreen: return LimeGreen;
            case ColorEnum.Linen: return Linen;
            case ColorEnum.Magenta: return Magenta;
            case ColorEnum.Maroon: return Maroon;
            case ColorEnum.MediumAquamarine: return MediumAquamarine;
            case ColorEnum.MediumBlue: return MediumBlue;
            case ColorEnum.MediumOrchid: return MediumOrchid;
            case ColorEnum.MediumPurple: return MediumPurple;
            case ColorEnum.MediumSeaGreen: return MediumSeaGreen;
            case ColorEnum.MediumSlateBlue: return MediumSlateBlue;
            case ColorEnum.MediumSpringGreen: return MediumSpringGreen;
            case ColorEnum.MediumTurquoise: return MediumTurquoise;
            case ColorEnum.MediumVioletRed: return MediumVioletRed;
            case ColorEnum.MidnightBlue: return MidnightBlue;
            case ColorEnum.Mintcream: return Mintcream;
            case ColorEnum.MistyRose: return MistyRose;
            case ColorEnum.Moccasin: return Moccasin;
            case ColorEnum.NavajoWhite: return NavajoWhite;
            case ColorEnum.Navy: return Navy;
            case ColorEnum.OldLace: return OldLace;
            case ColorEnum.Olive: return Olive;
            case ColorEnum.Olivedrab: return OliveDrab;
            case ColorEnum.Orange: return Orange;
            case ColorEnum.OrangeRed: return OrangeRed;
            case ColorEnum.Orchid: return Orchid;
            case ColorEnum.PaleGoldenrod: return PaleGoldenrod;
            case ColorEnum.PaleGreen: return PaleGreen;
            case ColorEnum.PaleTurquoise: return PaleTurquoise;
            case ColorEnum.PaleVioletRed: return PaleVioletRed;
            case ColorEnum.PapayaWhip: return PapayaWhip;
            case ColorEnum.PeachPuff: return PeachPuff;
            case ColorEnum.Peru: return Peru;
            case ColorEnum.Pink: return Pink;
            case ColorEnum.Plum: return Plum;
            case ColorEnum.PowderBlue: return PowderBlue;
            case ColorEnum.Purple: return Purple;
            case ColorEnum.Red: return Red;
            case ColorEnum.RosyBrown: return RosyBrown;
            case ColorEnum.RoyalBlue: return RoyalBlue;
            case ColorEnum.SaddleBrown: return SaddleBrown;
            case ColorEnum.Salmon: return Salmon;
            case ColorEnum.SandyBrown: return SandyBrown;
            case ColorEnum.SeaGreen: return SeaGreen;
            case ColorEnum.Seashell: return Seashell;
            case ColorEnum.Sienna: return Sienna;
            case ColorEnum.Silver: return Silver;
            case ColorEnum.SkyBlue: return SkyBlue;
            case ColorEnum.SlateBlue: return SlateBlue;
            case ColorEnum.SlateGray: return SlateGray;
            case ColorEnum.Snow: return Snow;
            case ColorEnum.SpringGreen: return SpringGreen;
            case ColorEnum.SteelBlue: return SteelBlue;
            case ColorEnum.Tan: return Tan;
            case ColorEnum.Teal: return Teal;
            case ColorEnum.Thistle: return Thistle;
            case ColorEnum.Tomato: return Tomato;
            case ColorEnum.Turquoise: return Turquoise;
            case ColorEnum.Violet: return Violet;
            case ColorEnum.Wheat: return Wheat;
            case ColorEnum.White: return White;
            case ColorEnum.WhiteSmoke: return WhiteSmoke;
            case ColorEnum.Yellow: return Yellow;
            case ColorEnum.YellowGreen: return YellowGreen;
            default: return Black;
        }
    }
}