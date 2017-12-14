namespace Comet.UserControls
{
    using System.Drawing;
    using System.Windows.Forms;

    using Comet.Properties;

    internal class Banner
    {
        public static void DrawBanner(Graphics graphics, string caption, string subText, Padding padding)
        {
            // Draw icon
            Bitmap _icon = Resources.Comet.ToBitmap();

            graphics.DrawImage(_icon, new Point(padding.Left, padding.Top));
            
            // Draw caption
            graphics.DrawString(caption, new Font("Microsoft Sans Serif", 10F, FontStyle.Bold), new SolidBrush(Color.Black), new PointF(padding.Left + _icon.Width + 5, padding.Top));

            // Draw sub text
            graphics.DrawString(subText, new Font("Microsoft Sans Serif", 8.25F), new SolidBrush(Color.Black), new PointF(padding.Left + _icon.Width + 5, padding.Top + 20));
        }
    }
}