namespace blud;

public partial class MainPage : ContentPage
{
	const int Gravidade = 1;
	const int TempoEntreFrames = 25;
	bool EstaMorto = true;
	double LarguraJanela = 0;
	double AlturaJanela = 0;
	int Velocidade = 20;

	void AplicaGravidade()
	{
		mosca.TranslationY+= Gravidade;
	}

	async Task Desenha()
	{
		while(!EstaMorto)
		{
			AplicaGravidade();
			await Task.Delay(TempoEntreFrames);
			GerenciaCanos();
		}
	}

	void OnGameOverClicked(object s,TappedEventArgs e)
	{
		FrameGameOver.IsVisible = true;
		Inicializar();
		Desenha();
	}

	void Inicializar()
	{
		mosca.TranslationY = 0;
		EstaMorto= false;
	}

	protected override void OnSizeAllocated( double w, double h)
	{
		base.OnSizeAllocated(w, h);
		LarguraJanela= w;
		AlturaJanela= h;
	}

	void GerenciaCanos()
	{
		CanoCima.TranslationX -= Velocidade;
		CanoBaixo.TranslationX -= Velocidade;
		if (CanoBaixo.TranslationX <- LarguraJanela)
		{
			CanoBaixo.TranslationX = 0;
			CanoCima.TranslationX = 0;
		}
	}

	public MainPage()
	{
		InitializeComponent();
	}
}

