using System.ComponentModel;

namespace GeoLib.WinServiceHost
{
    partial class GeoLibWindowsServiceHost
    {
        private IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }

            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            components = new Container();
            ServiceName = "GeoLib Windows Service Host";
        }
    }
}
