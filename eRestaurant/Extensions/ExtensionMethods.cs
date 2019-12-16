﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace eRestaurant.Extensions
{
    public static class ExtensionMethods
    {
        public static byte[] ToByteArray(this Image image)
        {
            using var ms = new MemoryStream();
            image.Save(ms, ImageFormat.Jpeg);
            return ms.ToArray();
        }
    }
}