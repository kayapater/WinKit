using System.Windows;

namespace WinKit
{
    public partial class App : Application
    {
        public static bool IsDarkTheme { get; private set; } = true;

        public static void ToggleTheme()
        {
            IsDarkTheme = !IsDarkTheme;
            var themePath = IsDarkTheme ? "Themes/DarkTheme.xaml" : "Themes/LightTheme.xaml";
            
            var newTheme = new ResourceDictionary { Source = new Uri(themePath, UriKind.Relative) };
            
            Current.Resources.MergedDictionaries.Clear();
            Current.Resources.MergedDictionaries.Add(newTheme);
        }

        public static void SetTheme(bool isDark)
        {
            IsDarkTheme = isDark;
            var themePath = isDark ? "Themes/DarkTheme.xaml" : "Themes/LightTheme.xaml";
            
            var newTheme = new ResourceDictionary { Source = new Uri(themePath, UriKind.Relative) };
            
            Current.Resources.MergedDictionaries.Clear();
            Current.Resources.MergedDictionaries.Add(newTheme);
        }
    }
}
