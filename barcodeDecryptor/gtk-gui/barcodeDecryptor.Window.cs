
// This file has been generated by the GUI designer. Do not modify.
namespace barcodeDecryptor
{
	public partial class Window
	{
		private global::Gtk.Fixed fixed1;
		
		private global::Gtk.Button button1;
		
		private global::Gtk.Label d;
		
		private global::Gtk.Entry entry1;
		
		private global::Gtk.Label label3;
		
		private global::Gtk.ScrolledWindow GtkScrolledWindow;
		
		private global::Gtk.TextView textview2;
		
		private global::Gtk.Image image1;

		protected virtual void Build ()
		{
			global::Stetic.Gui.Initialize (this);
			// Widget barcodeDecryptor.Window
			this.Name = "barcodeDecryptor.Window";
			this.Title = global::Mono.Unix.Catalog.GetString ("Window");
			this.WindowPosition = ((global::Gtk.WindowPosition)(4));
			// Container child barcodeDecryptor.Window.Gtk.Container+ContainerChild
			this.fixed1 = new global::Gtk.Fixed ();
			this.fixed1.Name = "fixed1";
			this.fixed1.HasWindow = false;
			// Container child fixed1.Gtk.Fixed+FixedChild
			this.button1 = new global::Gtk.Button ();
			this.button1.CanFocus = true;
			this.button1.Name = "button1";
			this.button1.UseUnderline = true;
			this.button1.Label = global::Mono.Unix.Catalog.GetString ("GtkButton");
			this.fixed1.Add (this.button1);
			global::Gtk.Fixed.FixedChild w1 = ((global::Gtk.Fixed.FixedChild)(this.fixed1 [this.button1]));
			w1.X = 172;
			w1.Y = 309;
			// Container child fixed1.Gtk.Fixed+FixedChild
			this.d = new global::Gtk.Label ();
			this.d.Name = "d";
			this.d.LabelProp = global::Mono.Unix.Catalog.GetString ("Barcode");
			this.fixed1.Add (this.d);
			global::Gtk.Fixed.FixedChild w2 = ((global::Gtk.Fixed.FixedChild)(this.fixed1 [this.d]));
			w2.X = 74;
			w2.Y = 37;
			// Container child fixed1.Gtk.Fixed+FixedChild
			this.entry1 = new global::Gtk.Entry ();
			this.entry1.WidthRequest = 391;
			this.entry1.CanFocus = true;
			this.entry1.Name = "entry1";
			this.entry1.IsEditable = true;
			this.entry1.InvisibleChar = '●';
			this.fixed1.Add (this.entry1);
			global::Gtk.Fixed.FixedChild w3 = ((global::Gtk.Fixed.FixedChild)(this.fixed1 [this.entry1]));
			w3.X = 138;
			w3.Y = 32;
			// Container child fixed1.Gtk.Fixed+FixedChild
			this.label3 = new global::Gtk.Label ();
			this.label3.WidthRequest = 42;
			this.label3.HeightRequest = 0;
			this.label3.Name = "label3";
			this.label3.LabelProp = global::Mono.Unix.Catalog.GetString ("Value");
			this.fixed1.Add (this.label3);
			global::Gtk.Fixed.FixedChild w4 = ((global::Gtk.Fixed.FixedChild)(this.fixed1 [this.label3]));
			w4.X = 82;
			w4.Y = 68;
			// Container child fixed1.Gtk.Fixed+FixedChild
			this.GtkScrolledWindow = new global::Gtk.ScrolledWindow ();
			this.GtkScrolledWindow.Name = "GtkScrolledWindow";
			this.GtkScrolledWindow.ShadowType = ((global::Gtk.ShadowType)(1));
			// Container child GtkScrolledWindow.Gtk.Container+ContainerChild
			this.textview2 = new global::Gtk.TextView ();
			this.textview2.WidthRequest = 368;
			this.textview2.HeightRequest = 142;
			this.textview2.CanFocus = true;
			this.textview2.Name = "textview2";
			this.GtkScrolledWindow.Add (this.textview2);
			this.fixed1.Add (this.GtkScrolledWindow);
			global::Gtk.Fixed.FixedChild w6 = ((global::Gtk.Fixed.FixedChild)(this.fixed1 [this.GtkScrolledWindow]));
			w6.X = 141;
			w6.Y = 66;
			// Container child fixed1.Gtk.Fixed+FixedChild
			this.image1 = new global::Gtk.Image ();
			this.image1.WidthRequest = 194;
			this.image1.HeightRequest = 149;
			this.image1.Name = "image1";
			this.fixed1.Add (this.image1);
			global::Gtk.Fixed.FixedChild w7 = ((global::Gtk.Fixed.FixedChild)(this.fixed1 [this.image1]));
			w7.X = 381;
			w7.Y = 285;
			this.Add (this.fixed1);
			if ((this.Child != null)) {
				this.Child.ShowAll ();
			}
			this.DefaultWidth = 643;
			this.DefaultHeight = 481;
			this.Show ();
		}
	}
}