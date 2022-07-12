using System.IO;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace TradeOptNight.Common
{
    internal class ImageProcess
    {
        public static System.Drawing.Image CaptureControlImage(UIElement source)
        {
            System.Drawing.Image result = null;

            double actualHeight = source.RenderSize.Height;
            double actualWidth = source.RenderSize.Width;

            int zoom = 1;

            double renderHeight = actualHeight * zoom;
            double renderWidth = actualWidth * zoom;

            RenderTargetBitmap renderTarget = new RenderTargetBitmap((int)renderWidth, (int)renderHeight, 96, 96, PixelFormats.Pbgra32);
            VisualBrush sourceBrush = new VisualBrush(source);

            DrawingVisual drawingVisual = new DrawingVisual();
            DrawingContext drawingContext = drawingVisual.RenderOpen();

            using (drawingContext)
            {
                drawingContext.PushTransform(new ScaleTransform(zoom, zoom));
                drawingContext.DrawRectangle(sourceBrush, null, new Rect(new Point(0, 0), new Point(actualWidth, actualHeight)));
            }
            renderTarget.Render(drawingVisual);

            PngBitmapEncoder encoder = new PngBitmapEncoder();
            encoder.Frames.Add(BitmapFrame.Create(renderTarget));

            using (MemoryStream ms = new MemoryStream())
            {
                encoder.Save(ms);
                result = System.Drawing.Image.FromStream(ms);
            }

            return result;
        }
    }
}