namespace FamilyHub.Web.ViewModels.Events
{
    using FamilyHub.Data.Models.Planner;
    using FamilyHub.Services.Mapping;

    public class AssignedUsersViewModel: IMapFrom<UserEvent>
    {
        public string UserUserName { get; set; }
    }
}