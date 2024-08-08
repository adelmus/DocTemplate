namespace DocTemplate
{
    public class DocField
    {
        public string FileName { get; set; }        
        public Dictionary<string, string>? Fields { get; set; }    
        public bool IsBarcode { get; set; } = false;
        public bool IsQRcode { get; set; } = false;
        public string code { get; set; } = string.Empty;
        public DocFieldPosition? Position { get; set; } = new DocFieldPosition();
    }
    public class DocFieldPosition
    {
        public float HorizontalPosition { get; set; } = 4f;
        public float VerticalPosition { get; set; } = 60f;
        public float Height { get; set; } = 45;
        public float Width { get; set; } = 40;        
    }
}
