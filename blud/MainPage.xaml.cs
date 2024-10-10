namespace blud;

public partial class MainPage : ContentPage
{
	const int Gravidade = 1;
	const int TempoEntreFrames=25;
	bool EstaMorto=false;

	void AplicaGravidade()
	{
		mosca.TranslationY+= Gravidade;
	}

	async Task Desenha()
	{
		while (!EstaMorto)
		{
			AplicaGravidade();
			await Task.Delay(TempoEntreFrames);
		}
	}

	void OnGameOverClicked(object s,TappedEventArgs e)
	{
		FrameGameOver.IsVisible = false;
		Inicializar();
		Desenha();
	}

	void Inicializar()
	{
		mosca.TranslationY=0;
		EstaMorto= false;
	}

	public MainPage()
	{
		InitializeComponent();
	}
}

