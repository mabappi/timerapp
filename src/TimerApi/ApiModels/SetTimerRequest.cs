using System.ComponentModel.DataAnnotations;

namespace TimerApi.ApiModels;

public class SetTimerRequest
{
    public int Hours { get; set; }
    public int Minutes { get; set; }
    public int Seconds { get; set; }
    [Required(AllowEmptyStrings = false, ErrorMessage = "Callback Url is required. Cannot be null or empty.")]
    public string CallbackUrl { get; set; } = string.Empty;

}
