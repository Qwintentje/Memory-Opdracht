namespace WPF;

public partial class LobbyWindow : Window
{
    private static readonly LinearGradientBrush green = new(Color.FromRgb(233, 255, 233), Color.FromRgb(199, 255, 159), 0);
    private static readonly LinearGradientBrush red = new(Color.FromRgb(255, 191, 195), Color.FromRgb(255, 0, 0), 0);
    private List<string> uploadedImages = new List<string>();

    public LobbyWindow()
    {
        InitializeComponent();
        FileService.ClearIconsDirectory();
    }

    private void BackButton_Click(object sender, RoutedEventArgs e)
    {
        MainWindow mainWindow = new MainWindow();
        mainWindow.Show();
        Close();
    }

    private void PlayButton_Click(object sender, RoutedEventArgs e)
    {
        string nameInput = name.Text.Trim();
        string amountInput = cardAmount.Text.Trim();
        if (CheckName(nameInput) && CheckCardAmount(amountInput))
        {
            int.TryParse(amountInput, out int cardAmount);
            GameWindow gameWindow = new GameWindow(nameInput, cardAmount, uploadedImages);
            gameWindow.Show();
            Close();
        }
        else
        {
            MessageBox.Show("Naam moet minimaal 3 of meer tekens zijn.\nAantal moet minimaal 4 zijn en maximaal 15.", "Vul een geldige naam of aantal in");
        }
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
            if (cardAmount >= 4 && cardAmount <= 15)
            {
                return true;
            }
        }
        return false;
    }


    private void UploadImageButton_Click(object sender, RoutedEventArgs e)
    {
        OpenFileDialog openFileDialog = new OpenFileDialog
        {
            Title = "Select an Image",
            Filter = "Image files (*.png;*.jpeg;*.jpg)|*.png;*.jpeg;*.jpg|All files (*.*)|*.*"
        };

        if (openFileDialog.ShowDialog() == true)
        {
            string selectedImagePath = openFileDialog.FileName;
            string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
            string destinationDirectory = Path.Combine(baseDirectory, "assets", "uploadedimages");

            var succes = FileService.CopyImageToDestination(selectedImagePath, destinationDirectory, selectedImagePath);

            if (succes)
            {
                // Create a new Image control
                Image newImage = new Image
                {
                    Width = 100, // Set your desired width
                    Height = 100, // Set your desired height
                    Margin = new Thickness(5), // Set your desired margin
                    Source = new BitmapImage(new Uri(selectedImagePath)),
                };

                // Add the new Image control to the WrapPanel
                imageWrapPanel.Children.Add(newImage);

                uploadedImages.Add(selectedImagePath);
            }
        }
    }
}
