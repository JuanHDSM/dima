using MudBlazor;

namespace Dima.Web
{
    public static class Configuration
    {
        public static string BackendUrl { get; set; } = "http://localhost:5201";
        public const string HttpClientName = "dima";
        public static MudTheme Theme = new MudTheme()
        {
            Typography = new Typography()
            {
                Default = new Default()
                {
                    FontFamily = new[] { "Raleway", "sans-serif" }
                }
            },
            PaletteLight = new PaletteLight()
            {
                Primary = Colors.LightBlue.Accent2,
                PrimaryContrastText = "#ffffff",
                Secondary = Colors.Lime.Lighten1,
                Background = Colors.Gray.Lighten4,
                AppbarBackground = Colors.LightBlue.Darken4,
                AppbarText = Colors.Shades.White,
                TextPrimary = Colors.Shades.Black,
                DrawerText = Colors.Shades.White,
                DrawerBackground = Colors.LightBlue.Darken4
            },
            PaletteDark = new PaletteDark()
            {
                Primary = Colors.LightBlue.Darken4,
                Secondary = Colors.LightBlue.Accent3,
                AppbarBackground = Colors.LightBlue.Darken4,
                AppbarText = Colors.Shades.White,
                PrimaryContrastText = "#ffffff"
            }
        };
    }
}