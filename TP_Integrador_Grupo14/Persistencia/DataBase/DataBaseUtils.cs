using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistencia.DataBase
{
    public class DataBaseUtils
    {
        private string _rutaBase;

        public DataBaseUtils()
        {
            string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\..\Persistencia\DataBase\Tablas");
            _rutaBase = Path.GetFullPath(path);

            // Verificación de si la ruta encontrada existe, si no, se utiliza la implementación anterior como fallback.
            if (!Directory.Exists(_rutaBase))
            {
                // Busca la carpeta Persistencia/DataBase/Tablas desde el directorio actual
                _rutaBase = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Persistencia", "DataBase", "Tablas");

                // Si no existe, intenta desde el directorio de la solución
                if (!Directory.Exists(_rutaBase))
                {
                    _rutaBase = Path.Combine(Directory.GetCurrentDirectory(), "Persistencia", "DataBase", "Tablas");
                }
            }
            Console.WriteLine($"[DEBUG] DataBaseUtils: Usando ruta base: {_rutaBase}");
        }

        public List<String> BuscarRegistro(String nombreArchivo)
        {
            string rutaCompleta = Path.Combine(_rutaBase, nombreArchivo);
            List<String> listado = new List<String>();

            try
            {
                if (!File.Exists(rutaCompleta))
                {
                    Console.WriteLine($"Archivo no encontrado: {rutaCompleta}");
                    return listado;
                }

                using (StreamReader sr = new StreamReader(rutaCompleta))
                {
                    string linea;
                    bool primeraLinea = true;
                    while ((linea = sr.ReadLine()) != null)
                    {
                        if (primeraLinea && linea.ToLower().Contains("legajo"))
                        {
                            primeraLinea = false;
                            continue;
                        }
                        primeraLinea = false;
                        if (!string.IsNullOrWhiteSpace(linea))
                        {
                            Console.WriteLine($"[DEBUG] Leyendo línea: {linea}");
                            listado.Add(linea);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error al leer archivo {nombreArchivo}: {e.Message}");
            }
            return listado;
        }

        // Busca un registro específico por ID (primera columna)
        public String BuscarRegistroPorId(String nombreArchivo, String id)
        {
            List<String> registros = BuscarRegistro(nombreArchivo);

            foreach (string registro in registros)
            {
                string[] campos = registro.Split(';');
                if (campos.Length > 0 && campos[0].Equals(id, StringComparison.OrdinalIgnoreCase))
                {
                    return registro;
                }
            }
            return null; // No encontrado
        }

        // Busca registros que coincidan con un valor en una columna específica
        public List<String> BuscarRegistrosPorColumna(String nombreArchivo, int columna, String valor)
        {
            List<String> registros = BuscarRegistro(nombreArchivo);
            List<String> coincidencias = new List<String>();

            foreach (string registro in registros)
            {
                string[] campos = registro.Split(';');
                if (campos.Length > columna && campos[columna].Equals(valor, StringComparison.OrdinalIgnoreCase))
                {
                    coincidencias.Add(registro);
                }
            }
            return coincidencias;
        }

        public void BorrarRegistro(string id, String nombreArchivo)
        {
            string rutaCompleta = Path.Combine(_rutaBase, nombreArchivo);

            try
            {
                if (!File.Exists(rutaCompleta))
                {
                    Console.WriteLine($"El archivo no existe: {rutaCompleta}");
                    return;
                }

                List<string> registros = BuscarRegistro(nombreArchivo);
                var registrosRestantes = registros.Where(linea =>
                {
                    var campos = linea.Split(';');
                    return campos.Length == 0 || campos[0] != id;
                }).ToList();

                File.WriteAllLines(rutaCompleta, registrosRestantes);
                Console.WriteLine($"Registro con ID {id} borrado correctamente.");
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error al borrar registro: {e.Message}");
            }
        }

        public void AgregarRegistro(string nombreArchivo, string nuevoRegistro)
        {
            string rutaCompleta = Path.Combine(_rutaBase, nombreArchivo);

            try
            {
                // Crea el archivo si no existe
                if (!File.Exists(rutaCompleta))
                {
                    File.Create(rutaCompleta).Close();
                }

                using (StreamWriter sw = new StreamWriter(rutaCompleta, append: true))
                {
                    sw.WriteLine(nuevoRegistro);
                }

                Console.WriteLine("Registro agregado correctamente.");
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error al agregar registro: {e.Message}");
            }
        }

        // Actualiza un registro existente
        public void ActualizarRegistro(string nombreArchivo, string id, string nuevoRegistro)
        {
            string rutaCompleta = Path.Combine(_rutaBase, nombreArchivo);

            try
            {
                if (!File.Exists(rutaCompleta))
                {
                    Console.WriteLine($"El archivo no existe: {rutaCompleta}");
                    return;
                }

                List<string> registros = BuscarRegistro(nombreArchivo);

                for (int i = 0; i < registros.Count; i++)
                {
                    string[] campos = registros[i].Split(';');
                    if (campos.Length > 0 && campos[0] == id)
                    {
                        registros[i] = nuevoRegistro;
                        break;
                    }
                }

                File.WriteAllLines(rutaCompleta, registros);
                Console.WriteLine($"Registro con ID {id} actualizado correctamente.");
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error al actualizar registro: {e.Message}");
            }
        }

        // Verifica si un registro existe
        public bool ExisteRegistro(string nombreArchivo, string id)
        {
            return BuscarRegistroPorId(nombreArchivo, id) != null;
        }
    }
}