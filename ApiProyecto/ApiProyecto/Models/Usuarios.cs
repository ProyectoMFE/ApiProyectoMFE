﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace ApiProyecto.Models
{
    public partial class Usuarios
    {
        public Usuarios()
        {
            Solicitudes = new HashSet<Solicitudes>();
        }

        public int IdUsuario { get; set; }
        public string Correo { get; set; }
        public string Nombre { get; set; }
        public string PrimerApellido { get; set; }
        public string SegundoApellido { get; set; }
        public string Tipo { get; set; }
        public string Contrasenia { get; set; }

        public virtual ICollection<Solicitudes> Solicitudes { get; set; }
    }
}