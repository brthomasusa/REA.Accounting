namespace REA.Accounting.SharedKernel.Base
{
    public class ValidationResult
    {
        public bool IsValid { get; set; }
        public List<string> Messages { get; set; } = new();
    }
}