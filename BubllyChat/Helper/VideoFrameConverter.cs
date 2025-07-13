using Microsoft.MixedReality.WebRTC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace BubllyChat.Helper
{
    public static class VideoFrameConverter
    {
        public static WriteableBitmap ConvertToBitmap(Argb32VideoFrame frame)
        {
            int width = (int)frame.width;
            int height = (int)frame.height;
            int stride = (int)frame.stride;

            var bitmap = new WriteableBitmap(width, height, 96, 96, PixelFormats.Bgra32, null);
            bitmap.Lock();

            unsafe
            {
                Buffer.MemoryCopy(
                    source: frame.data.ToPointer(),
                    destination: bitmap.BackBuffer.ToPointer(),
                    destinationSizeInBytes: stride * height,
                    sourceBytesToCopy: stride * height);
            }

            bitmap.AddDirtyRect(new Int32Rect(0, 0, width, height));
            bitmap.Unlock();

            return bitmap;
        }
    }
}
