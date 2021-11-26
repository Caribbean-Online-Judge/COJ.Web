using System.ComponentModel;

namespace COJ.Web.Domain.Entities;
public class AccountSettings : BaseEntity
{
    public static readonly AccountSettings Default = new AccountSettings() { 
        ShowBirthday = false,
        ShowEmail= false,
        SeeSolutions = true,
        EnabledAdvancedEditor = true,
        ContestNotifications = true,
        EntriesNotifications = true,
        SubmitionNotification = true,
    };

    /// <summary>
    /// Indicate if the email is public for others users
    /// </summary>
    [DefaultValue(false)]
    public bool ShowEmail { get; set; }
    [DefaultValue(true)]
    public bool ShowBirthday { get; set; }
    public bool SeeSolutions { get; set; }
    public bool EnabledAdvancedEditor { get; set; }

    public bool ContestNotifications { get; set; }
    public bool EntriesNotifications { get; set; }
    public bool SubmitionNotification { get; set; }

}