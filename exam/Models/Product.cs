public class Product : BaseEntity
{
    public int CategoryId{get;set;}
    public Category? Category{get;set;}
    public decimal Price{get;set;}
}