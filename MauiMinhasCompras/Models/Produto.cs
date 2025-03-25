using SQLite;

namespace MauiMinhasCompras.Models
{
    public class Produto
    {
        string def_descricao;
        double def_quant;
        double def_preco;
        readonly DateTime currentTime = DateTime.Now;
        long def_data;
        



        [PrimaryKey, AutoIncrement]

        public int Id { get; set; }
        public string Descricao
        {
            get => def_descricao;
            set
            {
                if (value == null)
                {
                    throw new Exception("O produto não possuí uma descrição.");
                }

                def_descricao = value;
            }
        }

        public double Preco
        {
            get => def_preco;
            set
            {
                if (value <= 0.0)
                {
                    value = 1;
                }

                def_preco = value;
            }
        }
        public double Quantidade
        {
            get => def_quant;
            set
            {
                if (value <= 0.0)
                {
                    value = 1;
                }

                def_quant = value;
            }
        }
        public double Total { get => Quantidade * Preco; }

        public long DataCadastro { get; set; } // Tempo no formato Unix em segundos

        public DateTime DataPesquisa { get; set; } // Tempo no formato DateTime, usado para a parte visual do app

    }
}
