namespace DocTemplate
{
    public class DocField
    {
        public string FileName { get; set; }        
        public Dictionary<string, string>? Fields { get; set; }    
        public bool IsBarcode { get; set; } = false;
        public bool IsQRcode { get; set; } = false;
        public string code { get; set; } = string.Empty;
    }
}
