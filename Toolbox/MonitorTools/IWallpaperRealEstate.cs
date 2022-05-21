﻿using aemarcoCommons.Toolbox.PictureTools;
using System.Drawing;

namespace aemarcoCommons.Toolbox.MonitorTools
{
    public interface IWallpaperRealEstate : IPictureInPicture
    {
        string DeviceName { get; }
        RealEstateType Type { get; }

        void SetWallpaper(Image image, Color? background = null);
    }


}