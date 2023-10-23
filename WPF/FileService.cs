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

            // Extract the file name from the source path
            string fileName = Path.GetFileName(sourcePath);

            // Construct the destination path
            string destinationPath = Path.Combine(destinationDirectory, fileName);

            // Check for file name conflicts and handle appropriately
            if (File.Exists(destinationPath))
            {
                // Handle the case where the file already exists (e.g., show an error message)
                MessageBox.Show($"Error: File '{fileName}' already exists in the destination directory.");
                return false; // Don't proceed with the upload
            }
            else
            {
                // Copy the file to the destination
                File.Copy(sourcePath, destinationPath);
                return true;
            }
        }
        catch (Exception ex)
        {
            // Handle exceptions (e.g., display an error message)
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
            // Check if the directory exists
            if (Directory.Exists(iconsDirectory))
            {
                // Delete all files in the directory
                foreach (var file in Directory.GetFiles(iconsDirectory))
                {
                    File.Delete(file);
                }
            }
        }
        catch (Exception ex)
        {
            // Handle exceptions (e.g., log the error, display an error message)
            MessageBox.Show($"Error clearing icons directory: {ex.Message}");
        }
    }
}
