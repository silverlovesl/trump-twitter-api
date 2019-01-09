using System.Collections.Generic;

namespace TrumpTwitter.Models
{
  public class ListWrap<T>
  {
    public IEnumerable<T> Data { get; set; }
  }
}