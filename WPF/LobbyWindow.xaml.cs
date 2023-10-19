namespace WPF;

public partial class LobbyWindow : Window
{
    static readonly LinearGradientBrush green = new(Color.FromRgb(233, 255, 233), Color.FromRgb(199, 255, 159), 0);
    static readonly LinearGradientBrush red = new(Color.FromRgb(255, 191, 195), Color.FromRgb(255, 0, 0), 0);


    public LobbyWindow()
    {
        InitializeComponent();
    }

    private void BackButton_Click(object sender, RoutedEventArgs e)
    {
        MainWindow mainWindow = new MainWindow();
        mainWindow.Show();
        Close();
    }

    private void PlayButton_Click(object sender, RoutedEventArgs e)
    {

    }

    private void NameTextBox_TextChanged(object sender, TextChangedEventArgs e)
    {
        string inputText = name.Text.Trim();
        if (CheckName(inputText)) name.Background = green;
        else name.Background = red;
    }

    private void CardAmountTextBox_TextChanged(object sender, TextChangedEventArgs e)
    {
        string inputText = cardAmount.Text.Trim();
        if (CheckCardAmount(inputText)) cardAmount.Background = green;
        else cardAmount.Background = red;
    }

    private bool CheckName(string name)
    {
        if (name == null || name.Length < 3) return false;
        else return true;
    }

    private bool CheckCardAmount(string amount)
    {
        if (int.TryParse(amount, out int cardAmount))
        {
            if (cardAmount >= 4)
            {
                return true;
            }
        }
        return false;
    }
}
