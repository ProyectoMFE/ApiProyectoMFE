﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace ProyectoMFE.Models
{
    public partial class Solicitudes
    {
        public string NumSerie { get; set; }
        public int IdUsuario { get; set; }
        public string Estado { get; set; }

        public virtual Usuarios IdUsuarioNavigation { get; set; }
        public virtual Dispositivos NumSerieNavigation { get; set; }
    }
}