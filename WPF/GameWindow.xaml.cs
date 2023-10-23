namespace WPF;

public partial class GameWindow : Window
{
    private Game? game { get; set; }
    private Image defaultCardImage = new Image
    {
        //PC
        //Source = new BitmapImage(new Uri("C:\\Users\\quint\\Documents\\Github\\Memory-Opdracht\\WPF\\assets\\Logo.png")),
        //LAPTOP
        Source = new BitmapImage(new Uri("C:\\Users\\quint\\Documents\\Github\\Memory Opdracht\\WPF\\assets\\Logo.png")),
        Stretch = Stretch.Uniform,
    };

    private Card? firstCard { get; set; }
    private Card? secondCard { get; set; }

    private Button? firstButton { get; set; }
    private Button? secondButton { get; set; }
    public GameWindow(string name, int cardAmount, List<string> uploadedImages)
    {
        InitializeComponent();
        GameService.Initialize(name, cardAmount, uploadedImages);
        game = GameService.Game; //Set the game property to the same memory reference as the GameService.Game
        DataContext = game;
        GenerateCards();
    }

    private void GenerateCards()
    {
        if (game?.Cards != null)
        {
            foreach (var card in game.Cards)
            {
                Button cardButton = new Button
                {
                    Width = 80,
                    Height = 120,
                    Margin = new Thickness(5),
                    Tag = card,
                    Background = Brushes.Transparent,
                    BorderBrush = Brushes.White
                };
                Image cardImage = new Image
                {
                    Source = defaultCardImage.Source,
                    Stretch = defaultCardImage.Stretch,
                };
                RenderOptions.SetBitmapScalingMode(cardImage, BitmapScalingMode.HighQuality);
                cardButton.Content = cardImage;
                cardButton.Click += CardButton_Click;
                cardsContainer.Items.Add(cardButton);
            }
        }
    }

    private void CardButton_Click(object sender, RoutedEventArgs e)
    {
        if (game.Status == GameStatus.Pending)
        {
            GameService.Start();
            guideTextBlock.Visibility = Visibility.Collapsed;
        }
        if (secondButton != null && firstButton != null)
        {
            if (!GameService.CheckMatch(firstCard, secondCard))
            {
                firstButton.Content = GetDefaultImage();
                secondButton.Content = GetDefaultImage();
            }
            firstCard = null;
            secondCard = null;
            firstButton = null;
            secondButton = null;
            matchTextBlock.Visibility = Visibility.Collapsed;
            Thread.Sleep(100);
        }
        if (sender is Button button && button.Tag is Card card && game.Status == GameStatus.InProgress && !card.IsTurned)
        {
            SetCardImage(card, button);
            card.IsTurned = true;
            if (firstCard == null)
            {
                firstCard = card;
                firstButton = button;
            }
            else if (secondCard == null)
            {
                secondCard = card;
                secondButton = button;
            }
            else return;

            if (secondCard != null)
            {
                game.Attempts++;
                if (GameService.CheckMatch(firstCard, secondCard))
                {
                    matchTextBlock.Visibility = Visibility.Visible;
                    if (GameService.CheckGameFinished())
                    {
                        Console.WriteLine(game);
                        guideTextBlock.Visibility = Visibility.Collapsed;
                        matchTextBlock.Visibility = Visibility.Collapsed;
                        finishTextBlock.Text = $"Je hebt gewonnen met een score van {game.Score} in maar liefst {game.Duration} seconden";
                        DisableButtons();
                    }
                }
            }
        }
    }

    private Image GetDefaultImage()
    {
        Image image = new Image
        {
            Source = defaultCardImage.Source,
            Stretch = defaultCardImage.Stretch,
        };
        return image;
    }

    private void SetCardImage(Card card, Button button)
    {
        Image cardImage = new Image
        {
            Source = new BitmapImage(new Uri(card.imagePath)),
            Stretch = Stretch.Uniform,
        };
        RenderOptions.SetBitmapScalingMode(cardImage, BitmapScalingMode.HighQuality);
        button.Content = cardImage;
    }
    private void QuitButton_Click(object sender, RoutedEventArgs e)
    {
        game = null;
        MainWindow mainWindow = new MainWindow();
        mainWindow.Show();
        Close();
    }

    private void DisableButtons()
    {
        foreach (var item in cardsContainer.Items)
        {
            if (item is Button cardButton)
            {
                cardButton.Click -= CardButton_Click;
            }
        }
    }
}
