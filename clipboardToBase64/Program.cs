// See https://aka.ms/new-console-template for more information

using System.Diagnostics;

Thread thread = new(() =>
{
    string? file = Main();
    Thread.Sleep(1000);
    if (file != null)
    {
        File.Delete(file);
    }
});
thread.SetApartmentState(ApartmentState.STA);
thread.Start();

string? Main()
{
    Image? image = Clipboard.GetImage();
    if (image == null)
    {
        return null;
    }
    string filePath = SaveImage(image);
    Process.Start(new ProcessStartInfo()
    {
        UseShellExecute = true,
        CreateNoWindow = true,
        FileName = "chrome.exe",
        Arguments = filePath, // or https://lens.google.com/search?p=... I have to check how this works.
        WindowStyle = ProcessWindowStyle.Hidden,
    });

    return filePath;
}

string SaveImage(Image image)
{
    string location = Path.GetTempPath();
    string filePath = Path.Combine(location, Guid.NewGuid().ToString() + ".png");
    image.Save(filePath);
    return filePath;
}