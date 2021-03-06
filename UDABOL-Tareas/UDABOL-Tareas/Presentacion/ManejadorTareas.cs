﻿using System;
using System.Collections.Generic;
using System.Text;
using Modelo;
using Util;

namespace Presentacion
{
    class ManejadorTareas
    {
        public static void MostrarListadoTareas(KeyValuePair<Usuario, List<ModeloBase>> usuarioLista)
        {
            while (true)
            {
                System.Console.WriteLine("Este es el Listado de Tareas");

                System.Console.WriteLine("Id\tFecha\tNombre\tUsuario\tEstado");

                foreach (Tarea tarea in usuarioLista.Value)
                {
                    System.Console.WriteLine("%s\t%s\t%s\t%s\t%s", tarea.ObtenerId(), tarea.ObtenerFecha(), tarea.ObtenerNombre(), tarea.ObtenerUsario().ObtenerNombre(), tarea.ObtenerEstado());
                }
                System.Console.WriteLine("Desea Modficiar una Tarea?(S/N)");
                String modificarTarea = System.Console.ReadLine();
                if (modificarTarea.ToUpper().Equals("S"))
                {
                    System.Console.WriteLine("Ingrese el Id de la Tarea a Modificar:");
                    String idTarea = System.Console.ReadLine();
                    Negocio.ManejadorTareas manejador = new Negocio.ManejadorTareas();

                    KeyValuePair<Tarea, List<String>> tareaCamposModificables = manejador.VerificarModificabilidad(usuarioLista.Key, usuarioLista.Value, idTarea);
                    //0  Usuario Comun -----> Estado
                    //1  Super Usuario -----> Nombre, Usuario, Estado
                    System.Console.WriteLine("Ingrese que campo quiere Modificar:");
                    foreach (String campo in tareaCamposModificables.Value)
                    {
                        System.Console.WriteLine("\t" + (tareaCamposModificables.Value.IndexOf(campo) + 1).ToString() + ". " + campo);
                    }
                    Int32 indiceCampoModificar = Int32.Parse(System.Console.ReadLine());
                    if (indiceCampoModificar > 0 && indiceCampoModificar <= tareaCamposModificables.Value.Count)
                    {
                        String nombreCampo = tareaCamposModificables.Value.[indiceCampoModificar - 1];
                        System.Console.WriteLine("El Valor Actual del Campo: " + nombreCampo + ", es:" + tareaCamposModificables.Key.ObtenerValorCampo(nombreCampo) + ", Cuál quieres que sea su nuevo valor?:");
                        String valorCampo = System.Console.ReadLine();
                        tareaCamposModificables.Key.GuardarValorCampo(nombreCampo, valorCampo);
                        Utilidades.salida("Se guardo con existo");
                    }
                    else
                    {
                        System.Console.WriteLine("Rango incorrecto");
                    }
                }
                else
                {
                    System.Console.WriteLine("Desea Salir?(S/N)");
                    String salir = System.Console.ReadLine();
                    if (salir.ToUpper().Equals("S"))
                    {
                        return;
                    }
                }
            }
        }
    }
}
