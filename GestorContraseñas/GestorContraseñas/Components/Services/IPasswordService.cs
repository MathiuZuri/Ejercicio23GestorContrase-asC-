using GestorContraseñas.Components.Models;

namespace GestorContraseñas.Components.Services;

public interface IPasswordService
{

    string GenerarPassword(int longitud, bool conSimbolos);
    NivelFortaleza EvaluarFortaleza(string password);
    
    Task<bool> VerificarPasswordsRepetidos(string password);
    Task<List<Credencial>> ObtenerTodas();
    Task AgregarCredencial(Credencial credencial);
    Task<Credencial?> BuscarPorServicio(string servicio);
    Task ModificarPassword(Guid id, string nuevoPassword);
    Task EliminarCredencial(Guid id); // Añadido para completar el CRUD
}