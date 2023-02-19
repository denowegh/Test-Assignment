using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Net;
using System.Reflection.PortableExecutable;
using System.IO;
using Newtonsoft.Json;
using Test_Assignment.Model;
using System.Data;
using Test_Assignment.View_Model;

namespace Test_Assignment
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public MainWindow()
        {
            InitializeComponent();
            
        }

        

        private void Delete(object sender, RoutedEventArgs e)
        {
            var target = (FrameworkElement)sender;
            while (target is TabItem==false)
                target = (FrameworkElement)target.Parent;
            Tabs.Items.Remove(target);
        }

        private void Find_Coin_Btn(object sender, RoutedEventArgs e)
        {
            var btn = new Button();
            btn.Content = "Del";
            btn.Click+= new RoutedEventHandler(Delete);
            Binding binding = new Binding("MainViewModel.TableViewModel");
           
            binding.Source = DataContext;
            TabItem newTabItem = new TabItem
            {
                Header = "Test",
                Name = "Test",
                

            };
            newTabItem.SetBinding(TabItem.ContentProperty,binding);
            _ = Tabs.Items.Add(newTabItem);
        }



        
    }
}
