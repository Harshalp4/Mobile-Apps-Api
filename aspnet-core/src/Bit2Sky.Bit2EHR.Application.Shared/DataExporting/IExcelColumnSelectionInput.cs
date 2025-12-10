using System.Collections.Generic;

namespace Bit2Sky.Bit2EHR.DataExporting;

public interface IExcelColumnSelectionInput
{
    List<string> SelectedColumns { get; set; }
}

