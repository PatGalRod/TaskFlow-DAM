using DAM2_DI_UD03_P01_Patrick_Galan.Models;
using DAM2_DI_UD03_P01_Patrick_Galan.ViewModels;
using System.Windows;
using System.Windows.Controls;

namespace DAM2_DI_UD03_P01_Patrick_Galan
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void BtAnadir_Click(object sender, RoutedEventArgs e)
        {
            var vm = (MainViewModel)DataContext;
            vm.Anadir();
        }

        private void BtLimpiar_Click(object sender, RoutedEventArgs e)
        {
            var vm = (MainViewModel)DataContext;
            vm.Limpiar();
        }

        private void BtEliminar_Click(object sender, RoutedEventArgs e)
        {
            var vm = (MainViewModel)DataContext;
            Tarea tarea = (Tarea)((Button)sender).DataContext;

            var respuesta = MessageBox.Show(
                $"¿Quieres eliminar la tarea \"{tarea.Titulo}\"?",
                "Confirmar eliminación",
                MessageBoxButton.YesNo,
                MessageBoxImage.Question,
                MessageBoxResult.No);

            if (respuesta == MessageBoxResult.Yes)
            {
                vm.Eliminar(tarea);
            }

        }

        private void LstTareas_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}