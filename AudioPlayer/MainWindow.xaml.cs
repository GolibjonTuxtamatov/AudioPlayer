using System.ComponentModel;
using System.Data;
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
using System.Windows.Threading;
using Microsoft.Win32;

namespace AudioPlayer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private IList<string> musicFullPaths;
        public event PropertyChangedEventHandler PropertyChanged;


        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;
            mediaPlayer.Play();
            musicFullPaths = new List<string>();
            musicVolume.Minimum = 0;
            musicVolume.Maximum = 100;
        }

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private double musicDuration;

        public double MusicDuration
        {
            get { return musicDuration; }
            set {
                if (musicDuration != value)
                {
                    musicDuration = value;
                    OnPropertyChanged(nameof(MusicDuration));
                    mediaPlayer.Position = TimeSpan.FromSeconds(value);
                }
            }
        }


        private void btnMiniMized_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
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
            if(musics.Items.Count == 0)
            {
                var openFileDialog = new OpenFileDialog();
                openFileDialog.Filter = "mp3 | *mp3";
                openFileDialog.Multiselect = true;
                openFileDialog.ShowDialog();
                musicFullPaths = openFileDialog.FileNames;
                var musicPaths = openFileDialog.SafeFileNames;

                foreach (var musicPath in musicPaths)
                {
                    musics.Items.Add(musicPath);
                }
            }
            else if(musics.Items.Count > 0 && musics.SelectedItem == null)
            {
                musicName.Text = musics.Items[0].ToString();
                mediaPlayer.Source = new Uri(musicFullPaths[0]);
                musics.SelectedItem = musics.Items[0];
            }
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
                musicName.Text = musics.SelectedItem.ToString();

                mediaPlayer.Source = new Uri(musicFullPaths[musics.SelectedIndex-1]);
                musics.SelectedItem = musics.Items[musics.SelectedIndex - 1];
            }
            else if(musics.SelectedIndex == 0)
            {
                musicName.Text = musics.SelectedItem.ToString();

                mediaPlayer.Source = new Uri(musicFullPaths[musics.SelectedIndex]);
            }
            else
            {

            }
        }

        private void btnNext_Click(object sender, RoutedEventArgs e)
        {
            if (musics.Items.Count > 0 && musics.SelectedIndex >= 0 && musics.SelectedIndex < musics.Items.Count - 1)
            {
                musicName.Text = musics.SelectedItem.ToString();

                mediaPlayer.Source = new Uri(musicFullPaths[musics.SelectedIndex+1]);
                musics.SelectedItem = musics.Items[musics.SelectedIndex + 1];
            }
            else if (musics.SelectedIndex == 0)
            {
                musicName.Text = musics.SelectedItem.ToString();

                mediaPlayer.Source = new Uri(musicFullPaths[musics.SelectedIndex]);
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
            musicFullPaths = openFileDialog.FileNames;
            var musicPaths = openFileDialog.SafeFileNames;

            foreach (var musicPath in musicPaths)
            {
                musics.Items.Add(musicPath);
            }
        }

        private void musics_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            musicName.Text = musics.SelectedItem.ToString();
            mediaPlayer.Source = new Uri(musicFullPaths[musics.SelectedIndex]);
        }

        private void mainGrid_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        private void mediaPlayer_MediaOpened(object sender, RoutedEventArgs e)
        {
            musicLength.Maximum = mediaPlayer.NaturalDuration.TimeSpan.TotalSeconds;
            var timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += (timerSender, timerArgs) =>
            {
                musicLength.Value = mediaPlayer.Position.TotalSeconds;
            };
            timer.Start();
        }

        private void musicVolume_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            mediaPlayer.Volume = musicVolume.Value/100;
        }

        private void mediaPlayer_MediaEnded(object sender, RoutedEventArgs e)
        {
            if (musics.Items.Count > 0 && musics.SelectedIndex >= 0 && musics.SelectedIndex < musics.Items.Count - 1)
            {
                musicName.Text = musics.SelectedItem.ToString();

                mediaPlayer.Source = new Uri(musicFullPaths[musics.SelectedIndex + 1]);
                musics.SelectedItem = musics.Items[musics.SelectedIndex + 1];
            }
            else if (musics.SelectedIndex == 0)
            {
                musicName.Text = musics.SelectedItem.ToString();

                mediaPlayer.Source = new Uri(musicFullPaths[musics.SelectedIndex]);
            }
            else
            {

            }
        }
    }
}