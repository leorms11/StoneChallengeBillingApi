using StoneChallengeBillingApi.Domain.Interfaces;
using StoneChallengeBillingApi.Infra.CrossCutting.Utils.Notifications;

namespace StoneChallengeBillingApi.Domain.Models;

public abstract class BaseModel : Notifiable, ITimeStamped
{
    public int Id { get; protected set; }
    public DateTime CreatedAt { get; set; }
}