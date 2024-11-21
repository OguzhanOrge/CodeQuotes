namespace CodeQuotes
{
    public partial class MainPage : ContentPage
    {
        List<string> data = new List<string>();
        Random random = new Random();
        public MainPage()
        {
            InitializeComponent();
        }
        async Task LoadMauiAsset()
        {
            using var stream = await FileSystem.OpenAppPackageFileAsync("quotes.txt");
            using var reader = new StreamReader(stream);

            while (reader.Peek() != -1) 
            {
                if (!string.IsNullOrEmpty(reader.ReadLine()))
                {
                    data.Add(reader.ReadLine());
                }

            }
        }
        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await LoadMauiAsset();
        }
        private void btnGenerateQuote_Clicked(object sender, EventArgs e)
        {
            var startColor = System.Drawing.Color.FromArgb(random.Next(0,256), random.Next(0,256), random.Next(0, 256));
            var endColor = System.Drawing.Color.FromArgb(random.Next(0, 256), random.Next(0, 256), random.Next(0, 256));
            var color = ColorUtility.ColorControls.GetColorGradient(startColor,endColor,5);
            float stopOffset = .0f;
            var stops = new GradientStopCollection();
            foreach (var item in color)
            {
                stops.Add(new GradientStop(Color.FromArgb(item.Name), stopOffset));
                stopOffset += .2f;
            }
            var gradient = new LinearGradientBrush(stops,new Point(0,0),new Point(1,1));
            BackGround.Background = gradient;
            int index = random.Next(data.Count());
            textquetos.Text = data[index];
        }
    }

}
