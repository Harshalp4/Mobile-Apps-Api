namespace Bit2Sky.Bit2EHR.Storage.FileValidator;
public interface IFileValidator
{
    void Validate(IFileValidateInput file);
    bool CanValidate(IFileValidateInput file);
}