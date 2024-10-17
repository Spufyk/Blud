namespace blud;

public partial class MainPage : ContentPage
{
	const int Gravidade = 3;
	const int TempoEntreFrames = 25;
	bool EstaMorto = true;
	double LarguraJanela = 0;
	double AlturaJanela = 0;
	int Velocidade = 20;
	const int ForcaPulo = 30;
	const int MaxTempoPulando = 3; //frames
	bool EstaPulando = false;
	int TempoPulando = 0;
	const int aberturaMinima = 100;
	int score = 0;
	


	void AplicaGravidade()
	{
		mosca.TranslationY += Gravidade;
	}

	async Task Desenha()
	{
		while (!EstaMorto)
		{
			if (EstaPulando)
				AplicaPulo();
			else
				AplicaGravidade();

			GerenciarCanos();

			await Task.Delay(TempoEntreFrames);

		}

	}

	void OnGameOverClicked(object s, TappedEventArgs e)
	{
		FrameGameOver.IsVisible = false;
		Inicializar();
		Desenha();
	}

	void Inicializar()
	{
		mosca.TranslationY = 0;
		EstaMorto = false;
	}

	protected override void OnSizeAllocated(double w, double h)
	{
		base.OnSizeAllocated(w, h);
		LarguraJanela = w;
		AlturaJanela = h;
	}

	void GerenciarCanos()
	{

		CanoCima.TranslationX -= Velocidade;
		CanoBaixo.TranslationX -= Velocidade;
		if (CanoBaixo.TranslationX < -Largura)
		{
			CanoBaixo.TranslationX = 20;
			CanoCima.TranslationX = 20;
		
			var alturaMaxima = -100;
			var alturaMinima = -CanoBaixo.HeightRequest;

			CanoCima.TranslationY = Random.Shared.Next((int)alturaMinima, (int)alturaMaxima);
			CanoBaixo.TranslationY = CanoCima.TranslationY + aberturaMinima + CanoBaixo.HeightRequest;
			score++;
			LabelScore.Text = "Canos:" + score.ToString("D3");
		}
	}

	bool VerificaColisaoTeto()
		
	{
		var minY = -AlturaJanela / 2;

		if (mosca.TranslationY <= minY)
			return true;
		else
			return false;
	}

	bool VerificaColisaoChao()
	{
		var maxY = AlturaJanela / 2 - Imgchao.HeightRequest;

		if (mosca.TranslationY >= maxY)
			return true;
		else
			return false;
	}

	bool VericaColisao()
	{
		if (!EstaMorto)
		{
			if (VerificaColisaoTeto() || VerificaColisaoChao())
			{
				return true;
			}
		}

		return false;
	}

	void AplicaPulo()
	{
		mosca.TranslationY -= ForcaPulo;
		TempoPulando++;
		if (TempoPulando >= MaxTempoPulando)
		{
			EstaPulando = false;
			TempoPulando = 0;
		}
	}

	void OnGridClicked(object sender, EventArgs a)
	{
		EstaPulando = true;
	}

	public MainPage()
	{
		InitializeComponent();
	}
}

