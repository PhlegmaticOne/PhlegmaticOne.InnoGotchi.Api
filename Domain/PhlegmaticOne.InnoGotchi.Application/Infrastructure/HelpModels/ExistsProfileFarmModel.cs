namespace PhlegmaticOne.InnoGotchi.Application.Infrastructure.HelpModels;

public class ProfileFarmModel
{
    public ProfileFarmModel(Guid profileId) => ProfileId = profileId;

    public Guid ProfileId { get; set; }
}