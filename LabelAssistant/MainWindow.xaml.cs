using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace LabelAssistant
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        string whsePrefix;
        ObservableCollection<Label> outputBuffer = new ObservableCollection<Label>();
        string labelPath = System.IO.Path.Combine(Directory.GetCurrentDirectory(), "locationlabels.txt");
        string barcodePath = System.IO.Path.Combine(Directory.GetCurrentDirectory(), "locationbarcodes.txt");
        string configPath = System.IO.Path.Combine(Directory.GetCurrentDirectory(), "config.txt");
        string waspPath;
        public MainWindow()
        {
            //Initializes the application.
            InitializeComponent();
            StartUp();
        }
        /// <summary>
        /// Event handlers for MainWindow.xaml
        /// </summary>

        private void MainSelect_Checked(object sender, RoutedEventArgs e)
        {
            whsePrefix = "20";
        }

        private void CageSelect_Checked(object sender, RoutedEventArgs e)
        {
            whsePrefix = "10";
        }

        private void BkWhseSelect_Checked(object sender, RoutedEventArgs e)
        {
            whsePrefix = "60";
        }

        private void VialSelect_Checked(object sender, RoutedEventArgs e)
        {
            whsePrefix = "70";
        }

        private void AddLabel_Click(object sender, RoutedEventArgs e)
        {
            if (Range_Select.IsChecked == false)
            {
                SingleAdd();
            }
            else
            {
                RangeAdd();
            }
            UpdateForm();
        }

        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void PrintButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                PrintOutputBuffer();
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show(ex.Message + Environment.NewLine + "Place WASP shortcut path in config.txt.");
                this.Close();
            }
            CountLabels();
            outputBuffer.Clear();
        }

        private void AisleNumber_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (AisleNumber.GetLineLength(AisleNumber.GetLastVisibleLineIndex()) == 2)
            {
                Keyboard.Focus(SectionName);
            }
        }

        private void SectionName_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (SectionName.GetLineLength(SectionName.GetLastVisibleLineIndex()) == 2)
            {
                Keyboard.Focus(ShelfNumber);
            }
        }

        private void ShelfNumber_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (ShelfNumber.GetLineLength(ShelfNumber.GetLastVisibleLineIndex()) == 2)
            {
                Keyboard.Focus(PositionNumber);
            }
        }

        private void PositionNumber_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (PositionNumber.GetLineLength(PositionNumber.GetLastVisibleLineIndex()) == 2)
            {
                if (Range_Select.IsChecked == true)
                {
                    Keyboard.Focus(LocationMax);
                }
                else Keyboard.Focus(AddLabel);
            }
        }

        private void LocationMax_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (LocationMax.GetLineLength(LocationMax.GetLastVisibleLineIndex()) == 2)
            {
                Keyboard.Focus(AddLabel);
            }
        }

        private void Range_Select_Checked(object sender, RoutedEventArgs e)
        {
            LocationMaxText.Visibility = Visibility.Visible;
            LocationMax.Visibility = Visibility.Visible;
            PositionLabel.Visibility = Visibility.Hidden;
            Keyboard.Focus(AisleNumber);
        }

        private void Range_Select_Unchecked(object sender, RoutedEventArgs e)
        {
            LocationMaxText.Visibility = Visibility.Hidden;
            LocationMax.Visibility = Visibility.Hidden;
            PositionLabel.Visibility = Visibility.Visible;
            Keyboard.Focus(AisleNumber);
        }

        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            Keyboard.Focus(AisleNumber);
        }
        
        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            int index;
            var item = (sender as FrameworkElement).DataContext;
            index = OutputViewer.Items.IndexOf(item);
            outputBuffer.RemoveAt(index);
        }

        /// <summary>
        /// Other methods to be used by MainWindow()
        /// </summary>

        private void StartUp()
        {
            File.Delete(labelPath);
            File.Delete(barcodePath);
            if (LoadConfig() == "")
            {
                System.Windows.MessageBox.Show("Place WASP shortcut path in config.txt.");
            }
            else waspPath = LoadConfig();
            OutputViewer.ItemsSource = outputBuffer;
        }

        string LoadConfig()
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

        private void SingleAdd()
        {
            Label temp = new Label();
            temp.BarCode = whsePrefix + AisleNumber.Text + SectionName.Text + ShelfNumber.Text + PositionNumber.Text;
            temp.LabelText = AisleNumber.Text + "-" + SectionName.Text + "-" + ShelfNumber.Text + "-" + PositionNumber.Text;
            temp.FullName = AisleNumber.Text + "-" + SectionName.Text + "-" + ShelfNumber.Text + "-" + PositionNumber.Text + "    -    " + whsePrefix + AisleNumber.Text + SectionName.Text + ShelfNumber.Text + PositionNumber.Text;
            outputBuffer.Add(temp);
        }

        private void RangeAdd()
        {
            int rangeCounter = 0;
            int rangeMax = 0;
            try
            {
                rangeCounter = Convert.ToInt32(PositionNumber.Text);
                rangeMax = Convert.ToInt32(LocationMax.Text);
            }
            catch (Exception)
            {

                MessageBox.Show("Must be a numerical value!");
                UpdateForm();
            }
            while (rangeCounter <= rangeMax)
            {
                Label temp = new Label();
                if (rangeCounter < 10)
                {
                    temp.BarCode = $"{whsePrefix}{AisleNumber.Text}{SectionName.Text}{ShelfNumber.Text}0{rangeCounter}";
                    temp.LabelText = $"{AisleNumber.Text}-{SectionName.Text}-{ShelfNumber.Text}-0{rangeCounter}";
                    temp.FullName = $"{AisleNumber.Text}-{SectionName.Text}-{ShelfNumber.Text}-0{rangeCounter}    -    {whsePrefix}{AisleNumber.Text}{SectionName.Text}{ShelfNumber.Text}0{rangeCounter}";
                }
                else if (rangeCounter >= 10)
                {
                    temp.BarCode = $"{whsePrefix}{AisleNumber.Text}{SectionName.Text}{ShelfNumber.Text}{rangeCounter}";
                    temp.LabelText = $"{AisleNumber.Text}-{SectionName.Text}-{ShelfNumber.Text}-{rangeCounter}";
                    temp.FullName = $"{AisleNumber.Text}-{SectionName.Text}-{ShelfNumber.Text}-{rangeCounter}    -    {whsePrefix}{AisleNumber.Text}{SectionName.Text}{ShelfNumber.Text}{rangeCounter}";
                }
                outputBuffer.Add(temp);
                rangeCounter++;

            }
        }

        private void UpdateForm()
        {
            AisleNumber.Clear();
            SectionName.Clear();
            PositionNumber.Clear();
            ShelfNumber.Clear();
            LocationMax.Clear();
            Keyboard.Focus(AisleNumber);
            OutputViewer.Items.Refresh();
            //LabelTextBlock.Text = "";
            foreach (Label label in outputBuffer)
            {
                // LabelTextBlock.Text += label.FullName + Environment.NewLine;
            }
        }

        void PrintOutputBuffer()
        {
            foreach (Label label in outputBuffer)
            {
                File.AppendAllText(labelPath, label.LabelText + Environment.NewLine);
                File.AppendAllText(barcodePath, label.BarCode + Environment.NewLine);
                //Process.Start(ldConfig());
            }
        }

        private void CountLabels()
        {
            int numberOfLabels;
            numberOfLabels = outputBuffer.Count;
            MessageBox.Show($"{ numberOfLabels } labels to print.");
        }
    }
}
