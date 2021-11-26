using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppS7SQLite.Models;
using SQLite;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AppS7SQLite
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ConsultaRegistro : ContentPage
    {
        private SQLiteAsyncConnection con;
        //crear un contenedor
        private ObservableCollection<Estudiante> tablaEstudiantes;
        public ConsultaRegistro()
        {
            InitializeComponent();
            con = DependencyService.Get<DataBase>().GetConnection();
            consulta();
        }
        public async void consulta()
        {
            
            var Registros = await con.Table<Estudiante>().ToListAsync();
            tablaEstudiantes = new ObservableCollection<Estudiante>(Registros);
            ListaUsuario.ItemsSource = tablaEstudiantes;
        }
        private void ListaUsuario_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var Obj = (Estudiante)e.SelectedItem;
            var item = Obj.Id.ToString();
            int Id = Convert.ToInt32(item);
            try
            {
                Navigation.PushAsync(new Elemento(Id));
            }
            catch (Exception ex)
            {

            }
        }
    }
}