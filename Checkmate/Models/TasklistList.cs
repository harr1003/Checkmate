using System.Collections.Generic;

namespace jsonreadwrite.Models
{
   public class TasklistList
   {
      public required List<Tasklist> Lists { get; set; }
   }
}