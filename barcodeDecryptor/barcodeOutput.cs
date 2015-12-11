﻿//
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
 

  public class barcodeOutput
  {
    public enum barcodeEnum{
      BC_EAN_13,
      NUM_BARCODE

    };
    private const int penWidth = 1;

    public barcodeOutput()
    {
        
    }

    public Gdk.Pixbuf DrawLinearBarcode(String encryptedBarcode, String decryptedBarcode, int barcodeType, int[] barcodeValueIndex, int magnification)
      {
        Bitmap bmp=new Bitmap(300,300);
        System.Drawing.Graphics g=System.Drawing.Graphics.FromImage(bmp);
        System.Drawing.Pen BlackColumn = new System.Drawing.Pen(System.Drawing.Color.Black,penWidth);
        System.Drawing.Pen whiteColumn = new System.Drawing.Pen(System.Drawing.Color.White,penWidth);

        // create a white background to the image
        System.Drawing.SolidBrush whiteBrush = new System.Drawing.SolidBrush(System.Drawing.Color.White);
        System.Drawing.Rectangle fullImageRectangle = new System.Drawing.Rectangle(0, 0, 300, 300);
        System.Drawing.Region fullImageRegion = new System.Drawing.Region(fullImageRectangle);
        g.FillRegion(whiteBrush, fullImageRegion);

        // creating the barcode image
        int StartPosX = 15;
        int StartPosY = 15;
        int endPosY = 135;
        int lenghtToAdd = 40;
        System.Drawing.Font ff= new System.Drawing.Font (System.Drawing.FontFamily.GenericSansSerif, 11.0F, FontStyle.Regular, GraphicsUnit.Pixel);


        int space = 0;
        g.DrawString(decryptedBarcode.Substring(0,1),ff,Brushes.Black,new PointF(5,endPosY-10));
        for (int pos = 1; pos < decryptedBarcode.Length; pos++)
        {

          if (pos == 7)
            {
              ++space;
            } else
            {
              //space = 0;
            }

            g.DrawString(decryptedBarcode.Substring(pos, 1), ff, Brushes.Black, new PointF((StartPosX+2-space-space) + (((pos+space)-1)*7), endPosY));

            
        }
        

        for (int pos = 0; pos < encryptedBarcode.Length; pos++)
        {
          if (encryptedBarcode[pos].CompareTo('1') == 0)
            {
              int iToAdd = 0;
              if (barcodeValueIndex[pos] == 0)
                {
                  iToAdd = lenghtToAdd;
                } else
                {
                  iToAdd = 0;
                }
                g.DrawLine(BlackColumn, StartPosX + (pos*penWidth), StartPosY, StartPosX + (pos*penWidth), endPosY + iToAdd);        

            } else
            {
                g.DrawLine(whiteColumn, StartPosX + (pos*penWidth), StartPosY, StartPosX + (pos*penWidth), endPosY);        
            }
        }
        
        //System.Drawing.Font ff= new System.Drawing.Font (System.Drawing.FontFamily.GenericMonospace, 12.0F, FontStyle.Italic, GraphicsUnit.Pixel);
        //g.DrawString("hello world",ff,Brushes.Red,new PointF(0,0));
        MemoryStream ms = new MemoryStream ();
        bmp.Save (ms, System.Drawing.Imaging.ImageFormat.Png);
        //bmp.Save("/home/francois/test.jpg",System.Drawing.Imaging.ImageFormat.Jpeg);
        ms.Position = 0;
        Gtk.Application.Init();
        Gdk.Pixbuf pb= new Gdk.Pixbuf (ms); 
        //image1.Pixbuf=pb;

        BlackColumn.Dispose();
        whiteColumn.Dispose();
        whiteBrush.Dispose(); 
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
  }
}

