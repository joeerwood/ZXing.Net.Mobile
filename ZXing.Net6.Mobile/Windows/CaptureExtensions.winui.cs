
namespace ZXing.Mobil
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using global::WinRT.Interop;
    using Microsoft.UI.Xaml;
    using Windows.Foundation.Collections;
    using Windows.Media.Capture;
    using Windows.Storage;
    using Windows.System;

    public class CameraCaptureUI
    {
        private readonly LauncherOptions launcherOptions;

        public CameraCaptureUI(Window window)
        {
            var hWnd = WindowNative.GetWindowHandle(window);
            this.launcherOptions = new LauncherOptions();
            InitializeWithWindow.Initialize(this.launcherOptions, hWnd);

            this.launcherOptions.TreatAsUntrusted = false;
            this.launcherOptions.DisplayApplicationPicker = false;
            this.launcherOptions.TargetApplicationPackageFamilyName = "Microsoft.WindowsCamera_8wekyb3d8bbwe";
        }

        public async Task<StorageFile> CaptureFileAsync(CameraCaptureUIMode mode)
        {
            if (mode != CameraCaptureUIMode.Photo)
            {
                throw new NotImplementedException();
            }

            var currentAppData = ApplicationData.Current;
            var tempLocation = currentAppData.TemporaryFolder;
            var tempFileName = "CCapture.jpg";
            var tempFile = await tempLocation.CreateFileAsync(tempFileName, CreationCollisionOption.GenerateUniqueName);
            var token = Windows.ApplicationModel.DataTransfer.SharedStorageAccessManager.AddFile(tempFile);

            var valueSet = new ValueSet();
            valueSet.Add("MediaType", "photo");
            valueSet.Add("PhotoFileToken", token);

            var uri = new Uri("microsoft.windows.camera.picker:");
            var result = await Launcher.LaunchUriForResultsAsync(uri, this.launcherOptions, valueSet);
            if (result.Status == LaunchUriStatus.Success && result.Result != null)
            {
                return tempFile;
            }

            return null;
        }
    }
}
