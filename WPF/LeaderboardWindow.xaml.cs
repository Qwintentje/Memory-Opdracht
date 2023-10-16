
namespace WPF;
public partial class LeaderboardWindow : Window
{
    private List<GameDbModel> leaderboard;
    public LeaderboardWindow()
    {
        InitializeComponent();
        Database db = new Database();
        leaderboard = db.GetHighscores();
        DataContext = leaderboard;
    }

    private void BackButton_Click(object sender, RoutedEventArgs e)
    {
        MainWindow mainWindow = new MainWindow();
        mainWindow.Show();
        Close();
    }
}