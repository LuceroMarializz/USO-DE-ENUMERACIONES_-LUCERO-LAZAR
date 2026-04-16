namespace USO_ENUMERACIONES
{
    // ENUMERACION 
    public enum EstadoSolicitud
    {
        Pendiente = 1,   
        EnProceso = 2,
        Completada = 3,
        Cancelada = 4
    }

    //  CLASR SOLICITUD
    public class Solicitud
    {
        public int Id { get; set; }
        public string NombreCliente { get; set; }
        public string Descripcion { get; set; }
        public EstadoSolicitud Estado { get; set; }

        public Solicitud(int id, string nombreCliente, string descripcion, EstadoSolicitud estado)
        {
            Id = id;
            NombreCliente = nombreCliente;
            Descripcion = descripcion;
            Estado = estado;
        }

        public void MostrarInformacion()
        {
            Console.WriteLine($"ID: {Id}");
            Console.WriteLine($"CLIENTE: {NombreCliente}");
            Console.WriteLine($"DESCRIPCIÓN: {Descripcion}");
            Console.WriteLine($"ESTADO: {Estado}");
        
        }
    }

    class Program
    {
        static List<Solicitud> solicitudes = new List<Solicitud>();
        static int siguienteId = 1;

        static void Main()
        {
            bool salir = false;

            while (!salir)
            {
                Console.WriteLine("\nSISTEMA DE GESTIÓN DE SOLICITUDES:");
                Console.WriteLine();
                Console.WriteLine("1. REGISTRAR NUEVA SOLICITUD");
                Console.WriteLine("2. MOSTRAR TODAS LAS SOLICITUDES");
                Console.WriteLine("3. CAMBIAR ESTADO DE UNA SOLICITUD");
                Console.WriteLine("4. BUSCAR SOLICITUD POR ID");
                Console.Write("SELECCIONE UNA OPCIÓN: ");

                string seleccion = Console.ReadLine();

                switch (seleccion)
                {
                    case "1":
                        RegistrarSolicitud();
                        break;
                    case "2":
                        MostrarTodasSolicitudes();
                        break;
                    case "3":
                        CambiarEstadoSolicitud();
                        break;
                    case "4":
                        BuscarSolicitudPorId();
                        break;
                    default:
                        Console.WriteLine("OPCIÓN INVÁLIDA");
                        break;
                }
            }
        }
        static void MostrarEstadosDisponibles()
        {
            Console.WriteLine("\nESTADOS DISPONIBLES:");
            foreach (EstadoSolicitud estado in Enum.GetValues(typeof(EstadoSolicitud)))
            {
                Console.WriteLine($"{(int)estado}. {estado}");
            }
        }

        static void RegistrarSolicitud()
        {
            Console.WriteLine("\nREGISTRAR SOLICITUD:");
            Console.Write("NOMBRE DEL CLIENTE: ");
            string nombre = Console.ReadLine();
            Console.Write("DESCRIPCIÓN: ");
            string descripcion = Console.ReadLine();

           // METODO PARA MOSTRAR ESTADOS
            MostrarEstadosDisponibles();
            Console.Write("SELECCIONE ESTADO: ");

            if (int.TryParse(Console.ReadLine(), out int seleccion))
            {
                
                if (Enum.IsDefined(typeof(EstadoSolicitud), seleccion))
                {
                    EstadoSolicitud estado = (EstadoSolicitud)seleccion;
                    Solicitud nueva = new Solicitud(siguienteId++, nombre, descripcion, estado);
                    solicitudes.Add(nueva);
                    Console.WriteLine($"\n¡SOLICITUD REGISTRADA! ID: {siguienteId - 1}");
                }
                else
                {
                    Console.WriteLine("ESTADO NO VÁLIDO. SOLICITUD NO REGISTRADA.");
                }
            }
            else
            {
                Console.WriteLine("ENTRADA INVÁLIDA. SOLICITUD NO REGISTRADA.");
            }
        }

        static void MostrarTodasSolicitudes()
        {
            Console.WriteLine("\nLISTA DE SOLICITUDES");
            if (solicitudes.Count == 0)
            {
                Console.WriteLine("NO HAY SOLICITUDES REGISTRADAS.");
                return;
            }

            foreach (var solicitud in solicitudes)
            {
                solicitud.MostrarInformacion();
            }
        }
        static void CambiarEstadoSolicitud()
        {
            Console.Write("\nINGRESE ID DE LA SOLICITUD: ");
            if (!int.TryParse(Console.ReadLine(), out int id))
            {
                Console.WriteLine("ID NO VALIDO.");
                return;
            }

            Solicitud solicitudEncontrada = null;
            foreach (var solicitud in solicitudes)
            {
                if (solicitud.Id == id)
                {
                    solicitudEncontrada = solicitud;
                    break;
                }
            }

            if (solicitudEncontrada == null)
            {
                Console.WriteLine("SOLICITUD NO ENCONTRADA");
                return;
            }

            Console.WriteLine($"ESTADO ACTUAL: {solicitudEncontrada.Estado}");
            MostrarEstadosDisponibles();
            Console.Write("SELECCIONE NUEVO ESTADO: ");

            if (int.TryParse(Console.ReadLine(), out int seleccion))
            {
                if (Enum.IsDefined(typeof(EstadoSolicitud), seleccion))
                {
                    EstadoSolicitud nuevoEstado = (EstadoSolicitud)seleccion;

                    switch (nuevoEstado)
                    {
                        case EstadoSolicitud.Pendiente:
                            Console.WriteLine("Estado cambiado a PENDIENTE");
                            break;
                        case EstadoSolicitud.EnProceso:
                            Console.WriteLine("Estado cambiado a EN PROCESO");
                            break;
                        case EstadoSolicitud.Completada:
                            Console.WriteLine("Estado cambiado a COMPLETADA");
                            break;
                        case EstadoSolicitud.Cancelada:
                            Console.WriteLine("Estado cambiado a CANCELADA");
                            break;
                    }

                    solicitudEncontrada.Estado = nuevoEstado;
                    Console.WriteLine("ESTADO ACTUALIZADO CORRECTAMENTE");
                }
                else
                {
                    Console.WriteLine("ESTADO NO VÁLIDO.");
                }
            }
            else
            {
                Console.WriteLine("ENTRADA INVÁLIDA.");
            }
        }

        static void BuscarSolicitudPorId()
        {
            Console.Write("\nINGRESE ID DE LA SOLICITUD: ");
            if (!int.TryParse(Console.ReadLine(), out int id))
            {
                Console.WriteLine("ID INVÁLIDO.");
                return;
            }

            Solicitud solicitudEncontrada = null;
            foreach (var solicitud in solicitudes)
            {
                if (solicitud.Id == id)
                {
                    solicitudEncontrada = solicitud;
                    break;
                }
            }

            if (solicitudEncontrada == null)
            {
                Console.WriteLine("SOLICITUD NO ENCONTRADA");
            }
            else
            {
                Console.WriteLine("\nSOLICITUD ENCONTRADA:");
                solicitudEncontrada.MostrarInformacion();
            }
        }
    }
}