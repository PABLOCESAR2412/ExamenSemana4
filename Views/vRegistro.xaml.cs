using ptorresExamen.Models;

namespace ptorresExamen.Views;

public partial class vRegistro : ContentPage
{
    private readonly string _usuario;
    private decimal _pagoMensual;

    public vRegistro(string usuario)
    {
        InitializeComponent();
        _usuario = usuario;
        lblUsuario.Text = "Usuario conectado: " + usuario;
        dateInicio.MinimumDate = DateTime.Today;
        dateInicio.Date = DateTime.Today;
    }

    private void btnCalcular_Clicked(object? sender, EventArgs e)
    {
        lblMensaje.Text = "";

        // Validación: edad solo números y >= 16
        if (!int.TryParse(txtEdad.Text, out int edad) || edad < 16)
        {
            lblMensaje.Text = "La edad debe ser un número mayor o igual a 16";
            return;
        }

        // Validación: fecha no pasada
        if (dateInicio.Date < DateTime.Today)
        {
            lblMensaje.Text = "La fecha de inicio no puede ser pasada";
            return;
        }

        // Validación: plan seleccionado
        if (pickerPlan.SelectedIndex < 0)
        {
            lblMensaje.Text = "Selecciona un plan";
            return;
        }

        // Validación: monto inicial entre 120 y 600
        if (!decimal.TryParse(txtMonto.Text, out decimal monto) || monto < 120 || monto > 600)
        {
            lblMensaje.Text = "El monto inicial debe estar entre 120 y 600";
            return;
        }

        // Cálculo: resto en 5 cuotas + 2% del costo por cuota + 10 si hay casillero
        const decimal costo = 1200m;
        decimal resto = costo - monto;
        decimal cuota = resto / 5m;
        decimal recargo = costo * 0.02m;          // 2% del costo = 24
        if (swCasillero.IsToggled)
            recargo += 10m;                       // casillero: 10 adicionales por cuota

        _pagoMensual = cuota + recargo;
        txtPago.Text = _pagoMensual.ToString("F2");
    }

    private async void btnResumen_Clicked(object? sender, EventArgs e)
    {
        // Validar que se haya calculado el pago
        if (string.IsNullOrWhiteSpace(txtPago.Text))
        {
            lblMensaje.Text = "Primero calcula el pago mensual";
            return;
        }

        try
        {
            Socio socio = new Socio
            {
                Usuario = _usuario,
                Nombre = txtNombre.Text?.Trim() ?? "",
                Apellido = txtApellido.Text?.Trim() ?? "",
                Edad = int.Parse(txtEdad.Text),
                FechaInicio = dateInicio.Date ?? DateTime.Today,
                Plan = pickerPlan.SelectedItem?.ToString() ?? "",
                Casillero = swCasillero.IsToggled,
                MontoInicial = decimal.Parse(txtMonto.Text),
                PagoMensual = _pagoMensual,
                PagoTotal = decimal.Parse(txtMonto.Text) + (_pagoMensual * 5m)
            };

            await Navigation.PushAsync(new vResumen(socio));
        }
        catch (Exception ex)
        {
            await DisplayAlertAsync("Alerta", "Error " + ex.Message, "OK");
        }
    }
}
