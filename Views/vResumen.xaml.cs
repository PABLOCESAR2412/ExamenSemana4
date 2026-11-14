using ptorresExamen.Models;

namespace ptorresExamen.Views;

public partial class vResumen : ContentPage
{
    private readonly Socio _socio;

    public vResumen(Socio socio)
    {
        InitializeComponent();
        _socio = socio;

        lblUsuario.Text = "Usuario conectado: " + socio.Usuario;
        lblNombre.Text = socio.Nombre;
        lblApellido.Text = socio.Apellido;
        lblEdad.Text = socio.Edad.ToString();
        lblFecha.Text = socio.FechaInicio.ToString("dd/MM/yyyy");
        lblPlan.Text = socio.Plan;
        lblCasillero.Text = socio.Casillero ? "Sí" : "No";
        lblMonto.Text = socio.MontoInicial.ToString("F2");
        lblPagoMensual.Text = socio.PagoMensual.ToString("F2");
        lblPagoTotal.Text = socio.PagoTotal.ToString("F2");
    }

    private async void btnExportar_Clicked(object? sender, EventArgs e)
    {
        try
        {
            // Nombre y ruta del archivo de texto
            string nombreArchivo = "registro_" + _socio.Nombre + "_" + _socio.Apellido + ".txt";
            string ruta = Path.Combine(FileSystem.AppDataDirectory, nombreArchivo);

            // Contenido con todos los datos del resumen
            string contenido =
                "RESUMEN DE REGISTRO - GYM ISRAEL\r\n" +
                "================================\r\n" +
                "Usuario conectado: " + _socio.Usuario + "\r\n" +
                "Nombre: " + _socio.Nombre + "\r\n" +
                "Apellido: " + _socio.Apellido + "\r\n" +
                "Edad: " + _socio.Edad + "\r\n" +
                "Fecha de inicio: " + _socio.FechaInicio.ToString("dd/MM/yyyy") + "\r\n" +
                "Plan: " + _socio.Plan + "\r\n" +
                "Casillero: " + (_socio.Casillero ? "Si" : "No") + "\r\n" +
                "Monto inicial: " + _socio.MontoInicial.ToString("F2") + "\r\n" +
                "Pago mensual: " + _socio.PagoMensual.ToString("F2") + "\r\n" +
                "Pago total: " + _socio.PagoTotal.ToString("F2") + "\r\n";

            File.WriteAllText(ruta, contenido);

            // Alerta con el nombre y la ruta del archivo
            await DisplayAlertAsync("Exportacion exitosa",
                "Archivo: " + nombreArchivo + "\nRuta: " + ruta, "Aceptar");
        }
        catch (Exception ex)
        {
            await DisplayAlertAsync("Error", "No se pudo exportar: " + ex.Message, "Aceptar");
        }
    }

    private async void btnNuevo_Clicked(object? sender, EventArgs e)
    {
        // Regresa a Registro con los campos limpios
        await Navigation.PushAsync(new vRegistro(_socio.Usuario));
    }

    private void btnCerrarSesion_Clicked(object? sender, EventArgs e)
    {
        // Abre el Login (reinicia la aplicación en la ventana Login)
        Application.Current!.Windows[0].Page = new AppShell();
    }
}
