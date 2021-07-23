using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfApp9.MyControl
{
    /// <summary>
    /// Interaction logic for StarRate.xaml
    /// </summary>
    public partial class StarRate : UserControl
    {
        public StarRate()
        {
            InitializeComponent();
            stars = new List<ToggleButton>
            {
                Star1, Star2, Star3, Star4, Star5
            };
            for (int i = 0; i < StarRating; i++)
            {
                stars[i].IsChecked = true;
            }
        }

        public static readonly DependencyProperty StarDependency = DependencyProperty.Register("StarRating", typeof(int), typeof(StarRate));
        public int StarRating
        {
            get { return (int)GetValue(StarDependency); }
            set { SetValue(StarDependency, value); }
        }

        private List<ToggleButton> stars;

        private void StarRate_OnLoaded(object sender, RoutedEventArgs e)
        {
            for (int i = 0; i < StarRating; i++)
            {
                stars[i].IsChecked = true;
                stars[i].Background = Brushes.Gold;
            }
        }

        private void Star1_Click(object sender, RoutedEventArgs e)
        {
            ToggleButton selectedStar = sender as ToggleButton;
            int index = Int32.Parse(selectedStar.Name.Substring(4));
            StarRating = index;
            for (int i = 0; i < index; i++)
            {
                stars[i].IsChecked = true;
            }
            for (int i = index; i < stars.Count; i++)
            {
                stars[i].IsChecked = false;
            }
        }
    }
}
