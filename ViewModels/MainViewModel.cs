using DAM2_DI_UD03_P01_Patrick_Galan.Models;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace DAM2_DI_UD03_P01_Patrick_Galan.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<Tarea> Tareas { get; } = new ObservableCollection<Tarea>();

        public MainViewModel()
        {
            Tarea tarea1 = new Tarea
            {
                Titulo = "Crear repositorio GitHub para la práctica",
                Modulo = "DI",
                FechaLimite = DateTime.Today.AddDays(1),
                Prioridad = NivelPrioridad.Baja,
                EsUrgente = false,
                Completada = true
            };
            Tarea tarea2 = new Tarea
            {
                Titulo = "Configurar contenedor Docker para Odoo 18",
                Modulo = "SGE",
                FechaLimite = DateTime.Today.AddDays(8),
                Prioridad = NivelPrioridad.Media,
                EsUrgente = false,
                Completada = false
            };
            Tarea tarea3 = new Tarea
            {
                Titulo = "Diseñar wireframe de la biblioteca escolar",
                Modulo = "AD",
                FechaLimite = DateTime.Today.AddDays(6),
                Prioridad = NivelPrioridad.Alta,
                EsUrgente = false,
                Completada = false
            };

            Tarea tarea4 = new Tarea
            {
                Titulo = "Instalar extensiones de Figma para VS Code",
                Modulo = "PSP",
                FechaLimite = DateTime.Today.AddDays(4),
                Prioridad = NivelPrioridad.Media,
                EsUrgente = false,
                Completada = true
            };

            Tarea tarea5 = new Tarea
            {
                Titulo = "Repasar árboles lógico y visual en XAML",
                Modulo = "EIE",
                FechaLimite = DateTime.Today.AddDays(2),
                Prioridad = NivelPrioridad.Alta,
                EsUrgente = true,
                Completada = true
            };

            Tareas.CollectionChanged += Tareas_CollectionChanged;

            Tareas.Add(tarea1);
            Tareas.Add(tarea2);
            Tareas.Add(tarea3);
            Tareas.Add(tarea4);
            Tareas.Add(tarea5);
        }

        private string _nuevoTitulo = "";
        private string _nuevoModulo = "DI";
        private DateTime? _nuevaFecha = null;
        private bool _nuevoEsUrgente = false;

        private NivelPrioridad _nuevaPrioridad = NivelPrioridad.Baja;

        public string NuevoTitulo
        {
            get => _nuevoTitulo;
            set
            {
                if (_nuevoTitulo != value)
                {
                    _nuevoTitulo = value;
                    OnPropertyChanged();
                    OnPropertyChanged(nameof(TituloValido));
                }
            }
        }

        public string NuevoModulo
        {
            get => _nuevoModulo;
            set
            {
                if (_nuevoModulo != value)
                {
                    _nuevoModulo = value;
                    OnPropertyChanged();
                }
            }
        }

        public DateTime? NuevaFecha
        {
            get => _nuevaFecha;
            set
            {
                if (_nuevaFecha != value)
                {
                    _nuevaFecha = value;
                    OnPropertyChanged();
                }
            }
        }

        public bool NuevoEsUrgente
        {
            get => _nuevoEsUrgente;
            set
            {
                if (_nuevoEsUrgente != value)
                {
                    _nuevoEsUrgente = value;
                    OnPropertyChanged();
                }
            }
        }

        public NivelPrioridad NuevaPrioridad
        {
            get => _nuevaPrioridad;
            set
            {
                if (_nuevaPrioridad != value)
                {
                    _nuevaPrioridad = value;
                    OnPropertyChanged();
                }
            }
        }

        public List<string> Modulos { get; } = new List<string> { "DI", "SGE", "AD", "PSP", "PMDM", "EIE" };


        public int TareasPendientesCount => Tareas.Count(t => !t.Completada);

        public int TareasCompletadas => Tareas.Count(t => t.Completada);

        public double ProgresoPorcentaje => Tareas.Count() == 0 ? (double)0 : (double)TareasCompletadas / Tareas.Count * 100;

        public string TextoProgreso => $"{TareasCompletadas} de {Tareas.Count} completadas ({ProgresoPorcentaje:F0}%)";

        public string TextoPendientes => $"{TareasPendientesCount} tareas pendientes.";

        public bool TituloValido => !string.IsNullOrWhiteSpace(NuevoTitulo);

        private string _estadoApp = "Listo para operar.";

        public string EstadoApp
        {
            get => _estadoApp;
            set
            {
                if (_estadoApp != value)
                {
                    _estadoApp = value;
                    OnPropertyChanged();
                }
            }
        }

        private void Tareas_CollectionChanged(object? sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.NewItems != null)
            {
                foreach (Tarea tarea in e.NewItems)
                {
                    tarea.PropertyChanged += Tarea_PropertyChanged;
                }
            }

            if (e.OldItems != null)
            {
                foreach (Tarea tarea in e.OldItems)
                {
                    tarea.PropertyChanged -= Tarea_PropertyChanged;
                }
            }

            ActualizarContadores();
        }

        private void ActualizarContadores()
        {
            OnPropertyChanged(nameof(TareasPendientesCount));
            OnPropertyChanged(nameof(TareasCompletadas));
            OnPropertyChanged(nameof(ProgresoPorcentaje));
            OnPropertyChanged(nameof(TextoProgreso));
            OnPropertyChanged(nameof(TextoPendientes));
        }

        private void Tarea_PropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            ActualizarContadores();
        }

        public void Anadir()
        {
            if (string.IsNullOrWhiteSpace(NuevoTitulo)) { return; }


            Tarea tarea = new Tarea
            {
                Titulo = NuevoTitulo,
                Modulo = NuevoModulo,
                FechaLimite = NuevaFecha,
                EsUrgente = NuevoEsUrgente,
                Prioridad = NuevaPrioridad,
                Completada = false
            };

            Tareas.Add(tarea);
            Limpiar();
            EstadoApp = "Tarea agregada con éxito";
        }

        public void Eliminar(Tarea tarea)
        {
            Tareas.Remove(tarea);
            EstadoApp = Tareas.Count == 0 ? "Lista vaciada" : "Tarea eliminada";
        }

        public void Limpiar()
        {
            NuevoTitulo = "";
            NuevoModulo = "DI";
            NuevaFecha = null;
            NuevoEsUrgente = false;
            NuevaPrioridad = NivelPrioridad.Baja;
        }


        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}
