using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KeyBoard.Model
{
    public interface IEnKeyBoard
    {
        string SetText(int startPosition,string text, string symbol);
        void TextCopy(string text);
        void TextToImageCopy(string text);
        int CountSymbols(string text);
    }

    public class EnKeyBoardBL : IEnKeyBoard
    {
        public int CountSymbols(string text)
        {
            return text.Length;
        }

        public string SetText(int startPosition, string text, string symbol)
        {
            if (text.Length == startPosition)
            {
                return text += symbol;
            }
            else
            {
                return text.Insert(startPosition, symbol);
            }
        }

        public void TextCopy(string text)
        {
            Clipboard.SetText(text);
        }

        public void TextToImageCopy(string text)
        {
            Bitmap bitmap = new Bitmap(text.Length * 30, 90, PixelFormat.Format64bppArgb);
            Graphics graphics = Graphics.FromImage(bitmap);
            graphics.Clear(Color.White);
            graphics.DrawString(text, new Font("Arial", 50, FontStyle.Regular), new SolidBrush(Color.FromArgb(0, 0, 0)), new PointF(0.4F, 2.4F));

            Clipboard.SetImage(bitmap);
            bitmap.Dispose();
        }
    }
}
