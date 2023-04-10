

using Microsoft.AspNetCore.Components.WebView;
using Microsoft.Maui.Platform;

namespace ArmyFlag;

public partial class MainPage : ContentPage
{
	public MainPage()
	{
		InitializeComponent();

        blazorWebView.BlazorWebViewInitialized += BlazorWebViewInitialized;
        blazorWebView.BlazorWebViewInitializing += BlazorWebViewInitializing;
    }

    #region 浏览器权限

    private void BlazorWebViewInitialized(object sender, BlazorWebViewInitializedEventArgs e)
    {
#if ANDROID
        if (e.WebView.Context?.GetActivity() is not AndroidX.Activity.ComponentActivity activity)
        {
            throw new InvalidOperationException($"The permission-managing WebChromeClient requires that the current activity be a '{nameof(AndroidX.Activity.ComponentActivity)}'.");
        }

        e.WebView.Settings.JavaScriptEnabled = true;
        e.WebView.Settings.AllowFileAccess = true;
        e.WebView.Settings.MediaPlaybackRequiresUserGesture = false;
        e.WebView.Settings.SetGeolocationEnabled(true);

        //这里似乎有版本限制，加了会闪退
        e.WebView.Settings.SetGeolocationDatabasePath(e.WebView.Context?.FilesDir?.Path);
        e.WebView.SetWebChromeClient(new Platforms.Android.PermissionManagingBlazorWebChromeClient(e.WebView.WebChromeClient!, activity));

        
#endif
    }

    private void BlazorWebViewInitializing(object sender, BlazorWebViewInitializingEventArgs e)
    {
    }

    #endregion
}
