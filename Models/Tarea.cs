using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace DAM2_DI_UD03_P01_Patrick_Galan.Models
{
    

    public class Tarea : INotifyPropertyChanged
    {
        private String _titulo = string.Empty;
        private String _modulo = "DI";
        private DateTime? _fechaLimite;
        private NivelPrioridad _prioridad = NivelPrioridad.Baja;
        private bool _esUrgente;
        private bool _completada;

        public String Titulo 
        {
            get => _titulo;
            set
            {
                if (_titulo != value)
                {
                    _titulo = value;
                    OnPropertyChanged();
                }
            }
        }

        public String Modulo
        {
            get => _modulo;
            set
            {
                if (_modulo != value)
                {
                    _modulo = value;
                    OnPropertyChanged();
                }
            }
        }

        public DateTime? FechaLimite
        {
            get => _fechaLimite;
            set
            {
                if (_fechaLimite != value)
                {
                    _fechaLimite = value;
                    OnPropertyChanged();
                }
            }
        }

        public NivelPrioridad Prioridad
        {
            get => _prioridad;
            set
            {
                if (_prioridad != value)
                {
                    _prioridad = value;
                    OnPropertyChanged();
                }
            } 
        }

        public bool EsUrgente
        {
            get => _esUrgente;
            set
            {
                if (_esUrgente != value)
                {
                    _esUrgente = value;
                    OnPropertyChanged();
                }
            }
        }
        public bool Completada
        {
            get => _completada;
            set
            {
                if (_completada != value)
                {
                    _completada = value;
                    OnPropertyChanged();
                }
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}
