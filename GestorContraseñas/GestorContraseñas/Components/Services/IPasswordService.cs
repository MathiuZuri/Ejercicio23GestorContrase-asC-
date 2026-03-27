using GestorContraseñas.Components.Models;

namespace GestorContraseñas.Components.Services;

public interface IPasswordService
{
    // Métodos requeridos por el ejercicio
    string GenerarPassword(int longitud, bool conSimbolos);
    NivelFortaleza EvaluarFortaleza(string password);
    bool VerificarPasswordsRepetidos(string password);
    
    // Gestión de la lista de credenciales
    List<Credencial> ObtenerTodas();
    void AgregarCredencial(Credencial credencial);
    Credencial? BuscarPorServicio(string servicio);
    void ModificarPassword(Guid id, string nuevoPassword);
}