using System;

public class Label
{
    private int whse = 20; //Variables for the label class.
    private string aisle;
    private string section;
    private string shelf;
    private string location;
    private string barCode;
    private string labelText;
    public int creationErrorStatus;

    public void BarCode()
    {
        barCode = Convert.ToString(whse) + aisle + section + Convert.ToString(shelf) + Convert.ToString(location);
        //Derives the barcode string from previously input information.
    }

    public void Whse(int x)
    {
        whse = x;
    }
    public void LabelText()
    {
        labelText = aisle + "-" + section + "-" + shelf + "-" + location;
        //Derives the Label Text from input information.
    }
    public string GetBarCode()
    {
        BarCode();
        return (barCode);
        //Creates barCode string and returns the value.
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
    public string GetLabelText()
    {
        LabelText();
        return (labelText);
        //Outputs the label text.
    }
}