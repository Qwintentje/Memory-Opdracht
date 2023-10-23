namespace WPF;

public static class FileService
{
    public static bool CopyImageToDestination(string sourcePath, string destinationDirectory, string selectedImagePath)
    {
        try
        {
            // Ensure the destination directory exists
            if (!Directory.Exists(destinationDirectory))
            {
                Directory.CreateDirectory(destinationDirectory);
            }

            string fileName = Path.GetFileName(sourcePath);

            // Construct the destination path
            string destinationPath = Path.Combine(destinationDirectory, fileName);

            // Check for file name conflicts and handle appropriately
            if (File.Exists(destinationPath))
            {
                MessageBox.Show($"Error: File '{fileName}' already exists in the destination directory.");
                return false; // Don't proceed with the upload
            }
            else
            {
                File.Copy(sourcePath, destinationPath);
                return true;
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Error uploading image: {ex.Message}");
            return false;
        }
    }

    public static void ClearIconsDirectory()
    {
        string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
        string iconsDirectory = Path.Combine(baseDirectory, "assets", "uploadedimages");
        try
        {
            if (Directory.Exists(iconsDirectory))
            {
                foreach (var file in Directory.GetFiles(iconsDirectory))
                {
                    File.Delete(file);
                }
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Error clearing icons directory: {ex.Message}");
        }
    }
}
