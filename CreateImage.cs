using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;

namespace BatchSend
{
    public class Rand
    {
        public static int GetRandomSeed()
        {
            byte[] bytes = new byte[4];
            System.Security.Cryptography.RNGCryptoServiceProvider rng = new System.Security.Cryptography.RNGCryptoServiceProvider();
            rng.GetBytes(bytes);
            return BitConverter.ToInt32(bytes, 0);
        }
        public static int getNext(int n)
        {
            Random random = new Random(GetRandomSeed());
            return random.Next(n);
        }
    }
    /**/
    /// <summary>
    /// WaterMark
    /// </summary>
    public class Watermark
    {
        private int _width;
        private int _height;
        private string _fontFamily;
        private int _fontSize;
        private bool _adaptable;
        private FontStyle _fontStyle;
        private bool _shadow;
        private string _backgroundImage;
        private Color _bgColor;
        private int _left;
        private string _resultImage;
        private string _text;
        private int _top;
        private int _alpha;
        private int _red;
        private int _green;
        private int _blue;
        private long _quality;

        // custom
        private int _maxLineHeight;
        private int _minLineHeight;
        public bool _isRandomFontSize;
        private List<string> _fontFamilyList;
        public bool _isRandomFont;
        public int _imageBorder;

        public Watermark()
        {
            //
            // TODO: Add constructor logic here
            //
            _width = 439;
            _height = 454;
            _fontFamily = "华文行楷";
            _fontSize = 20;
            _fontStyle = FontStyle.Regular;
            _adaptable = true;
            _shadow = false;
            _left = 0;
            _top = 0;
            _alpha = 255;
            _red = 0;
            _green = 0;
            _blue = 0;
            _backgroundImage = "";
            _quality = 100;
            _bgColor = Color.FromArgb(255, 229, 229, 229);

            _maxLineHeight = 20;
            _minLineHeight = 20;
            _isRandomFontSize = false;
            _isRandomFont = false;

            _fontFamilyList = new List<string>();
            _imageBorder = 0;
        }
        /**/
        /// <summary>
        /// 字体
        /// </summary>
        public string FontFamily
        {
            set { this._fontFamily = value; }
            get { return this._fontFamily; }
        }
        /**/
        /// <summary>
        /// 文字大小
        /// </summary>
        public int FontSize
        {
            set { this._fontSize = value; }
            get { return this._fontSize; }
        }
        /**/
        /// <summary>
        /// 文字风格
        /// </summary>
        public FontStyle FontStyle
        {
            get { return _fontStyle; }
            set { _fontStyle = value; }
        }
        /**/
        /// <summary>
        /// 透明度0-255,255表示不透明
        /// </summary>
        public int Alpha
        {
            get { return _alpha; }
            set { _alpha = value; }
        }
        /**/
        /// <summary>
        /// 水印文字是否使用阴影
        /// </summary>
        public bool Shadow
        {
            get { return _shadow; }
            set { _shadow = value; }
        }
        public int Red
        {
            get { return _red; }
            set { _red = value; }
        }
        public int Green
        {
            get { return _green; }
            set { _green = value; }
        }
        public int Blue
        {
            get { return _blue; }
            set { _blue = value; }
        }
        /**/
        /// <summary>
        /// 底图
        /// </summary>
        public string BackgroundImage
        {
            set { this._backgroundImage = value; }
        }
        /**/
        /// <summary>
        /// 水印文字的左边距
        /// </summary>
        public int Left
        {
            set { this._left = value; }
        }
        /**/
        /// <summary>
        /// 水印文字的顶边距
        /// </summary>
        public int Top
        {
            set { this._top = value; }
        }
        /**/
        /// <summary>
        /// 生成后的图片
        /// </summary>
        public string ResultImage
        {
            set { this._resultImage = value; }
        }
        /**/
        /// <summary>
        /// 水印文本
        /// </summary>
        public string Text
        {
            set { this._text = value; }
            get { return this._text; }
        }
        /**/
        /// <summary>
        /// 生成图片的宽度
        /// </summary>
        public int Width
        {
            get { return _width; }
            set { _width = value; }
        }
        /**/
        /// <summary>
        /// 生成图片的高度
        /// </summary>
        public int Height
        {
            get { return _height; }
            set { _height = value; }
        }
        /**/
        /// <summary>
        /// 若文字太大，是否根据背景图来调整文字大小，默认为适应
        /// </summary>
        public bool Adaptable
        {
            get { return _adaptable; }
            set { _adaptable = value; }
        }
        public Color BgColor
        {
            get { return _bgColor; }
            set { _bgColor = value; }
        }
        /**/
        /// <summary>
        /// 输出图片质量，质量范围0-100,类型为long
        /// </summary>
        public long Quality
        {
            get { return _quality; }
            set { _quality = value; }
        }

        /**/
        /// <summary>
        /// 扩展
        /// </summary>
        public int MaxLineHeight
        {
            get { return _maxLineHeight; }
            set { _maxLineHeight = value; }
        }
        public int MinLineHeight
        {
            get { return _minLineHeight; }
            set { _minLineHeight = value; }
        }
        public List<string> FontFamilyList
        {
            get { return _fontFamilyList; }
            set { _fontFamilyList = value; }
        }

        /**/
        /// <summary>
        /// 立即生成水印效果图
        /// </summary>
        /// <returns>生成成功返回true,否则返回false</returns>
        public bool Create()
        {
            try
            {
                Color c1 = Color.FromArgb(250, Rand.getNext(Rand.getNext(255)), Rand.getNext(Rand.getNext(Rand.getNext(255))), Rand.getNext(Rand.getNext(255)));
                Bitmap bitmap;
                Bitmap bitmap2;
                Graphics g;
                int borderPix = 0;
                //使用纯背景色
                if (this._backgroundImage.Trim() == "")
                {

                    bitmap2 = new Bitmap(this._width / 2, this._height / 2, PixelFormat.Format64bppArgb);
                    g = Graphics.FromImage(bitmap2);
                   // g.Clear(this._bgColor);
                    bitmap2 = randomBitmap(bitmap2);
                    borderPix = Rand.getNext(_imageBorder);
                   
                }
                else
                {
                    bitmap2 = new Bitmap(Image.FromFile(this._backgroundImage));
                    borderPix = Rand.getNext(_imageBorder);
                    borderPix -= _imageBorder / 2;

                   // bitmap = new Bitmap(bitmap2, bitmap2.Width + borderPix, bitmap2.Height + borderPix);

                }

                bitmap = new Bitmap(bitmap2, this._width + borderPix, this._height + borderPix);
            //    bitmap = fogProcess(bitmap);
                g = Graphics.FromImage(bitmap);

                g.SmoothingMode = SmoothingMode.HighQuality;
                g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                g.CompositingQuality = CompositingQuality.HighQuality;
                Font f = new Font(_fontFamily, _fontSize, _fontStyle);
                SizeF size = g.MeasureString(_text, f);
                // 调整文字大小直到能适应图片尺寸
                while (_adaptable == true && size.Width > bitmap.Width)
                {
                    _fontSize--;
                    f = new Font(_fontFamily, _fontSize, _fontStyle);
                    size = g.MeasureString(_text, f);
                }
                Brush b = new SolidBrush(Color.FromArgb(_alpha, _red, _green, _blue));
                string[] allLines = _text.Split(new string[] { "#l" }, StringSplitOptions.None);
                if (borderPix < 0)
                {
                    borderPix = 0;
                }
                PointF initPoint = new PointF(_left + 10, _top + 20);
                float fontHeight =0f;
                foreach (string line in allLines)
                {
                    fontHeight = parseTxt(ref g, line, ref f, initPoint);
                    if (_isRandomFontSize)
                    {
                        initPoint.Y += _maxLineHeight + 10;
                    }
                    else
                    {
                        initPoint.Y += fontHeight + 5 + Rand.getNext(5);
                    }
                    initPoint.X = _left + 2;
                }

                initPoint.Y += fontHeight+5+Rand.getNext(5);

                parseTxt(ref g, Guid.NewGuid().ToString().Replace('-',' '), ref f, initPoint);

                bitmap.Save(this._resultImage, ImageFormat.Jpeg);
                bitmap.Dispose();
                g.Dispose();
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Creates color with corrected brightness.
        /// </summary>
        /// <param name="color">Color to correct.</param>
        /// <param name="correctionFactor">The brightness correction factor. Must be between -1 and 1. 
        /// Negative values produce darker colors.</param>
        /// <returns>
        /// Corrected <see cref="Color"/> structure.
        /// </returns>
        public static Color ChangeColorBrightness(Color color, float correctionFactor)
        {
            float red = (float)color.R;
            float green = (float)color.G;
            float blue = (float)color.B;

            if (correctionFactor < 0)
            {
                correctionFactor = 1 + correctionFactor;
                red *= correctionFactor;
                green *= correctionFactor;
                blue *= correctionFactor;
            }
            else
            {
                red = (255 - red) * correctionFactor + red;
                green = (255 - green) * correctionFactor + green;
                blue = (255 - blue) * correctionFactor + blue;
            }

            return Color.FromArgb(color.A, (int)red, (int)green, (int)blue);
        }

        public static Bitmap GetThumbnail(Bitmap b, int destHeight, int destWidth)
        {
            System.Drawing.Image imgSource = b;
            System.Drawing.Imaging.ImageFormat thisFormat = imgSource.RawFormat;
            int sW = 0, sH = 0;
            // 按比例缩放           
            int sWidth = imgSource.Width;
            int sHeight = imgSource.Height;
            if (sHeight > destHeight || sWidth > destWidth)
            {
                if ((sWidth * destHeight) > (sHeight * destWidth))
                {
                    sW = destWidth;
                    sH = (destWidth * sHeight) / sWidth;
                }
                else
                {
                    sH = destHeight;
                    sW = (sWidth * destHeight) / sHeight;
                }
            }
            else
            {
                sW = sWidth;
                sH = sHeight;
            }
            Bitmap outBmp = new Bitmap(destWidth, destHeight);
            Graphics g = Graphics.FromImage(outBmp);
            g.Clear(Color.Transparent);
            // 设置画布的描绘质量         
            g.CompositingQuality = CompositingQuality.HighQuality;
            g.SmoothingMode = SmoothingMode.HighQuality;
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;
            g.DrawImage(imgSource, new Rectangle((destWidth - sW) / 2, (destHeight - sH) / 2, sW, sH), 0, 0, imgSource.Width, imgSource.Height, GraphicsUnit.Pixel);
            g.Dispose();
            // 以下代码为保存图片时，设置压缩质量     
            EncoderParameters encoderParams = new EncoderParameters();
            long[] quality = new long[1];
            quality[0] = 100;
            EncoderParameter encoderParam = new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, quality);
            encoderParams.Param[0] = encoderParam;
            imgSource.Dispose();
            return outBmp;
        }
        private Bitmap softProcess(Bitmap bitmap)
        {
            int Height = bitmap.Height;
            int Width = bitmap.Width;
            Bitmap newBitmap = new Bitmap(Width, Height);
            Color pixel;
            int[] Gauss = { 1, 2, 1, 2, 4, 2, 1, 2, 1 };
            for (int x = 1; x < Width - 1; x++)
                for (int y = 1; y < Height - 1; y++)
                {
                    int r = 0, g = 0, b = 0;
                    int Index = 0;
                    for (int col = -1; col <= 1; col++)
                        for (int row = -1; row <= 1; row++)
                        {
                            pixel = bitmap.GetPixel(x + row, y + col);
                            r += pixel.R * Gauss[Index];
                            g += pixel.G * Gauss[Index];
                            b += pixel.B * Gauss[Index];
                            Index++;
                        }
                    r /= 16;
                    g /= 16;
                    b /= 16;
                    //处理颜色值溢出
                    r = r > 255 ? 255 : r;
                    r = r < 0 ? 0 : r;
                    g = g > 255 ? 255 : g;
                    g = g < 0 ? 0 : g;
                    b = b > 255 ? 255 : b;
                    b = b < 0 ? 0 : b;
                    newBitmap.SetPixel(x - 1, y - 1, Color.FromArgb(r, g, b));
                }
            return newBitmap;
        }
        private Bitmap randomBitmap(Bitmap bitmap)
        {
            int Height = bitmap.Height;
            int Width = bitmap.Width;
            Bitmap newBitmap = new Bitmap(Width, Height);
            Color pixel;
            for (int x = 0; x < Width ; x++)
                for (int y = 0; y < Height; y++)
                {
                    pixel = Watermark.ChangeColorBrightness(Color.FromArgb(Rand.getNext(255), Rand.getNext(255), Rand.getNext(255), Rand.getNext(255)),0.5f);
                    newBitmap.SetPixel(x, y, pixel);
                }
            return newBitmap;
        }
        private Bitmap fogProcess(Bitmap bitmap)
        {
            int Height = bitmap.Height;
            int Width = bitmap.Width;
            Bitmap newBitmap = new Bitmap(Width, Height);
            Color pixel;
            for (int x = 0; x < Width; x++)
                for (int y = 0; y < Height; y++)
                {
                    int k = Rand.getNext(123456);
                    //像素块大小
                    int dx = x + k % 100;
                    int dy = y + k % 100;
                    if (dx >= Width)
                        dx = Width - 1;
                    if (dy >= Height)
                        dy = Height - 1;
                    pixel = bitmap.GetPixel(dx, dy);
                    newBitmap.SetPixel(x, y, pixel);
                }
            return newBitmap;
        }

        private void drawText(ref Graphics g, string txt, Font f, Brush b, PointF ptf,Color bkColor)
        {
            StringFormat StrFormat = new StringFormat();
            StrFormat.Alignment = StringAlignment.Near;
            if (this._shadow)
            {
                Brush b2 = new SolidBrush(Color.FromArgb(90, 0, 0, 0));
                g.DrawString(txt, f, b2, ptf.X + 2, ptf.Y + 1);
            }
            SizeF size = g.MeasureString(txt.Trim(), f);
            SizeF size1 = g.MeasureString(txt, f);
            // 留白
           // Brush b3 = new SolidBrush(bkColorChangeColorBrightness(ColorUtil.GetRandomColor(),0.8f));
            Brush b3 = new SolidBrush(bkColor);
            g.FillRectangle(b3, ptf.X+(size1.Width-size.Width), ptf.Y, size.Width + 2, size.Height);
            g.DrawString(txt, f, b, ptf.X, ptf.Y, StrFormat);

        }

        private float parseTxt(ref Graphics g, string txt, ref Font f, PointF ptf)
        {
            FontStyle style = FontStyle.Regular;

            string fontFamily = null;
            int fontSize = -1;
            bool isBrushUsed = false;
            bool isAdd = false;

            List<string> symbols = new List<string>();
            Brush b = null;
            float rtnFloat = _height;

            Color bkColor = ChangeColorBrightness(ColorUtil.GetRandomColor(), 0.9f);
            for (int i = 0; i < txt.Length; )
            {
                bool isEnterIn = false;
                string startStr = txt.Substring(i, 1);
                while (startStr == "#")
                {
                    isEnterIn = true;
                    isAdd = false;
                    i++;
                    string symbol = txt.Substring(i, 1);
                    if (symbols.Count == 0
                        || symbols[symbols.Count - 1] != symbol)
                    {
                        symbols.Add(symbol);
                        isAdd = true;
                    }
                    else
                    {
                        symbols.RemoveAt(symbols.Count - 1);
                    }
                    style |= FontStyle.Bold;
                    switch (symbol)
                    {
                        case "i":
                            if (isAdd)
                            {
                                style |= FontStyle.Italic;
                            }
                            else
                            {
                                style &= ~FontStyle.Bold;
                            }
                            break;
                        case "u":
                            if (isAdd)
                            {
                                style |= FontStyle.Underline;
                            }
                            else
                            {
                                style &= ~FontStyle.Underline;
                            }
                            break;
                        case "r":
                            if (isAdd)
                            {
                                b = new SolidBrush(Color.FromArgb(_alpha, 255, 0, 0));
                                isBrushUsed = true;
                            }
                            else
                            {
                                isBrushUsed = false;
                            }
                            break;
                        case "g":
                            if (isAdd)
                            {
                                b = new SolidBrush(Color.Green);
                                isBrushUsed = true;
                            }
                            else
                            {
                                isBrushUsed = false;
                            }
                            break;
                        case "b":
                            if (isAdd)
                            {
                                b = new SolidBrush(Color.Blue);
                                isBrushUsed = true;
                            }
                            else
                            {
                                isBrushUsed = false;
                            }
                            break;
                        case "c":
                            if (isAdd)
                            {
                                fontSize = 2;
                            }
                            break;
                        default:
                            if (isAdd)
                            {
                                b = new SolidBrush(Color.Black);
                                isBrushUsed = true;
                            }
                            else
                            {
                                isBrushUsed = false;
                            }
                            break;
                    }
                    if (++i < txt.Length)
                    {
                        startStr = txt.Substring(i, 1);
                    }
                    else
                        startStr = "";

                }

                if (!isBrushUsed && b == null)
                {
                    b = new SolidBrush(ColorUtil.GetDarkerColor(ColorUtil.GetRandomColor()));
                }
                isAdd = false;
                int txtIdx = txt.IndexOf("#", i);
                bool allTxt = false;
                if (txtIdx < 0)
                {
                    txtIdx = txt.Length;
                }
                string renderText = "";
                renderText = txt.Substring(i, txtIdx - i);
                if (txtIdx <= i)
                {
                    continue;
                }
                if (_isRandomFont && fontFamily == null)
                {
                    int n = _fontFamilyList.Count;
                    if (n == 0)
                    {
                        fontFamily = _fontFamily;
                    }
                    else
                    {
                        int idx = Rand.getNext(n);
                        fontFamily = _fontFamilyList[idx];
                    }
                }
                else
                {
                    fontFamily = _fontFamily;
                }

                if (_isRandomFontSize && fontSize < 0)
                {
                    int n = _maxLineHeight - _minLineHeight;
                    fontSize = Rand.getNext(n) + _minLineHeight;
                }
                else
                {
                    fontSize = _fontSize;
                }
                style |= FontStyle.Bold;
                f = new Font(fontFamily, fontSize, style);
                drawText(ref g, renderText, f, b, ptf,bkColor);
                SizeF size = g.MeasureString(renderText, f);
                ptf.X += size.Width;
                rtnFloat = size.Height;

                i += renderText.Length;
            }

            return rtnFloat;

        }


    }

    public class ColorUtil
    {
        public static System.Drawing.Color GetRandomColor()
        {
            Random randomNum_1 = new Random(Guid.NewGuid().GetHashCode());
            System.Threading.Thread.Sleep(randomNum_1.Next(1));
            int int_Red = randomNum_1.Next(255);

            Random randomNum_2 = new Random((int)DateTime.Now.Ticks);
            int int_Green = randomNum_2.Next(255);

            Random randomNum_3 = new Random(Guid.NewGuid().GetHashCode());

            int int_Blue = randomNum_3.Next(255);
            int_Blue = (int_Red + int_Green > 380) ? int_Red + int_Green - 380 : int_Blue;
            int_Blue = (int_Blue > 255) ? 255 : int_Blue;


            return GetDarkerColor(System.Drawing.Color.FromArgb(int_Red, int_Green, int_Blue));
        }

        //获取加深颜色
        public static Color GetDarkerColor(Color color)
        {
            const int max = 255;
            int increase = new Random(Guid.NewGuid().GetHashCode()).Next(100, 255); //还可以根据需要调整此处的值


            int r = Math.Abs(Math.Min(color.R - increase, max));
            int g = Math.Abs(Math.Min(color.G - increase, max));
            int b = Math.Abs(Math.Min(color.B - increase, max));


            return Color.FromArgb(r, g, b);
        }
    }
}
