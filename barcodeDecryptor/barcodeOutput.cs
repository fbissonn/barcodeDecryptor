//
//  barcodeOutput.cs
//
//  Author:
//       Francois Bissonnette <fbissonn@gmail.com>
//
//  Copyright (c) 2015 Francois Bissonnette
//
//  This program is free software: you can redistribute it and/or modify
//  it under the terms of the GNU General Public License as published by
//  the Free Software Foundation, either version 3 of the License, or
//  (at your option) any later version.
//
//  This program is distributed in the hope that it will be useful,
//  but WITHOUT ANY WARRANTY; without even the implied warranty of
//  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//  GNU General Public License for more details.
//
//  You should have received a copy of the GNU General Public License
//  along with this program.  If not, see <http://www.gnu.org/licenses/>.
using System;
using System.IO;
using Gtk;
using Cairo;
using barcodeDecryptor;
using Microsoft.CSharp;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Drawing.Drawing2D;


namespace barcodeDecryptor
{
 
    /// <summary>
    /// Barcode output.
    /// </summary>
    public class barcodeOutput
    {
        
        /// <summary>
        /// Barcode enum.
        /// </summary>
        public enum barcodeEnum:int
        {
            //Australian barcode
            BC_APC,
            BC_AZTEC,
            BC_CODABAR,
            BC_CODE11,
            BC_CODE25_NON_INTERLEAVED,
            BC_CODE25_INTERLEAVED,
            BC_CODE39,
            BC_EXT_CODE39,
            BC_CODE49,
            BC_CODE93,
            BC_CODE128,
            BC_COMPOSITE_CODE,
            BC_CPC_BINARY,
            BC_DATAMATRIX,
            BC_DX_FILMEDGE,
            BC_EAN_2,
            BC_EAN_5,
            BC_EAN_8,
            BC_EAN_13,
            BC_EAN_BOOKLAND,
            BC_INDUSTRIAL_2_of_5,
            BC_INTERLEAVED_2_OF_5,
            BC_ITF_14,
            BC_JAN,
            BC_KARTRAK,
            BC_LOGMARS,
            BC_MATRIX_CODE,
            BC_MSI_PLESSEY,
            BC_OPC,
            BC_PDF_417,
            BC_PHARMACODE,
            BC_PLANET,
            BC_PLESSEY,
            BC_POSTBAR,
            BC_POSTNET,
            BC_QRCODE,
            BC_RM4SCC_KIX,
            BC_SCC14,
            BC_STANDARD_2_OF_5,
            BC_TELEPEN,
            BC_UCC_EAN128,
            BC_UCC_EAN_SHIPPING,
            BC_UPC_SHIPPING_CONTAINER_CODE,
            BC_UPC_A,
            BC_UPC_E,
            NUM_BARCODE}

        ;

        /// <summary>
        /// The width of the pen.
        /// </summary>
        private const int penWidth = 1;

        /// <summary>
        /// Initializes a new instance of the <see cref="barcodeDecryptor.barcodeOutput"/> class.
        /// </summary>
        public barcodeOutput()
        {
            //throw new NotImplementedException("Not implemented yet !!");
        }

        /// <summary>
        /// Draws the linear barcode.
        /// </summary>
        /// <returns>The linear barcode.</returns>
        /// <param name="encryptedBarcode">Encrypted barcode.</param>
        /// <param name="decryptedBarcode">Decrypted barcode.</param>
        /// <param name="barcodeType">Barcode type.</param>
        /// <param name="barcodeValueIndex">Barcode value index.</param>
        /// <param name="magnification">Magnification.</param>
        public Gdk.Pixbuf DrawLinearBarcode(String encryptedBarcode, String decryptedBarcode, int barcodeType, int[] barcodeValueIndex, int magnification)
        {
            Bitmap bmp = new Bitmap(150, 150);
            System.Drawing.Graphics g = System.Drawing.Graphics.FromImage(bmp);


            switch (barcodeType)
            {
                case (int) barcodeEnum.BC_EAN_8:
                    this.drawEAN8(g, decryptedBarcode, encryptedBarcode, barcodeValueIndex);
                    break;
                case (int) barcodeEnum.BC_EAN_13:
                    this.drawEAN13(g, decryptedBarcode, encryptedBarcode, barcodeValueIndex);
                    break;
            }
            ;


        
            //System.Drawing.Font ff= new System.Drawing.Font (System.Drawing.FontFamily.GenericMonospace, 12.0F, FontStyle.Italic, GraphicsUnit.Pixel);
            //g.DrawString("hello world",ff,Brushes.Red,new PointF(0,0));
            MemoryStream ms = new MemoryStream();
            bmp.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
            bmp.Save("test.jpg",System.Drawing.Imaging.ImageFormat.Jpeg);
            ms.Position = 0;
            Gtk.Application.Init();
            Gdk.Pixbuf pb = new Gdk.Pixbuf(ms); 
            //image1.Pixbuf=pb;


            return pb;
        }

        /*public Gdk.Pixbuf SaveBarcode(String encryptedBarcode, Bitmap bmp)
      {
        //Bitmap bmp=new Bitmap(300,300);
        System.Drawing.Graphics g=System.Drawing.Graphics.FromImage(bmp);

        MemoryStream ms = new MemoryStream ();
        bmp.Save (ms, System.Drawing.Imaging.ImageFormat.Png);
        //bmp.Save("/home/francois/test.jpg",System.Drawing.Imaging.ImageFormat.Jpeg);
        ms.Position = 0;
        Gtk.Application.Init();
        Gdk.Pixbuf pb= new Gdk.Pixbuf (ms); 
        //image1.Pixbuf=pb;


        return pb;
      }*/

        private int drawEAN13(System.Drawing.Graphics g, String decryptedBarcode, String encryptedBarcode, int[] barcodeValueIndex)
        {
            System.Drawing.Pen BlackColumn = new System.Drawing.Pen(System.Drawing.Color.Black, penWidth);
            System.Drawing.Pen whiteColumn = new System.Drawing.Pen(System.Drawing.Color.White, penWidth);

            // create a white background to the image
            System.Drawing.SolidBrush whiteBrush = new System.Drawing.SolidBrush(System.Drawing.Color.White);
            System.Drawing.Rectangle fullImageRectangle = new System.Drawing.Rectangle(0, 0, 150,150);
            System.Drawing.Region fullImageRegion = new System.Drawing.Region(fullImageRectangle);
            g.FillRegion(whiteBrush, fullImageRegion);

            // creating the barcode image
            int StartPosX = 15;
            int StartPosY = 15;
            int endPosY = 100;
            int lenghtToAdd = 15;
            System.Drawing.Font ff = new System.Drawing.Font(System.Drawing.FontFamily.GenericSansSerif, 11.0F, FontStyle.Regular, GraphicsUnit.Pixel);



            int space = 0;
            g.DrawString(decryptedBarcode.Substring(0, 1), ff, Brushes.Black, new PointF(5, endPosY - 5));
            for (int pos = 1; pos < decryptedBarcode.Length; pos++)
            {

                if (pos == 7)
                {
                    ++space;
                }
                else
                {
                    //space = 0;
                }

                g.DrawString(decryptedBarcode.Substring(pos, 1), ff, Brushes.Black, new PointF((StartPosX + 2 - space - space) + (((pos + space) - 1) * 7), endPosY));
            }


            for (int pos = 0; pos < encryptedBarcode.Length; pos++)
            {
                if (encryptedBarcode[pos].CompareTo('1') == 0)
                {
                    int iToAdd = 0;
                    if (barcodeValueIndex[pos] == 0)
                    {
                        iToAdd = lenghtToAdd;
                    }
                    else
                    {
                        iToAdd = 0;
                    }
                    g.DrawLine(BlackColumn, StartPosX + (pos * penWidth), StartPosY, StartPosX + (pos * penWidth), endPosY + iToAdd);        

                }
                else
                {
                    g.DrawLine(whiteColumn, StartPosX + (pos * penWidth), StartPosY, StartPosX + (pos * penWidth), endPosY);        
                }
            }
            BlackColumn.Dispose();
            whiteColumn.Dispose();
            whiteBrush.Dispose(); 
            return 0;

        }

        private int drawEAN8(System.Drawing.Graphics g, String decryptedBarcode, String encryptedBarcode, int[] barcodeValueIndex)
        {
            System.Drawing.Pen BlackColumn = new System.Drawing.Pen(System.Drawing.Color.Black, penWidth);
            System.Drawing.Pen whiteColumn = new System.Drawing.Pen(System.Drawing.Color.White, penWidth);

            // create a white background to the image
            System.Drawing.SolidBrush whiteBrush = new System.Drawing.SolidBrush(System.Drawing.Color.White);
            System.Drawing.Rectangle fullImageRectangle = new System.Drawing.Rectangle(0, 0, 300, 300);
            System.Drawing.Region fullImageRegion = new System.Drawing.Region(fullImageRectangle);
            g.FillRegion(whiteBrush, fullImageRegion);

            // creating the barcode image
            int StartPosX = 15;
            int StartPosY = 15;
            int endPosY = 135;
            int lenghtToAdd = 20;
            System.Drawing.Font ff = new System.Drawing.Font(System.Drawing.FontFamily.GenericSansSerif, 11.0F, FontStyle.Regular, GraphicsUnit.Pixel);



            int space = 0;
            g.DrawString(decryptedBarcode.Substring(0, 1), ff, Brushes.Black, new PointF(5, endPosY - 10));
            for (int pos = 1; pos < decryptedBarcode.Length; pos++)
            {

                if (pos == 5)
                {
                    ++space;
                }
                else
                {
                    //space = 0;
                }

                g.DrawString(decryptedBarcode.Substring(pos, 1), ff, Brushes.Black, new PointF((StartPosX + 2 - space - space) + (((pos + space) - 1) *5), endPosY));


            }


            for (int pos = 0; pos < encryptedBarcode.Length; pos++)
            {
                if (encryptedBarcode[pos].CompareTo('1') == 0)
                {
                    int iToAdd = 0;
                    if (barcodeValueIndex[pos] == 0)
                    {
                        iToAdd = lenghtToAdd;
                    }
                    else
                    {
                        iToAdd = 0;
                    }
                    g.DrawLine(BlackColumn, StartPosX + (pos * penWidth), StartPosY, StartPosX + (pos * penWidth), endPosY + iToAdd);        

                }
                else
                {
                    g.DrawLine(whiteColumn, StartPosX + (pos * penWidth), StartPosY, StartPosX + (pos * penWidth), endPosY);        
                }
            }
            BlackColumn.Dispose();
            whiteColumn.Dispose();
            whiteBrush.Dispose(); 
            return 0;

        }
    }
}

