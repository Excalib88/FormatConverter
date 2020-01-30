namespace FormatConverter.Abstractions
{
    public interface IFile
    {
        string FullName { get; set; }
        byte[] Content { get; set; }
    }
}