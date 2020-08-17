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
        string whsePrefix;
        List<Label> outputBuffer = new List<Label>();
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
            whsePrefix = "20";
        }

        private void cageSelect_Checked(object sender, RoutedEventArgs e)
        {
            whsePrefix = "10";
        }

        private void bkWhseSelect_Checked(object sender, RoutedEventArgs e)
        {
            whsePrefix = "60";
        }

        private void addLabel_Click(object sender, RoutedEventArgs e)
        {
            Label temp = new Label();
            temp.BarCode = whsePrefix + aisleNumber.Text + sectionName.Text + shelfNumber.Text + positionNumber.Text;
            temp.LabelText = aisleNumber.Text + "-" + sectionName.Text + "-" + shelfNumber.Text + "-" + positionNumber.Text;
            temp.FullName = aisleNumber.Text + "-" + sectionName.Text + "-" + shelfNumber.Text + "-" + positionNumber.Text + "    -    " + whsePrefix + aisleNumber.Text + sectionName.Text + shelfNumber.Text + positionNumber.Text;
            outputBuffer.Add(temp);
            aisleNumber.Clear();
            sectionName.Clear();
            positionNumber.Clear();
            shelfNumber.Clear();
            Keyboard.Focus(aisleNumber);
            labelTextBlock.Text = "";
            foreach (Label label in outputBuffer)
            {
                labelTextBlock.Text += label.FullName + Environment.NewLine;
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
            foreach (Label label in outputBuffer)
            {
                File.AppendAllText(labelPath, label.LabelText + Environment.NewLine);
                File.AppendAllText(barcodePath, label.BarCode + Environment.NewLine);
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
