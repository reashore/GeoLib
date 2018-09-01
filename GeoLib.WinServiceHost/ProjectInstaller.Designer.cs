using System.ServiceProcess;
using System.ComponentModel;
using System.Configuration.Install;

namespace GeoLib.WinServiceHost
{
    partial class ProjectInstaller
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

        #region Component Designer generated code

        private void InitializeComponent()
        {
            this.serviceProcessInstaller1 = new ServiceProcessInstaller();
            this.serviceInstaller1 = new ServiceInstaller();
            // 
            // serviceProcessInstaller1
            // 
            this.serviceProcessInstaller1.Password = null;
            this.serviceProcessInstaller1.Username = null;
            // 
            // serviceInstaller1
            // 
            this.serviceInstaller1.ServiceName = "GeoLib Windows Service Host";
            // 
            // ProjectInstaller
            // 
            this.Installers.AddRange(new Installer[] {
            this.serviceProcessInstaller1,
            this.serviceInstaller1});

        }

        #endregion

        private ServiceProcessInstaller serviceProcessInstaller1;
        private ServiceInstaller serviceInstaller1;
    }
}