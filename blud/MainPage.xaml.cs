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
			GerenciarCanos();
			if (EstaPulando)
				AplicaPulo();
			else
				AplicaGravidade();
			if (VericaColisao())
			{
				EstaMorto = true;
				SoundHelper.Play("bong.wav");
				FrameGameOver.IsVisible = true;
				LabelScore.Text = "Você passou \n por  " + score + "\n canos!";
				break;
			}
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
		CanoCima.TranslationX = -LarguraJanela;
		CanoBaixo.TranslationY = -LarguraJanela;
		mosca.TranslationX = 0;
		mosca.TranslationY = 0;
		score = 0;
		Velocidade = 10;
		SoundHelper.Play("chinese.wav");
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
		if (CanoBaixo.TranslationX < -LarguraJanela)
		{
			CanoBaixo.TranslationX = 20;
			CanoCima.TranslationX = 20;

			var alturaMaxima = -100;
			var alturaMinima = -CanoBaixo.HeightRequest;

			CanoCima.TranslationY = Random.Shared.Next((int)alturaMinima, (int)alturaMaxima);
			CanoBaixo.TranslationY = CanoCima.TranslationY + aberturaMinima + CanoBaixo.HeightRequest;
			score++;
			LabelScore.Text = "Canos:" + score.ToString("D3");
			SoundHelper.Play("hood.wav");
		}
		score++;
		if (score % 2 == 0)
			Velocidade++;
	}

	bool VerificaColisaoTeto()

	{
		var minY = -AlturaJanela / 2;

		if (mosca.TranslationY <= minY)
			return true;
		else
			return false;
	}

	bool VerificaColisaoCanoCima()

	{
		var minY = -AlturaJanela / 2;

		var posHPardal = (LarguraJanela / 2) - (mosca.WidthRequest / 2);
		var posVPardal = (AlturaJanela / 2) - (mosca.HeightRequest / 2) + mosca.TranslationY;
		if (posHPardal >= Math.Abs(CanoCima.TranslationX) - CanoCima.WidthRequest &&
			posHPardal <= Math.Abs(CanoCima.TranslationX) + CanoCima.WidthRequest &&
			posVPardal <= CanoCima.HeightRequest + mosca.TranslationY)
		{
			return true;
		}
		else
		{
			return false;
		}
	}

	bool VerificaColisaoCanoBaixo()

	{
		var posVPardal = (LarguraJanela / 2) - (mosca.WidthRequest / 2);
		var posHPardal = (AlturaJanela / 2) - (mosca.HeightRequest / 2) + mosca.TranslationY;
		var YMaxCano = CanoCima.HeightRequest + CanoCima.TranslationY + aberturaMinima;
		if (posHPardal >= Math.Abs(CanoBaixo.TranslationX) - CanoBaixo.WidthRequest &&
			posHPardal <= Math.Abs(CanoBaixo.TranslationX) + CanoBaixo.WidthRequest &&
			posVPardal >= YMaxCano)
		{
			return true;
		}
		else
		{
			return false;
		}
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
		return (!EstaMorto && (VerificaColisaoChao() || VerificaColisaoTeto() || VerificaColisaoCanoCima() || VerificaColisaoCanoBaixo()));
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

