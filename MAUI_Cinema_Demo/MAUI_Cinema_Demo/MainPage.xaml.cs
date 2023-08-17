using MAUI_Cinema_Demo.Models;
using MAUI_Cinema_Demo.Services;
using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace MAUI_Cinema_Demo;

public partial class MainPage : ContentPage
{
	int count = 0;
	private IMovieService _movieService;

	public MainPage()
	{
		InitializeComponent();
	}

	private void OnCounterClicked(object sender, EventArgs e)
	{
		count++;

		if (count == 1)
			CounterBtn.Text = $"Clicked {count} time";
		else
			CounterBtn.Text = $"Clicked {count} times";

		SemanticScreenReader.Announce(CounterBtn.Text);
	}

	protected async override void OnAppearing()
	{
		base.OnAppearing();
		this._movieService = new MovieService();
		List<Movies> list = await this._movieService.GetMoviesAsync();
    }
}

