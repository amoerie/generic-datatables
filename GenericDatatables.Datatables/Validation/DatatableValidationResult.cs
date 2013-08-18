namespace GenericDatatables.Datatables.Validation
{
    public class DatatableValidationResult
    {
        public string Message { get; set; }

        public DatatableValidationResult(string message)
        {
            Message = message;
        }
    }
}
