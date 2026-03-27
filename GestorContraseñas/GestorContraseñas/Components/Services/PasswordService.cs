using System.Security.Cryptography;
using System.Text;
using GestorContraseñas.Components.Models;

namespace GestorContraseñas.Components.Services;

public class PasswordService : IPasswordService
{
    private readonly List<Credencial> _credenciales = new();

    public string GenerarPassword(int longitud, bool conSimbolos)
    {
        if (longitud < 1) return string.Empty;

        const string letras = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
        const string simbolos = "!@#$%^&*()-_=+";
        
        string caracteresPermitidos = conSimbolos ? letras + simbolos : letras;
        StringBuilder sb = new StringBuilder();

        // SonarQube Checklist: Usar RandomNumberGenerator en lugar de Random
        for (int i = 0; i < longitud; i++)
        {
            int indice = RandomNumberGenerator.GetInt32(caracteresPermitidos.Length);
            sb.Append(caracteresPermitidos[indice]);
        }

        return sb.ToString();
    }

    public NivelFortaleza EvaluarFortaleza(string password)
    {
        if (string.IsNullOrEmpty(password)) return NivelFortaleza.Insegura;

        int puntuacion = 0;
        if (password.Length >= 8) puntuacion++;
        if (password.Length >= 12) puntuacion++;
        if (password.Any(char.IsUpper)) puntuacion++;
        if (password.Any(char.IsLower)) puntuacion++;
        if (password.Any(char.IsDigit)) puntuacion++;
        if (password.Any(ch => !char.IsLetterOrDigit(ch))) puntuacion++;

        return puntuacion switch
        {
            <= 2 => NivelFortaleza.Debil,
            3 or 4 => NivelFortaleza.Media,
            5 => NivelFortaleza.Fuerte,
            _ => NivelFortaleza.MuyFuerte
        };
    }

    public bool VerificarPasswordsRepetidos(string password)
    {
        // LINQ para verificar si ya existe esa contraseña en la lista
        return _credenciales.Any(c => c.Password == password);
    }

    public List<Credencial> ObtenerTodas() => _credenciales;

    public void AgregarCredencial(Credencial credencial)
    {
        credencial.Fortaleza = EvaluarFortaleza(credencial.Password);
        _credenciales.Add(credencial);
    }

    public Credencial? BuscarPorServicio(string servicio)
    {
        return _credenciales.FirstOrDefault(c => 
            c.Servicio.Contains(servicio, StringComparison.OrdinalIgnoreCase));
    }

    public void ModificarPassword(Guid id, string nuevoPassword)
    {
        var cred = _credenciales.FirstOrDefault(c => c.Id == id);
        if (cred != null)
        {
            cred.Password = nuevoPassword;
            cred.Fortaleza = EvaluarFortaleza(nuevoPassword);
        }
    }
}