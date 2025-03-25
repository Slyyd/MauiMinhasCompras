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

    private void ToolbarItem_Clicked(object sender, EventArgs e) // Mudar de página
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
            lst_produtos.IsRefreshing = true;

            lista.Clear();

            List<Produto> tmp = await App.Db.SearchTable(newMessage);

            tmp.ForEach(x => lista.Add(x));
        }
        catch (Exception ex) { await DisplayAlert("Erro", ex.Message, "OK!"); 
        }
        finally
        {
            lst_produtos.IsRefreshing = false;
        }
    }

    private void ToolbarItem_Clicked_1(object sender, EventArgs e) // Somar todos os produtos da lista
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

    private async void lst_produtos_Refreshing(object sender, EventArgs e)
    {
        try
        {
            lista.Clear();

            List<Produto> tmp = await App.Db.ListAll();

            tmp.ForEach(x => lista.Add(x));



        }
        catch (Exception ex)
        {
            await DisplayAlert("Erro", ex.Message, "OK!");
        }
        finally 
        {
            lst_produtos.IsRefreshing = false;
        }
    }

    private async void ToolbarItem_Clicked_2(object sender, EventArgs e) // Remover todos os produtos da lista
    {
        try
        {
             var confirm = await DisplayAlert("Limpar Lista", "Você tem certeza que quer apagar TUDO?", "Sim", "Não");

            if (confirm)
            {

                lst_produtos.IsRefreshing = true;

                await App.Db.DelEverything();

                lista.Clear();

            }

        }
        catch (Exception ex)
        {
            await DisplayAlert("Erro", ex.Message, "OK!");
        }
        finally
        {
            lst_produtos.IsRefreshing = false;
        }
    }

    private async void Button_Clicked(object sender, EventArgs e)
    {

        try
        {
            lst_produtos.IsRefreshing = true;

            lista.Clear();

            List<Produto> tmp = await App.Db.SearchByTime(new DateTimeOffset(dpk_inicio.Date).ToUnixTimeSeconds(), new DateTimeOffset(dpk_fim.Date).ToUnixTimeSeconds());

            tmp.ForEach(x => lista.Add(x));

        }
        catch (Exception ex) 
        {

            await DisplayAlert("Erro", ex.Message, "OK!");

        }
        finally
        {

            lst_produtos.IsRefreshing = false;

        }




    }
}