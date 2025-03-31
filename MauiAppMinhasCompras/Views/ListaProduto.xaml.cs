
using MauiAppMinhasCompras.Models;
using System.Collections.ObjectModel;

namespace MauiAppMinhasCompras.Views;

public partial class ListaProduto : ContentPage
{
	public ListaProduto()
	{
		ObservableCollection<Produto> lista = new ObservableCollection<Produto>();



        InitializeComponent();

		lst_produtos.ItemsSource = lista;
	}

    protected async override void OnAppearing()
    {
        try
        {
            Lista.Clear();

            List<Produto> tmp = await App.Db.GetAll();

            tmp.ForEach(i => lista.Add(i));

        }
        catch (Exception ex)
        {
            await DisplayAlert("Ops", ex.Message, "OK");
        }
    }

    private void lst_produtos_ItemSelected(object sender, SelectedItemChangedEventArgs e)
    {

    }
}

private void ToolbarItem_Clicked(object sender, EventArgs e)
    {
		try
		{
			Navigation.PushAsync(new Views.NovoProduto());


        } catch (Exception ex)
		{
			DisplayAlert("Ops", ex.Message, "OK");
        }
    }

private async void txt_search_TextChanged(object sender, TextChangedEventArgs e)
{
    try
    {

        string q = e.NewTextValue;

        ListaProduto.Clear();

        List<Produto> tmp = await App.Db.Search(q);

        tmp.ForEach(i => lista.Add(i));
    }
    catch (Exception ex)
    {
        await DisplayAlert("Ops", ex.Message, "OK");
    }
}

    private void ToolbarItem_Clicked_1(object sender, EventArgs e)
    {
        double soma = Lista.Sum(i => i.Total);

        string msg = $"O total � {soma:C}";

        DisplayAlert("Total dos Produtos", msg, "OK");
    }

    private async void MenuItem_Clicked(object sender, EventArgs e)
    {
    try
    {
        MenuItem selecionado = sender as MenuItem;

        Produto p = selecionado.BindingContext as Produto;

        bool confirm = await DisplayAlert("Tem Certeza?", $"Remover {p.Descricao}?", "Sim", "N�o");

        if (confirm)
        {
            await App.Db.Delete(p.Id);
            Lista.Remove(p);
        }



    }
    catch (Exception ex)
    {
       DisplayAlert("Ops", ex.Message, "OK");
    }
}

privated void lst_produtos_ItemSelected(object sender, SelectedItemChangedEventArgs e)
{
    try
    {
        Produto p = e.SelectedItem as Produto;

        Navigation.PushAsync(new Views.EditarProduto
        {
            BindingContext = p,
        }
            );
    }
    catch (Exception ex)
    {
       DisplayAlert("Ops", ex.Message, "OK");
    }
}


