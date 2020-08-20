namespace LabelAssistant
{
    class Label
    {
        private string fullName;
        private string barCode;
        private string labelText;

        public string BarCode
        {
            get { return barCode; }
            set { barCode = value.ToUpper(); }
        }
        public string LabelText
        {
            get { return labelText; }
            set { labelText = value.ToUpper(); }
        }
        public string FullName
        {
            get { return fullName; }
            set { fullName = value.ToUpper(); }
        }

    }
}
