namespace FlightManagerMVC.ViewModels
{
    public class ErrorVM : BaseVM
    {
        public string? RequestId { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }
}
