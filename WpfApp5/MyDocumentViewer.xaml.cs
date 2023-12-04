using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Xceed.Wpf.Toolkit;

namespace WpfApp5
{
    /// <summary>
    /// MyDocumentViewer.xaml 的互動邏輯
    /// </summary>
    public partial class MyDocumentViewer : Window
    {
        Color fontColor = Colors.Black;
        public MyDocumentViewer()
        {
            InitializeComponent();
            fontColorPicker.SelectedColor = fontColor;
            
            foreach (FontFamily fontfamily in Fonts.SystemFontFamilies)
            {
                fontFamilyComboBox.Items.Add(fontfamily.Source);
            }
            fontFamilyComboBox.SelectedIndex= 8;
            
            fontSizeComboBox.ItemsSource = new List<double>() { 8, 9, 10, 12, 14, 16, 18, 20, 24, 26, 28, 30, 34, 38, 40, 50, 60, 70, 80 };
            fontSizeComboBox.SelectedIndex = 3;
        }
        private void New_Excuted(object sender, ExecutedRoutedEventArgs e)
        {
            MyDocumentViewer myDocumentViewer = new MyDocumentViewer();
            myDocumentViewer.Show();
        }
        private void Open_Excuted(object sender, ExecutedRoutedEventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.Filter = "Rich Text Format檔案|*.rtf|所有檔案|*.*";
            if (fileDialog.ShowDialog() == true)
            {
                TextRange range = new TextRange(rtbEditor.Document.ContentStart, rtbEditor.Document.ContentEnd);

                using (FileStream fileStream = new FileStream(fileDialog.FileName, FileMode.Open))
                {
                    range.Load(fileStream, DataFormats.Rtf);
                }
            }
        }
        private void Save_Excuted(object sender, ExecutedRoutedEventArgs e)
        {

            SaveFileDialog saveFileDialog = new SaveFileDialog
            {
                Filter = "Rich Text Format (*.rtf)|*.rtf|Text Files (*.txt)|*.txt|html (*.html)|*.html",
                DefaultExt = "rtf",
                AddExtension = true
            };

            if (saveFileDialog.ShowDialog() == true)
            {
                string filePath = saveFileDialog.FileName;

                TextRange textRange = new TextRange(rtbEditor.Document.ContentStart, rtbEditor.Document.ContentEnd);

                using (FileStream fs = new FileStream(filePath, FileMode.Create))
                {
                    switch (saveFileDialog.FilterIndex)
                    {
                        case 1: // RTF Format
                            textRange.Save(fs, DataFormats.Rtf);
                            break;
                        case 2: // Text Format
                            textRange.Save(fs, DataFormats.Text);
                            break;
                        case 3: // Text Format
                            textRange.Save(fs, DataFormats.Html);
                            break;
                        default:
                            break;
                    }
                }
            }
        }
        private void clearButton_click(object sender, RoutedEventArgs e)
        {
            rtbEditor.Document.Blocks.Clear();
        }

        private void rtbEditor_SelectionChanged(object sender, RoutedEventArgs e)
        {
            if (!rtbEditor.Selection.IsEmpty)
            {
                object property = rtbEditor.Selection.GetPropertyValue(TextElement.FontWeightProperty);
                boldButton.IsChecked = (property != DependencyProperty.UnsetValue) && (property.Equals(FontWeights.Bold));

                property = rtbEditor.Selection.GetPropertyValue(TextElement.FontStyleProperty);
                italicButton.IsChecked = (property != DependencyProperty.UnsetValue) && (property.Equals(FontStyles.Italic));

                property = rtbEditor.Selection.GetPropertyValue(Inline.TextDecorationsProperty);
                underlineButton.IsChecked = (property != DependencyProperty.UnsetValue) && (property.Equals(TextDecorations.Underline));

                property = rtbEditor.Selection.GetPropertyValue(TextElement.FontFamilyProperty);
                if (property != DependencyProperty.UnsetValue && fontFamilyComboBox.Items.Contains(property.ToString()))
                {
                    fontFamilyComboBox.SelectedItem = property.ToString();
                }

                property = rtbEditor.Selection.GetPropertyValue(TextElement.FontSizeProperty);
                if (property != DependencyProperty.UnsetValue)
                {
                    double fontSize = (double)property;
                    fontSizeComboBox.SelectedItem = fontSize;
                }
            }
        }

        private void fontSizeComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (fontSizeComboBox.SelectedItem != null)
            {
                rtbEditor.Selection.ApplyPropertyValue(TextElement.FontSizeProperty, fontSizeComboBox.SelectedItem);
            }
        }

        private void fontFamilyComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (fontFamilyComboBox.SelectedItem != null)
            {
                rtbEditor.Selection.ApplyPropertyValue(TextElement.FontFamilyProperty, fontFamilyComboBox.SelectedItem);
            }
        }

        private void fontColorPicker_SelectedColorChanged(object sender, RoutedPropertyChangedEventArgs<Color?> e)
        {
            fontColor = (Color)e.NewValue;
            SolidColorBrush fontBrush = new SolidColorBrush(fontColor);
            rtbEditor.Selection.ApplyPropertyValue(TextElement.ForegroundProperty, fontBrush);
        }

    }
}
