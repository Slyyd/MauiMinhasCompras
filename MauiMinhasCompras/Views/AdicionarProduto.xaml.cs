using MauiMinhasCompras.Models;

namespace MauiMinhasCompras.Views;

public partial class AdicionarProduto : ContentPage
{
    public AdicionarProduto()
    {
        InitializeComponent();
    }

    private async void ToolbarItem_Clicked(object sender, EventArgs e)
    {
        try
        {
            Produto novoProduto = new Produto
            {
                Descricao = txt_desc.Text,
                Quantidade = Convert.ToDouble(txt_quant.Text),
                Preco = Convert.ToDouble(txt_prec.Text)
            };

            await App.Db.Insert(novoProduto);
            bool continuar = await DisplayAlert("Sucesso", "O produto foi inserido corretamente!", "Voltar", "Continuar");
            if (continuar)
            {
                await Navigation.PopAsync();
            }

        }
        catch (Exception ex) { await DisplayAlert("Erro", ex.Message, "OK!"); }
    }

    private void ToolbarItem_Clicked_1(object sender, EventArgs e)
    {
        try
        {
            Navigation.PopAsync();
        }
        catch (Exception ex) { DisplayAlert("Erro!", ex.Message, "OK!"); }
    }
}