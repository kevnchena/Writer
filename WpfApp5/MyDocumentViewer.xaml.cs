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
        public MyDocumentViewer()
        {
            InitializeComponent();
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
            object property = rtbEditor.Selection.GetPropertyValue(Inline.FontWeightProperty);
            object styleProperty = rtbEditor.Selection.GetPropertyValue(Inline.FontStyleProperty);

            boldButton.IsChecked = (property != DependencyProperty.UnsetValue) && property.Equals(FontWeights.Bold);//如果屬性沒有設值和是否粗體
            italicButton.IsChecked = (styleProperty != DependencyProperty.UnsetValue) && styleProperty.Equals(FontStyles.Italic);
         
        }
    }
}
