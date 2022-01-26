namespace Web_app.Models
{
    public class ListModel
    {
        public List<ListElement> list { get; set; }
        public ListModel()
        {
            list = new List<ListElement> { };
        }
    }

    public class ListElement 
    {
        public string Age { get; set; }
        public string Name { get; set; }
    }

}
