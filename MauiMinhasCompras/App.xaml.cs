using MauiMinhasCompras.Helpers;
using System.Globalization;

namespace MauiMinhasCompras
{
    public partial class App : Application
    {
        static SQLiteDatabaseHelper? _db;

        public static SQLiteDatabaseHelper Db 
        {
            get
            {

                if (_db == null) 
                {
                    string path = Path.Combine
                        (
                            Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                            "comprasdb.db3"
                        );
                    _db = new SQLiteDatabaseHelper(path);
                }


                return _db;
            }
        }
        public App()
        {
            InitializeComponent();

            Thread.CurrentThread.CurrentCulture = new CultureInfo("pt-BR");

            

            //MainPage = new AppShell();
            //MainPage = new NavigationPage(new Views.ListarProduto());

        }

        protected override Window CreateWindow(IActivationState? activationState)
        {
            var myWindow = new Window(new NavigationPage(new Views.ListarProduto()));

            myWindow.Height = 800;
            myWindow.Width = 600;

            return myWindow;
        }


    }
}
