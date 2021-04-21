using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace TextFileAnalyzer
{
    /// <summary>
    /// Interakční logika pro App.xaml
    /// </summary>
    public partial class App : Application
    {
        App() { 
            //System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("cs");
            System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("en"); 
        } 
    }
}
