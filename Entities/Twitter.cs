using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace TrumpTwitter.Entities
{

  [Table("Twitter")]
  public class Twitter
  {
    [Column("id_str")]
    [Key]
    public string IDStr { get; set; }

    [Column("text")]
    public string Text { get; set; }

    [Column("created_at")]
    public DateTime CreatedAT { get; set; }

    [Column("retweet_count")]
    public int RetweetCount { get; set; }

    [Column("favorite_count")]
    public string FavoriteCount { get; set; }

    [Column("is_retweet")]
    public byte IsRetweet { get; set; }

    [Column("Source")]
    public string Source { get; set; }
  }
}