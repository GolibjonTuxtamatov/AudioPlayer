using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Win32;

namespace AudioPlayer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            mediaPlayer.Play();
        }


        private void btnMiniMized_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void btnPin_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void btnOne_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnSetting_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnThree_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnPlay_Click(object sender, RoutedEventArgs e)
        {
            mediaPlayer.Play();
        }

        private void btnPause_Click(object sender, RoutedEventArgs e)
        {
            mediaPlayer.Pause();
        }

        private void btnStop_Click(object sender, RoutedEventArgs e)
        {
            mediaPlayer.Stop();
        }

        private void btnPrev_Click(object sender, RoutedEventArgs e)
        {
            if(musics.Items.Count > 0 && musics.SelectedIndex > 0)
            {
                mediaPlayer.Source = new Uri(musics.Items[musics.SelectedIndex-1].ToString());
                musics.SelectedItem = musics.Items[musics.SelectedIndex - 1];
            }
            else if(musics.SelectedIndex == 0)
            {
                mediaPlayer.Source = new Uri(musics.SelectedItems[musics.SelectedIndex].ToString());
            }
            else
            {

            }
        }

        private void btnNext_Click(object sender, RoutedEventArgs e)
        {
            if (musics.Items.Count > 0 && musics.SelectedIndex >= 0 && musics.SelectedIndex < musics.Items.Count - 1)
            {
                mediaPlayer.Source = new Uri(musics.Items[musics.SelectedIndex + 1].ToString());
                musics.SelectedItem = musics.Items[musics.SelectedIndex + 1];
            }
            else if (musics.SelectedIndex == 0)
            {
                mediaPlayer.Source = new Uri(musics.SelectedItems[musics.SelectedIndex].ToString());
            }
            else
            {

            }
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            var openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "mp3 | *mp3";
            openFileDialog.Multiselect = true;
            openFileDialog.ShowDialog();
            musics.ItemsSource = openFileDialog.FileNames;
        }

        private async void musics_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            musicName.Text = musics.SelectedItem.ToString();
            mediaPlayer.Source = new Uri(musics.SelectedItem.ToString());
        }

        private void mainGrid_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

    }
}