namespace WPF;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
    }
    private void PlayButton_Click(object sender, RoutedEventArgs e)
    {

    }


    private void LeaderboardButton_Click(object sender, RoutedEventArgs e)
    {
        LeaderboardWindow leaderboardWindow = new LeaderboardWindow();
        leaderboardWindow.Show();
        Close();
    }
    private void QuitButton_Click(object sender, RoutedEventArgs e) => Close();

}
