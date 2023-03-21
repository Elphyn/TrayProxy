using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsFormsApp1.Properties;
using Microsoft.Win32;

namespace WindowsFormsApp1
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MyCustomApplicationContext());
        }

    }
    public class MyCustomApplicationContext : ApplicationContext
    {
        private NotifyIcon trayIcon;

        public MyCustomApplicationContext()
        {
            // Initialize Tray Icon
            trayIcon = new NotifyIcon()
            {
                Icon = Resources.NotifyIcon,
                ContextMenu = new ContextMenu(new MenuItem[] {
                new MenuItem("Proxy On", On),
                new MenuItem("Proxy Off", Off),
                new MenuItem("", Check),
                new MenuItem("Change to USA", ChangeProxyUSA ),
                new MenuItem("Change to KZ", ChangeProxyKz),
                new MenuItem("Exit", Exit)
            }),
                Visible = true
            };
        }
        void On(object sender, EventArgs e)
        {
            const string userRoot = "HKEY_CURRENT_USER";
            const string subKey = "Software\\Microsoft\\Windows\\CurrentVersion\\Internet Settings";
            const string keyName = userRoot + "\\" + subKey;

            Registry.SetValue(keyName, "ProxyEnable", 1);
        }
        void Off(object sender, EventArgs e)
        {
            const string userRoot = "HKEY_CURRENT_USER";
            const string subKey = "Software\\Microsoft\\Windows\\CurrentVersion\\Internet Settings";
            const string keyName = userRoot + "\\" + subKey;

            Registry.SetValue(keyName, "ProxyEnable", 0);
        }

        void Exit(object sender, EventArgs e)
        {
            // Hide tray icon, otherwise it will remain shown until user mouses over it
            trayIcon.Visible = false;

            Application.Exit();
        }
        void Check(object sender, EventArgs e)
        {
            const string userRoot = "HKEY_CURRENT_USER";
            const string subKey = "Software\\Microsoft\\Windows\\CurrentVersion\\Internet Settings";
            const string keyName = userRoot + "\\" + subKey;


        }
        void ChangeProxyUSA(object sender, EventArgs e)
        {
            const string ipAddress = "154.13.35.170";
            const string portNumber = "63556";
            // Get the path to the Internet Settings registry key
            const string registryKeyPath = @"HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Internet Settings";

            // Set the proxy server IP and port number values
            Registry.SetValue(registryKeyPath, "ProxyServer", ipAddress + ":" + portNumber, RegistryValueKind.String);

            // Enable proxy settings
            Registry.SetValue(registryKeyPath, "ProxyEnable", 1, RegistryValueKind.DWord);
        }
        void ChangeProxyKz(object sender, EventArgs e)
        {
            const string ipAddress = "193.107.22.60";
            const string portNumber = "56799";
            // Get the path to the Internet Settings registry key
            const string registryKeyPath = @"HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Internet Settings";

            // Set the proxy server IP and port number values
            Registry.SetValue(registryKeyPath, "ProxyServer", ipAddress + ":" + portNumber, RegistryValueKind.String);

            // Enable proxy settings
            Registry.SetValue(registryKeyPath, "ProxyEnable", 1, RegistryValueKind.DWord);
        }
    }
}
