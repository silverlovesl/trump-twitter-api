using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace TrumpTwitter.Entities
{

  [Table("word_frequence")]
  public class WordFrequence
  {
    [Column("id")]
    [Key]
    public string ID { get; set; }

    [Column("word")]
    public string Word { get; set; }

    [Column("word")]
    public int Count { get; set; }

    [Column("category")]
    public int Category { get; set; }
  }
}