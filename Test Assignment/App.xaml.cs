using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Test_Assignment
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void Button_Click(object sender, RoutedEventArgs e)
        {



            var target = (FrameworkElement)sender;
            while (target is TabItem == false)
                target = (FrameworkElement)target.Parent;
            (target.Parent as TabControl).Items.Remove(target);
        }
    }
}
