using MauiMinhasCompras.Models;

namespace MauiMinhasCompras.Views;

public partial class EditarProduto : ContentPage
{
    Produto produtoAntigo;

    public EditarProduto(Produto prdt_editado)
    {
        InitializeComponent();

        produtoAntigo = prdt_editado;

        ent_desc_prdt.Text = prdt_editado.Descricao;
        ent_qnt_prdt.Text = Convert.ToString(prdt_editado.Quantidade);
        ent_preco_prdt.Text = Convert.ToString(prdt_editado.Preco);

        

    }



    private async void Button_Clicked(object sender, EventArgs e) // Salvar
    {
        try
        {
            Produto novoProduto = new Produto() {Id = produtoAntigo.Id , Descricao = ent_desc_prdt.Text, Quantidade = Convert.ToDouble(ent_qnt_prdt.Text), Preco = Convert.ToDouble(ent_preco_prdt.Text) };
            await App.Db.Update(novoProduto);
            bool confirm = await DisplayAlert("Sucesso!", "O objeto foi atualizado com sucesso!", "Voltar", "Continuar");
            if (confirm)
            {
                await Navigation.PopAsync();
            }

        }
        catch (Exception ex) { await DisplayAlert("Erro!", ex.Message, "OK!"); }
    }

    private void ToolbarItem_Clicked(object sender, EventArgs e) // Voltar
    {
        try
        {
            Navigation.PopAsync();
        }
        catch (Exception ex) { DisplayAlert("Erro!", ex.Message, "OK!"); }
    }
}