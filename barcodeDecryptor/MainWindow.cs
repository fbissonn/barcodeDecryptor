//
//  MainWindows.cs
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
using Gdk;



/// <summary>
/// Main window.
/// </summary>
public partial class MainWindow: Gtk.Window
{

	/// <summary>
	/// Initializes a new instance of the <see cref="MainWindow"/> class.
	/// </summary>
	public MainWindow()
		: base(Gtk.WindowType.Toplevel)
	{
		//label1.SetSizeRequest(500, 300);

		Build();
	}
	/// <summary>
	/// Raises the delete event event.
	/// </summary>
	/// <param name="sender">Sender.</param>
	/// <param name="a">The alpha component.</param>
	protected void OnDeleteEvent(object sender, DeleteEventArgs a)
	{
		Application.Quit();
		a.RetVal = true;
	}
	/// <summary>
	/// Raises the cmd decrypt entered event.
	/// </summary>
	/// <param name="sender">Sender.</param>
	/// <param name="e">E.</param>
	protected void OnCmdDecryptEntered(object sender, EventArgs e)
	{
		try
		{
            genericBarcode test = genericBarcode.getBarcode(txtInput.Text);
			//EAN13 test = new EAN13();
			barcodeOutput output = new barcodeOutput();

            Pango.FontDescription fontdesc = Pango.FontDescription.FromString(test.getFontDesc());

			label3.ModifyFont(fontdesc);

			//label3.Text = 

			txtOutput.Buffer.Text = test.Encrypt();
			//txtOutput.Buffer.Text = test.Encrypt(txtInput.Text);

            image1.Pixbuf = output.DrawLinearBarcode(txtOutput.Buffer.Text,txtInput.Text,(int) test.getBarcodeID(),test.getBarcodeValueIndex(),0);

		} catch (Exception ex)
		{
			errorHandling(ex.Message.ToString());
		}


	}

    protected void OnCmdPdfEntered(object sender, EventArgs e)
    {
        try
        {
            // (8,5 x 11) x 72dpi = (612 x 792)
            PdfSurface pdfSurface = new PdfSurface("test.pdf", (8.5*300), (11*300));

            pdfSurface.SetFallbackResolution(300.0, 300.0);

            Cairo.Context cc = new Cairo.Context(pdfSurface);

            Cairo.Matrix m = cc.Matrix;
            m.Scale(1,1);
            cc.Matrix = m;
                
            cc.Rectangle(new PointD(150,150),2000,1500);
           
            //Gdk.CairoHelper.SetSourcePixbuf(cc, pixbuf.ScaleSimple(300, 300, InterpType.Hyper), 300, 300);
            Gdk.CairoHelper.SetSourcePixbuf(cc, image1.Pixbuf.ScaleSimple(300, 300, InterpType.Hyper), 300, 300);

            cc.Fill();

            pdfSurface.Finish();


        } catch (Exception ex)
        {
            errorHandling(ex.Message.ToString());
        }


    }




	/// <summary>
	/// Errors the handling.
	/// </summary>
	/// <param name="message">Message.</param>
	private void errorHandling(String message)
	{

		MessageDialog md = new MessageDialog (null, DialogFlags.Modal, MessageType.Error, ButtonsType.Ok, message);
		md.Run ();
		md.Destroy();
	}

	/// <summary>
	/// Raises the click event.
	/// </summary>
	/// <param name="sender">Sender.</param>
	/// <param name="e">E.</param>
	protected void OnClick (object sender, EventArgs e)
	{
		Application.Quit ();
	}

	/// <summary>
	/// Ons the shown.
	/// </summary>
	/// <param name="sender">Sender.</param>
	/// <param name="e">E.</param>
	protected void onShown(object sender, EventArgs e)
	{
		throw new NotImplementedException("Not implemented yet !!");
	}

    protected void onClick (object sender, EventArgs e)
    {
        throw new NotImplementedException ();
    }
}