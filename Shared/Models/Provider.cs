using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CREATIM_naloga.Shared.Models
{
    public class Provider
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; } = string.Empty;
        public int Position { get; set; } = 0;
        public int SentCount { get; set; } = 0;
    }
}
