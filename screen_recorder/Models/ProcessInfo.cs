using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace screen_recorder.Models
{
    public record ProcessInfo(
        int Id,
        string Name,
        string MainWindowTitle,
        string? FileName
    );
}
