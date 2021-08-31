namespace KCSit.SalesforceAcademy.Lasagna.Business
{
    public class GenericReturn
    {
        public bool Succeeded { get; set; }
        public string Message { get; set; }
    }

    public class GenericReturn<T> : GenericReturn
    {
        public T Result { get; set; }
        
    }



}
