using System;
using System.Collections.Generic;
using System.Text;

namespace LabelAssistant
{
    enum WhseTag
    {
        Cage = 10,
        Main = 20
    }

    class LabelData
    {
        private WhseTag whse; //Variables for the label class.
        private string aisle;
        private string section;
        private string shelf;
        private string location;
        public int creationErrorStatus;

        public LabelData(WhseTag whse = WhseTag.Main)
        {
            this.whse = whse;
        }

        /// <summary>
        /// Derives the barcode string from previously input information.
        /// </summary>
        public string BarCode => whse.ToString() + aisle + section + shelf + location;

        /// <summary>
        /// Derives the Label Text from input information.
        /// </summary>
        public string LabelText => $"{aisle}-{section}-{shelf}-{location}";

        public void SetAisle(string value)
        {
            if (value.Length == 2)
            {
                aisle = value.ToUpper();
                creationErrorStatus = 0;
            }
            else creationErrorStatus = 1;

            //Handles errors and sets the value of aisle to user input.
        }

        public void SetSection(string value)
        {
            if (value.Length == 2)
            {
                section = value.ToUpper();
                creationErrorStatus = 0;
            }
            else creationErrorStatus = 1;
            //Handles errors and sets the value of section to user input.
        }

        public void SetLocation(string value)
        {
            if (value.Length == 2)
            {
                location = value;
                creationErrorStatus = 0;
            }
            else creationErrorStatus = 1;
            //Handles errors and sets the value of location to user input.
        }

        public void SetShelf(string value)
        {
            if (value.Length == 2)
            {
                shelf = value;
                creationErrorStatus = 0;
            }
            else creationErrorStatus = 1;
            //Handles errors and sets the value of shelf to user input.
        }
    }
}
