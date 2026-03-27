using System.ComponentModel.DataAnnotations;

namespace GestorContraseñas.Components.Models;

public class Credencial
{
    public Guid Id { get; set; } = Guid.NewGuid(); // Identificador único para búsquedas y UI

    [Required(ErrorMessage = "El nombre del servicio es obligatorio")]
    public string Servicio { get; set; } = string.Empty;

    [Required(ErrorMessage = "El nombre de usuario es obligatorio")]
    public string Usuario { get; set; } = string.Empty;

    [Required]
    [MinLength(8, ErrorMessage = "La contraseña debe tener al menos 8 caracteres")]
    public string Password { get; set; } = string.Empty;

    public NivelFortaleza Fortaleza { get; set; } = NivelFortaleza.Insegura;

    // Propiedad calculada para la UI (opcional, ayuda a mostrar colores)
    public string ColorFortaleza => Fortaleza switch
    {
        NivelFortaleza.MuyFuerte => "green",
        NivelFortaleza.Fuerte => "lightgreen",
        NivelFortaleza.Media => "orange",
        _ => "red"
    };
}