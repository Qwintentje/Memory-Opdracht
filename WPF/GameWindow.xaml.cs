namespace WPF;

public partial class GameWindow : Window
{
    private Game? game { get; set; }
    private Image defaultCardImage { get; set; }
    private Card? firstCard { get; set; }
    private Card? secondCard { get; set; }
    private Button? firstButton { get; set; }
    private Button? secondButton { get; set; }
    public GameWindow(string name, int cardAmount, List<string> uploadedImages)
    {
        InitializeComponent();
        string basePath = AppDomain.CurrentDomain.BaseDirectory;
        string imagePath = Path.Combine(basePath, $"assets/Logo.png");
        defaultCardImage = new Image
        {
            Source = new BitmapImage(new Uri(imagePath)),
            Stretch = Stretch.Uniform,
        };
        GameService.Initialize(name, cardAmount, uploadedImages);
        game = GameService.Game; //Set the game property to the same memory reference as the GameService.Game
        DataContext = game; //Set the datacontext to show attempts in a game
        GenerateCards();
    }

    private void GenerateCards()
    {
        if (game?.Cards != null)
        {
            foreach (var card in game.Cards) //Loop through all cards and make a button for them with a default image
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
                RenderOptions.SetBitmapScalingMode(cardImage, BitmapScalingMode.HighQuality); //Set quality to high
                cardButton.Content = cardImage;
                cardButton.Click += CardButton_Click;
                cardsContainer.Items.Add(cardButton);
            }
        }
    }

    private void CardButton_Click(object sender, RoutedEventArgs e)
    {
        if (game.Status == GameStatus.Pending) //If a card is clicked when game is not started, start it
        {
            GameService.Start();
            guideTextBlock.Visibility = Visibility.Collapsed;
        }
        if (secondButton != null && firstButton != null) //If both buttons have been pressed already
        {
            if (!GameService.CheckMatch(firstCard, secondCard)) //If no match, change button to default image
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
            else return; //Return if 2 cards are turned but user clicked another card

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
            Source = new BitmapImage(new Uri(card.imagePath)), //Get the image of the card his path
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
                cardButton.Click -= CardButton_Click; //descripe from the event so that the buttons won't do anything
            }
        }
    }
}
