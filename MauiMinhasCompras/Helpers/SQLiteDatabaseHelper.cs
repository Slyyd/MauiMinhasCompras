using MauiMinhasCompras.Models;
using SQLite;

namespace MauiMinhasCompras.Helpers
{
    public class SQLiteDatabaseHelper
    {
        readonly SQLiteAsyncConnection _conn;

        public SQLiteDatabaseHelper(string path) 
        {
            _conn = new SQLiteAsyncConnection(path);
            _conn.CreateTableAsync<Produto>();
        }

        public Task<int> Insert(Produto prdt) 
        {

            return _conn.InsertAsync(prdt);

        }

        public Task<int> Update(Produto prdt) 
        {

            return _conn.UpdateAsync(prdt);

        }

        public Task<int> Delete(int id) 
        {
            return _conn.Table<Produto>().DeleteAsync(x => x.Id == id);
        }

        public Task<List<Produto>> ListAll() 
        {
            return _conn.Table<Produto>().ToListAsync();
        }

        public Task<List<Produto>> SearchTable(string query) 
        {
            string sql = "SELECT * FROM Produto WHERE Descricao LIKE '%"+ query +"%'";

            return _conn.QueryAsync<Produto>(sql);

        }

        public Task<List<Produto>> DelEverything()
        {
            string sql = "DELETE FROM Produto";

            return _conn.QueryAsync<Produto>(sql);
        }

        public Task<List<Produto>> SearchByTime(long startingTime, long endingTime) // Função para pesquisar no BD usando o tempo (unix em segundos)
        {

            string sql = $"SELECT * FROM Produto WHERE DataCadastro BETWEEN {startingTime} AND {endingTime}";

            return _conn.QueryAsync<Produto>(sql);

        }


    }
}
