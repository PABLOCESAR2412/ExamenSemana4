namespace ptorresExamen.Views;

public partial class vLogin : ContentPage
{
    // Matriz 4 × 2 con los usuarios y contraseñas del enunciado
    private readonly string[,] _usuarios = new string[4, 2]
    {
        { "recepcion",  "gym2026" },
        { "entrenador", "fit_01"  },
        { "uisrael",    "2026"    },
        { "gerente",    "admin123"}
    };

    public vLogin()
    {
        InitializeComponent();
    }

    private async void btnIniciar_Clicked(object? sender, EventArgs e)
    {
        string usuario = txtUsuario.Text?.Trim() ?? "";
        string contrasena = txtContrasena.Text ?? "";
        bool correcto = false;

        for (int i = 0; i < _usuarios.GetLength(0); i++)
        {
            if (_usuarios[i, 0] == usuario && _usuarios[i, 1] == contrasena)
            {
                correcto = true;
                break;
            }
        }

        if (correcto)
        {
            await Navigation.PushAsync(new vRegistro(usuario));
        }
        else
        {
            await DisplayAlertAsync("Acceso", "Dato incorrecto", "OK");
        }
    }

    private async void btnAcercaDe_Clicked(object? sender, EventArgs e)
    {
        await Navigation.PushAsync(new vAcercaDe());
    }
}
