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
        string configPath = System.IO.Path.Combine(Directory.GetCurrentDirectory(), "config.txt");
        string waspPath;
        public MainWindow()
        {
            InitializeComponent();
            File.Delete(labelPath);
            File.Delete(barcodePath);
            if (ldConfig() == "")
            {
                System.Windows.MessageBox.Show("Place WASP shortcut path in config.txt.");
            }
            else waspPath = ldConfig();
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
            aisleNumber.Clear();
            sectionName.Clear();
            positionNumber.Clear();
            shelfNumber.Clear();
            Keyboard.Focus(aisleNumber);
            labelTextBlock.Text = "";
            foreach (LabelData label in outputBuffer)
            {
                labelTextBlock.Text += label.GetLabelText() + Environment.NewLine;
            }
        }

        private void exitButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void printButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                printOutputBuffer();
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show(ex.Message + Environment.NewLine + "Place WASP shortcut path in config.txt.");
                this.Close();
            }
            outputBuffer.Clear();
        }

        void printOutputBuffer()
        {
            foreach (LabelData label in outputBuffer)
            {
                File.AppendAllText(labelPath, label.GetLabelText() + Environment.NewLine);
                File.AppendAllText(barcodePath, label.GetBarCode() + Environment.NewLine);
                //Process.Start(ldConfig());
            }
        }
        private void aisleNumber_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (aisleNumber.GetLineLength(aisleNumber.GetLastVisibleLineIndex()) == 2)
            {
                Keyboard.Focus(sectionName);
            }
        }

        private void sectionName_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (sectionName.GetLineLength(sectionName.GetLastVisibleLineIndex()) == 2)
            {
                Keyboard.Focus(shelfNumber);
            }
        }

        private void shelfNumber_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (shelfNumber.GetLineLength(shelfNumber.GetLastVisibleLineIndex()) == 2)
            {
                Keyboard.Focus(positionNumber);
            }
        }

        private void positionNumber_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (positionNumber.GetLineLength(positionNumber.GetLastVisibleLineIndex()) == 2)
            {
                Keyboard.Focus(addLabel);
            }
        }

        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            Keyboard.Focus(aisleNumber);
        }

        string ldConfig()
        {
            if (File.Exists(configPath) == true && configPath != null)
            {
                return (File.ReadAllText(configPath));
            }
            else if (File.Exists(configPath) == false)
            {
                System.Windows.MessageBox.Show("Config.txt generated.");
                File.AppendAllText(configPath, "");
                return (""); 
            }
            else
            {
                return ("");
            }
        }
    }
}
