using System;
using System.Collections.Generic;
using System.Text;

namespace LabelAssistant
{
    class LabelData
    {
        private int whse = 20; //Variables for the label class.
        private string aisle;
        private string section;
        private string shelf;
        private string location;
        public int creationErrorStatus;

        /// <summary>
        /// Derives the barcode string from previously input information.
        /// </summary>
        public string BarCode => Convert.ToString(whse) + aisle + section + Convert.ToString(shelf) + Convert.ToString(location);

        /// <summary>
        /// Derives the Label Text from input information.
        /// </summary>
        public string LabelText => $"{aisle}-{section}-{shelf}-{location}";

        public void SetWhse(int x)
        {
            whse = x;
        }

        public void SetAisle(string value)
        {
            if (value.Length == 2)
            {
                aisle = value.ToUpper();
                creationErrorStatus = 0;
            }
            else { creationErrorStatus = 1; }

            //Handles errors and sets the value of aisle to user input.
        }

        public void SetSection(string value)
        {
            if (value.Length == 2)
            {
                section = value.ToUpper();
                creationErrorStatus = 0;
            }
            else { creationErrorStatus = 1; }
            //Handles errors and sets the value of section to user input.
        }

        public void SetLocation(string value)
        {
            if (value.Length == 2)
            {
                location = value;
                creationErrorStatus = 0;
            }
            else { creationErrorStatus = 1; }
            //Handles errors and sets the value of location to user input.
        }

        public void SetShelf(string value)
        {
            if (value.Length == 2)
            {
                shelf = value;
                creationErrorStatus = 0;
            }
            else { creationErrorStatus = 1; }
            //Handles errors and sets the value of shelf to user input.
        }
    }
}
