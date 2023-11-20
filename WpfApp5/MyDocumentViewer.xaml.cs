using System;
using System.Collections.Generic;
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
            MessageBox.Show("Open");
        }
        private void Save_Excuted(object sender, ExecutedRoutedEventArgs e)
        {
            MessageBox.Show("Save");
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
