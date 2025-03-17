using MauiMinhasCompras.Models;
using System.Collections.ObjectModel;

namespace MauiMinhasCompras.Views;

public partial class ListarProduto : ContentPage
{
    ObservableCollection<Produto> lista = new ObservableCollection<Produto>();

    public ListarProduto()
    {
        InitializeComponent();

        lst_produtos.ItemsSource = lista;
    }

    protected async override void OnAppearing()
    {
        try
        {
            lista.Clear();

            List<Produto> tmp = await App.Db.ListAll();

            tmp.ForEach(x => lista.Add(x));

           

        }
        catch (Exception ex) { await DisplayAlert("Erro", ex.Message, "OK!"); }
    }

    private void ToolbarItem_Clicked(object sender, EventArgs e)
    {
        try
        {
            Navigation.PushAsync(new Views.AdicionarProduto());
        }
        catch (Exception ex) { DisplayAlert("Erro", ex.Message, "OK!"); }
    }

    private async void txt_search_TextChanged(object sender, TextChangedEventArgs e)
    {
        try
        {
            string newMessage = e.NewTextValue;

            lista.Clear();

            List<Produto> tmp = await App.Db.SearchTable(newMessage);

            tmp.ForEach(x => lista.Add(x));
        }
        catch (Exception ex) { await DisplayAlert("Erro", ex.Message, "OK!"); }
    }

    private void ToolbarItem_Clicked_1(object sender, EventArgs e)
    {

        try
        {
            double soma = lista.Sum(i => i.Total);

            DisplayAlert("Total da Soma", $"{soma:C}", "OK!");
        }
        catch (Exception ex) { DisplayAlert("Erro", ex.Message, "OK!"); }

    }

    private async void MenuItem_Clicked(object sender, EventArgs e)
    {
        var menuItem = (MenuItem)sender;
        var newProduto = (Produto)menuItem.BindingContext;

        var confirm = await DisplayAlert("Deletar Item", $"Você deseja deletar {newProduto.Descricao} do DB?", "Sim, meu rei.", "Não");

        if (confirm == true)
        {
            try
            {
                await App.Db.Delete(newProduto.Id);

                lista.Remove(newProduto);

                await DisplayAlert("Deletar Item", $"O item {newProduto.Descricao} foi excluido com sucesso.", "Ok!");

            }
            catch (Exception ex)
            {
                await DisplayAlert("Deletar Item", $"Erro ao deletar o item {newProduto.Descricao}. Erro:{ex.Message}", "Ok!");
            }




        }
    }

    private async void MenuItem_Clicked_1(object sender, EventArgs e)
    {
        var menuItem = (MenuItem)sender;
        var newProduto = (Produto)menuItem.BindingContext;
        await Navigation.PushAsync(new Views.EditarProduto(newProduto));
    }
}