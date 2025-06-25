using System.ComponentModel.DataAnnotations.Schema;

namespace domain.Entities;

// ko gán trực tiếp vào db
// [Table("product")]
// public class Product
// {
//      [Column("id")]
//     public int Id { get; set; }

//     [Column("name")]
//     public string Name { get; set; } = default!;

//     [Column("price")]
//     public int Price { get; set; }
// }

public class Product
{
    public int Id { get; set; }

    public string Name { get; set; } = default!;

    public int Price { get; set; }
}
