okusing System;

// <summary>
//Todo: צרו מחלקת חריגים המטפלת במקרים בהם אין מספיק מחשבים זמינים להשאלה.
//שם המחלקה צריך להיות EquipmentUnavailableException.


class EquipmentUnavailableException : Exception
{
    public int Requested { get; private set;  }
    public int Available { get; private set; }

    public EquipmentUnavailableException(int requested, int available)
        : base($"Requested {requested} computers, but only {available} are available.")
    {
        Requested = requested;
        Available = available;
    }

}


class EquipmentRoom
{
    private int availableComputers;

    public int AvailableComputers
    {
        get
        {
            return availableComputers;
        }
        set
        {
            if(value < 0)
            {
                throw new ArgumentException("מספר המחשבים לא יכול להיות שלילי.");
            }
            availableComputers = value;
        }
    }

    public EquipmentRoom(int amount)
    {
        AvailableComputers = amount;
    }

    public void Borrow(int amount)
    {
       
            if (amount > AvailableComputers)
            {
                throw new EquipmentUnavailableException(amount, AvailableComputers);
            }
            if (amount < 0)
            {
                throw new ArgumentException("מספר המחשבים שמשאילים לא יכול להיות שלילי.");
        }



        AvailableComputers -= amount;
        Console.WriteLine($"הושאלו {amount} מחשבים בהצלחה.");
    }
}

class Program
{
    static void Main(string[] args)
    {
        int initialComputers = 10;

        EquipmentRoom room = new EquipmentRoom(initialComputers);

        // יצירת חדר מחשבים עם מספר מחשבים שלילי:
        // לאחר מימוש הבדיקה ב-set, יש לוודא שמצב כזה מטופל כראוי.
        // TODO: יש להוסיף try-catch מתאימים סביב הקריאות שעלולות לזרוק חריגה.
       // int negativeComputers = -5;
       // EquipmentRoom invalidRoom = new EquipmentRoom(negativeComputers);
       try
        {
            int negativeComputers = -5;
            EquipmentRoom invalidRoom = new EquipmentRoom(negativeComputers);
        }
       catch (ArgumentOutOfRangeException ex)
        {
            Console.WriteLine("שגיאה" + ex.Message);
        }
        try
        {
            room.Borrow(4);
            Console.WriteLine("ממשיכים");
            room.Borrow(8);
        }
        catch (EquipmentUnavailableException ex)
        {
            Console.WriteLine("שגיאה" + ex.Message);
        }
        catch (ArgumentException ex)
        {
            Console.WriteLine("שגיאה" + ex.Message);
        }
        Console.WriteLine("מספר המחשבים הזמינים: " + room.AvailableComputers);
    }
}
