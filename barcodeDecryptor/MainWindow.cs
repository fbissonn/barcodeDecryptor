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




public partial class MainWindow: Gtk.Window
{


  public MainWindow()
    : base(Gtk.WindowType.Toplevel)
  {
    //label1.SetSizeRequest(500, 300);

    Build();
  }

  protected void OnDeleteEvent(object sender, DeleteEventArgs a)
  {
    Application.Quit();
    a.RetVal = true;
  }

  protected void OnCmdDecryptEntered(object sender, EventArgs e)
  {
    try
      {
          
        EAN13 test = new EAN13();
        barcodeOutput output = new barcodeOutput();

        Pango.FontDescription fontdesc = Pango.FontDescription.FromString("EAN-13 66");

        label3.ModifyFont(fontdesc);

        //label3.Text = 

        txtOutput.Buffer.Text = test.Encrypt(txtInput.Text);

          image1.Pixbuf = output.DrawLinearBarcode(txtOutput.Buffer.Text,txtInput.Text,(int) barcodeOutput.barcodeEnum.BC_EAN_13,test.barcodeValueIndex);
        
      } catch (Exception ex)
      {
        errorHandling(ex.Message.ToString());
      }


  }



 
  
	private void errorHandling(String message)
	{

		MessageDialog md = new MessageDialog (null, DialogFlags.Modal, MessageType.Error, ButtonsType.Ok, message);
		md.Run ();
		md.Destroy();
	}


	protected void OnClick (object sender, EventArgs e)
	{
		Application.Quit ();
	}

	protected void onShown(object sender, EventArgs e)
	{

	}
}
