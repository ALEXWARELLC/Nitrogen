using System.ComponentModel;

namespace Nitrogen.Settings;

public class WebContentSettings
{
    /// <summary>
    /// Specified Timeout time in seconds.
    /// </summary>
    public short Timeout { get; set; } = 5;

    /// <summary>
    /// Should an exception be thrown if an operation fails?
    /// </summary>
    public bool ThrowExecptionOnFail { get; set; } = false;
}
