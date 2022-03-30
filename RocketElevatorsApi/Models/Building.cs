public class Building{
    public long Id { get; set; }
    public string? Full_Name_of_the_building_administrator {get; set;}
    public string? Email_of_the_administrator_of_the_building {get; set;}
    public string? Phone_number_of_the_building_administrator {get; set;}
    public string? Full_Name_of_the_technical_contact_for_the_building {get; set;}
    public string? Technical_contact_email_for_the_building {get; set;}
    public string? Technical_contact_phone_for_the_building {get; set;}
    public long? customer_id {get; set;}
    public long? adress_id {get; set;}
    public int? No_of_floors {get; set;}

}