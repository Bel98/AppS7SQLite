using AppS7SQLite.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AppS7SQLite
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Elemento : ContentPage
    {
        public int IdSeleccionado;
        private SQLiteAsyncConnection con;
        IEnumerable<Estudiante> ResultadoDelete;
        IEnumerable<Estudiante> ResultadoUpdate;

        public static IEnumerable<Estudiante> Delete(SQLiteConnection db, int id)
        {
            return db.Query<Estudiante>("DELETE FROM Estudiante where Id = ?", id);
        }
        public static IEnumerable<Estudiante> Update(SQLiteConnection db, string nombre, string usuario, string contrasenia, int id)
        {
            return db.Query<Estudiante>("UPDATE Estudiante SET Nombre = ?, Usuario = ?,"+
                "Contrasenia = ? where Id = ?",nombre, usuario,contrasenia, id);
        }

        public Elemento(int Id)
        {
            con = DependencyService.Get<DataBase>().GetConnection();
            IdSeleccionado = Id;
            InitializeComponent();
        }

        private void btnActualizar_Clicked(object sender, EventArgs e)
        {
            try
            {
                var databasePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "uisrael.db3");
                var db = new SQLiteConnection(databasePath);
                ResultadoUpdate = Update(db, txtNombre.Text, txtUsuario.Text, txtContrasenia.Text, IdSeleccionado);
                DisplayAlert("Alerta", "Se actualizó correctamente", "OK");
            }
            catch(Exception ex)
            {
                DisplayAlert("Alerta", "ERROR" + ex.Message, "OK");
            }
        }

      

        private void btnEliminar_Clicked(object sender, EventArgs e)
        {
            try
            {
                var databasePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "uisrael.db3");
                var db = new SQLiteConnection(databasePath);
                ResultadoDelete = Delete(db, IdSeleccionado);
                DisplayAlert("Alerta", "Se eliminó correctamente", "OK");
                Navigation.PushAsync(new Login());
            }
            catch (Exception ex)
            {
                DisplayAlert("Alerta", "ERROR" + ex.Message, "OK");

            }
        }
    }
}