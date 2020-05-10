using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
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

namespace LabelAssistant
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        int whseTag = 20;
        List<LabelData> outputBuffer = new List<LabelData>();
        string labelPath = System.IO.Path.Combine(Directory.GetCurrentDirectory(), "locationlabels.txt");
        string barcodePath = System.IO.Path.Combine(Directory.GetCurrentDirectory(), "locationbarcodes.txt");
        //string waspPath;
        public MainWindow()
        {
            InitializeComponent();
            File.Create(labelPath);
            File.Create(barcodePath);
        }

        private void mainSelect_Checked(object sender, RoutedEventArgs e)
        {
            whseTag = 20;
        }

        private void cageSelect_Checked(object sender, RoutedEventArgs e)
        {
            whseTag = 10;
        }

        private void addLabel_Click(object sender, RoutedEventArgs e)
        {
            LabelData temp = new LabelData();
            temp.SetAisle(aisleNumber.Text);
            temp.SetSection(sectionName.Text);
            temp.SetShelf(shelfNumber.Text);
            temp.SetLocation(positionNumber.Text);
            temp.SetWhse(whseTag);
            outputBuffer.Add(temp);
        }

        private void exitButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();

        }

        private void printButton_Click(object sender, RoutedEventArgs e)
        {
     
                foreach (LabelData label in outputBuffer)
                {
                    File.AppendAllText(labelPath, label.GetLabelText() + Environment.NewLine);
                File.AppendAllText(barcodePath, label.GetBarCode() + Environment.NewLine);
                //Process.Start(waspPath);
            }
            }
        
    }
}
